using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech.Translation;
using Microsoft.Extensions.Configuration;

namespace SpeechTranslation
{
    class Program
    {
        private static SpeechConfig speechConfig;
        private static SpeechTranslationConfig translationConfig;
        static async Task Main(string[] args)
        {
            try
            {
                // Get config settings from AppSettings
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                IConfigurationRoot configuration = builder.Build();
                string aiSvcKey = configuration["SpeechKey"];
                string aiSvcRegion = configuration["SpeechRegion"];

                // Set console encoding to unicode
                Console.InputEncoding = Encoding.Unicode;
                Console.OutputEncoding = Encoding.Unicode;

                // Configure translation
                translationConfig = SpeechTranslationConfig.FromSubscription(aiSvcKey, aiSvcRegion);
                translationConfig.SpeechRecognitionLanguage = "en-US";
                translationConfig.AddTargetLanguage("fr");
                translationConfig.AddTargetLanguage("es");
                translationConfig.AddTargetLanguage("hi");
                Console.WriteLine("Ready to translate from " + translationConfig.SpeechRecognitionLanguage);

                // Configure speech
                speechConfig = SpeechConfig.FromSubscription(aiSvcKey, aiSvcRegion);

                string targetLanguage = "";
                while (targetLanguage != "quit")
                {
                    Console.WriteLine("\nEnter a target language\n fr = French\n es = Spanish\n hi = Hindi\n Enter anything else to stop\n");
                    targetLanguage = Console.ReadLine().ToLower();
                    if (translationConfig.TargetLanguages.Contains(targetLanguage))
                    {
                        await Translate(targetLanguage);
                    }
                    else
                    {
                        targetLanguage = "quit";
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
     
        static async Task Translate(string targetLanguage)
        {
            string translation = "";

            // Translate speech
            using AudioConfig audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using TranslationRecognizer translator = new TranslationRecognizer(translationConfig, audioConfig);

            // Subscribe to the Synthesizing event
            //Verify if there are any limitations or constraints with the Azure Speech service that might prevent the event from triggering.
            translator.Synthesizing += (s, e) =>
            {
                Console.WriteLine($"Translation synthesis in progress...");
                // Get the translated audio stream
                var audioStream = e.Result.GetAudio();

                // Save the audio stream to a file
                string filePath = "../../../translated_audio.wav";
                using (MemoryStream ms = new MemoryStream(audioStream))
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        ms.CopyTo(fs);
                    }
                }

                Console.WriteLine($"Translated audio saved to: {filePath}");
            };


            Console.WriteLine("Speak now...");
            TranslationRecognitionResult result = await translator.RecognizeOnceAsync();
            // Check the result
            if (result.Reason == ResultReason.TranslatedSpeech)
            {
                Console.WriteLine($"Translating '{result.Text}'");
                translation = result.Translations[targetLanguage];
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine(translation);
            }

            // Translate speech
            string audioFile = "../../../station.wav";
            SoundPlayer wavPlayer = new SoundPlayer(audioFile);
            wavPlayer.Play();
            using AudioConfig audioConfig1 = AudioConfig.FromWavFileInput(audioFile);
            using TranslationRecognizer translator1 = new TranslationRecognizer(translationConfig, audioConfig1);
            Console.WriteLine("Getting speech from file...");
            TranslationRecognitionResult result1 = await translator1.RecognizeOnceAsync();
            Console.WriteLine($"Translating '{result1.Text}'");
            translation = result1.Translations[targetLanguage];
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine(translation);

            // Synthesize translation
            var voices = new Dictionary<string, string>
            {
                ["fr"] = "fr-FR-HenriNeural",
                ["es"] = "es-ES-ElviraNeural",
                ["hi"] = "hi-IN-MadhurNeural"
            };
            speechConfig.SpeechSynthesisVoiceName = voices[targetLanguage];
            using SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer(speechConfig);
            SpeechSynthesisResult speak = await speechSynthesizer.SpeakTextAsync(translation);
            if (speak.Reason != ResultReason.SynthesizingAudioCompleted)
            {
                Console.WriteLine(speak.Reason);
            }

        }

    }
}
