﻿using System;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Azure;
using System.Threading.Tasks;

namespace CustomModel
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Get configuration settings from AppSettings
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string endpoint = configuration["DocIntelligenceEndpoint"];
            string apiKey = configuration["DocIntelligenceKey"];
            AzureKeyCredential credential = new AzureKeyCredential(apiKey);
            DocumentAnalysisClient client = new DocumentAnalysisClient(new Uri(endpoint), credential);

            string modelId = configuration["ModelId"];
            Uri fileUri = new Uri("https://raw.githubusercontent.com/MicrosoftLearning/mslearn-ai-document-intelligence/main/Labfiles/02-custom-document-intelligence/test1.jpg");
            Console.WriteLine($"Analyzing document from Uri: {fileUri.AbsoluteUri}");

            AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, modelId, fileUri);
            AnalyzeResult result = operation.Value;

            Console.WriteLine($"Document was analyzed with model with ID: {result.ModelId}");

            foreach (AnalyzedDocument document in result.Documents)
            {
                Console.WriteLine($"Document of type: {document.DocumentType}");

                foreach (KeyValuePair<string, DocumentField> fieldKvp in document.Fields)
                {
                    string fieldName = fieldKvp.Key;
                    DocumentField field = fieldKvp.Value;

                    Console.WriteLine($"Field '{fieldName}': ");

                    Console.WriteLine($"  Content: '{field.Content}'");
                    Console.WriteLine($"  Confidence: '{field.Confidence}'");
                }


            }
        }
    }

}