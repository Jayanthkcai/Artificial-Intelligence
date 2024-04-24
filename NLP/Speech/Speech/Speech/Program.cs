using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Speaker;
using Microsoft.CognitiveServices.Speech.Translation;
using Microsoft.Extensions.Configuration;


namespace Speech
{
    class Program
    {
        private static SpeechConfig speechConfig;
        static async Task Main(string[] args)
        {
            try
            {
                // Get config settings from AppSettings
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                IConfigurationRoot configuration = builder.Build();
                string aiSvcKey = configuration["SpeechKey"];
                string aiSvcRegion = configuration["SpeechRegion"];

                // Configure speech service
                speechConfig = SpeechConfig.FromSubscription(aiSvcKey, aiSvcRegion);
                Console.WriteLine("Ready to use speech service in " + speechConfig.Region);

                // Configure voice
                speechConfig.SpeechSynthesisVoiceName = "en-US-AriaNeural";

                // Get spoken input
                string command = "";
                command = await TranscribeCommand();
                if (command.ToLower() == "what time is it?")
                {
                    await TellTime();
                }

                //Speech Translation
                await  SpeechTranslator(aiSvcKey, aiSvcRegion);

                ////Speaker Recognize
                ////verify the approval status of your subscription for the Speaker Recognition service.
                //await SpeakerRecognize(aiSvcKey, aiSvcRegion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static async Task<string> TranscribeCommand()
        {
            string command = "";

            // Configure speech recognition
            using AudioConfig audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using SpeechRecognizer speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
            Console.WriteLine("Speak now...");

            // Process speech input
            SpeechRecognitionResult speech = await speechRecognizer.RecognizeOnceAsync();
            if (speech.Reason == ResultReason.RecognizedSpeech)
            {
                command = speech.Text;
                Console.WriteLine(command);
            }
            else
            {
                Console.WriteLine(speech.Reason);
                if (speech.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(speech);
                    Console.WriteLine(cancellation.Reason);
                    Console.WriteLine(cancellation.ErrorDetails);
                }
            }

            // Return the command
            return command;
        }

        static async Task TellTime()
        {
            var now = DateTime.Now;
            string responseText = "The time is " + now.Hour.ToString() + ":" + now.Minute.ToString("D2");

            // Configure speech synthesis
            //speechConfig.SpeechSynthesisVoiceName = "en-GB-RyanNeural";
             speechConfig.SpeechSynthesisVoiceName = "en-GB-LibbyNeural"; // change this
            using SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(speechConfig);

            // Synthesize spoken output
            //SpeechSynthesisResult speak = await speechSynthesizer.SpeakTextAsync(responseText);
            //if (speak.Reason != ResultReason.SynthesizingAudioCompleted)
            //{
            //    Console.WriteLine(speak.Reason);
            //}

            // Synthesize spoken output
            string responseSsml = $@"
     <speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
         <voice name='en-GB-LibbyNeural'>
             {responseText}
             <break strength='weak'/>
             Time to end this exercise!
         </voice>
         <voice name='en-US-GuyNeural'>
            I say <phoneme alphabet='sapi' ph='t ao m ae t ow'> tomato </phoneme>.
            <break strength='weak'/>Let's call the whole thing off!
         </voice>
     </speak>";

            SpeechSynthesisResult speak = await speechSynthesizer.SpeakSsmlAsync(responseSsml);
            if (speak.Reason != ResultReason.SynthesizingAudioCompleted)
            {
                Console.WriteLine(speak.Reason);
            }

            // Print the response
            Console.WriteLine(responseText);
        }

        static async Task SpeechTranslator(string subscriptionKey, string serviceRegion)
        {
            try
            {
                // Set up the speech translation config
                var translationConfig = SpeechTranslationConfig.FromSubscription(subscriptionKey, serviceRegion);

                // Set the language for the source (input) and target (output) text
                translationConfig.SpeechRecognitionLanguage = "en-US";
                translationConfig.AddTargetLanguage("fr-FR"); // Frenches-CR
                translationConfig.AddTargetLanguage("es-CR"); // Spanish
                translationConfig.AddTargetLanguage("hi-IN"); // hindifr-CH

                // Create a translation recognizer
                using var recognizer = new TranslationRecognizer(translationConfig);

                Console.WriteLine("Say something in English...");

                // Start recognition
                var result = await recognizer.RecognizeOnceAsync();

                // Check the result
                if (result.Reason == ResultReason.TranslatedSpeech)
                {
                    // Display the translated text
                    Console.WriteLine($"English: {result.Text}");
                    Console.WriteLine($"French: {result.Translations["fr"]}");
                    Console.WriteLine($"Hindi: {result.Translations["hi"]}");
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    Console.WriteLine("No speech could be recognized.");
                }
                else if (result.Reason == ResultReason.Canceled)
                {
                    var cancellation = CancellationDetails.FromResult(result);
                    Console.WriteLine($"Speech recognition canceled. Reason: {cancellation.Reason}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task SpeakerRecognize(string subscriptionKey, string serviceRegion)
        {
            // Create a SpeakerRecognitionConfig
            var speakerConfig = SpeechConfig.FromSubscription(subscriptionKey, serviceRegion);

            // persist profileMapping if you want to store a record of who the profile is
            var profileMapping = new Dictionary<string, string>();
            var profileNames = new List<string>() { "Jayanth", "Ramya" };

            var enrolledProfiles = await IdentificationEnroll(speakerConfig, profileNames, profileMapping);
            await SpeakerIdentification(speakerConfig, enrolledProfiles, profileMapping);

            foreach (var profile in enrolledProfiles)
            {
                profile.Dispose();
            }
            Console.ReadLine();

        }

        public static async Task<List<VoiceProfile>> IdentificationEnroll(SpeechConfig config, List<string> profileNames, Dictionary<string, string> profileMapping)
        {
            List<VoiceProfile> voiceProfiles = new List<VoiceProfile>();
            using (var client = new VoiceProfileClient(config))
            {
                var phraseResult = await client.GetActivationPhrasesAsync(VoiceProfileType.TextIndependentVerification, "en-us");
                foreach (string name in profileNames)
                {
                    using (var audioInput = AudioConfig.FromDefaultMicrophoneInput())
                    {
                        var profile = await client.CreateProfileAsync(VoiceProfileType.TextIndependentIdentification, "en-us");
                        Console.WriteLine($"Creating voice profile for {name}.");
                        profileMapping.Add(profile.Id, name);

                        VoiceProfileEnrollmentResult result = null;
                        while (result is null || result.RemainingEnrollmentsSpeechLength > TimeSpan.Zero)
                        {
                            Console.WriteLine($"Speak the activation phrase, \"${phraseResult.Phrases[0]}\" to add to the profile enrollment sample for {name}.");
                            result = await client.EnrollProfileAsync(profile, audioInput);
                            Console.WriteLine($"Remaining enrollment audio time needed: {result.RemainingEnrollmentsSpeechLength}");
                            Console.WriteLine("");
                        }
                        voiceProfiles.Add(profile);
                    }
                }
            }
            return voiceProfiles;
        }

        public static async Task SpeakerIdentification(SpeechConfig config, List<VoiceProfile> voiceProfiles, Dictionary<string, string> profileMapping)
        {
            var speakerRecognizer = new SpeakerRecognizer(config, AudioConfig.FromDefaultMicrophoneInput());
            var model = SpeakerIdentificationModel.FromProfiles(voiceProfiles);

            Console.WriteLine("Speak some text to identify who it is from your list of enrolled speakers.");
            var result = await speakerRecognizer.RecognizeOnceAsync(model);
            Console.WriteLine($"The most similar voice profile is {profileMapping[result.ProfileId]} with similarity score {result.Score}");
        }

    }
}
