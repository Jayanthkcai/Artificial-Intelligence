using System;
using Azure.AI.OpenAI;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Azure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;

namespace PromptEng
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Build a config object and retrieve user settings.
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            string? oaiEndpoint = config["AzureOAIEndpoint"];
            string? oaiKey = config["AzureOAIKey"];
            string? oaiDeploymentName = config["AzureOAIDeploymentName"];

            if (string.IsNullOrEmpty(oaiEndpoint) || string.IsNullOrEmpty(oaiKey) || string.IsNullOrEmpty(oaiDeploymentName))
            {
                Console.WriteLine("Please check your appsettings.json file for missing or incorrect values.");
                return;

            }

            // Initialize the Azure OpenAI client...
            OpenAIClient client = new OpenAIClient(new Uri(oaiEndpoint), new AzureKeyCredential(oaiKey));

            do
            {
                Console.WriteLine("Enter your System Message (or type 'quit' to exit): ");
                string? systemMessage = Console.ReadLine();
                if (systemMessage == "quit") break;

                // Generate summary from Azure OpenAI
                if (systemMessage == null)
                {
                    Console.WriteLine("Please enter a System Message.");
                    continue;
                }

                Console.WriteLine("Enter your User Message (or type 'quit' to exit): ");
                string? userMessage = Console.ReadLine();
                if (userMessage == "quit") break;

                // Generate summary from Azure OpenAI
                if (userMessage == null)
                {
                    Console.WriteLine("Please enter a User Message.");
                    continue;
                }
                Console.WriteLine("\nAdding grounding context from grounding.txt");
                var currentFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                string groundingText = System.IO.File.ReadAllText("../../../grounding.txt");
                userMessage = groundingText + userMessage;

                // Format and send the request to the model
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    Messages =
                     {
                         new ChatRequestSystemMessage(systemMessage),
                         new ChatRequestUserMessage(userMessage)
                     },
                    Temperature = 0.7f,
                    MaxTokens = 800,
                    DeploymentName = oaiDeploymentName
                };

                // Get response from Azure OpenAI
                Response<ChatCompletions> response = await client.GetChatCompletionsAsync(chatCompletionsOptions);
                // Print the response
                string completion = response.Value.Choices[0].Message.Content;
                Console.WriteLine("Response: " + completion + "\n");


            } while (true);
        }
    }
}
