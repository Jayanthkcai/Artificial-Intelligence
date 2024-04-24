using System;
using Microsoft.Extensions.Configuration;
using Azure;
using Azure.AI.TextAnalytics;

namespace TextAnalysis
{
    class Program
    {
        static void Main(string[] args)
        {
            // Get config settings from AppSettings
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = builder.Build();
            string aiSvcEndpoint = configuration["AIServicesEndpoint"];
            string aiSvcKey = configuration["AIServicesKey"];

            // Authenticate Azure AI Vision client
            AzureKeyCredential credentials = new AzureKeyCredential(aiSvcKey);
            Uri endpoint = new Uri(aiSvcEndpoint);
            TextAnalyticsClient aiClient = new TextAnalyticsClient(endpoint, credentials);

            Console.WriteLine("Enter a Text for Analysis:");
            string text = Console.ReadLine();

            // Get language
            DetectedLanguage detectedLanguage = aiClient.DetectLanguage(text);
            Console.WriteLine($"\nLanguage: {detectedLanguage.Name}");


            // Get sentiment
            DocumentSentiment sentimentAnalysis = aiClient.AnalyzeSentiment(text);
            Console.WriteLine($"\nSentiment: {sentimentAnalysis.Sentiment}");

            // Get key phrases
            KeyPhraseCollection phrases = aiClient.ExtractKeyPhrases(text);
            if (phrases.Count > 0)
            {
                Console.WriteLine("\nKey Phrases:");
                foreach (string phrase in phrases)
                {
                    Console.WriteLine($"\t{phrase}");
                }
            }


            // Get entities
            CategorizedEntityCollection entities = aiClient.RecognizeEntities(text);
            if (entities.Count > 0)
            {
                Console.WriteLine("\nEntities:");
                foreach (CategorizedEntity entity in entities)
                {
                    Console.WriteLine($"\t{entity.Text} ({entity.Category})");
                }
            }

            // Get linked entities
            LinkedEntityCollection linkedEntities = aiClient.RecognizeLinkedEntities(text);
            if (linkedEntities.Count > 0)
            {
                Console.WriteLine("\nLinks:");
                foreach (LinkedEntity linkedEntity in linkedEntities)
                {
                    Console.WriteLine($"\t{linkedEntity.Name} ({linkedEntity.Url})");
                }
            }
        }
    }
}
