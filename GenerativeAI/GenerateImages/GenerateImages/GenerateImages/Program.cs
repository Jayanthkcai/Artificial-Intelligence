using System;
using Azure.AI.OpenAI;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Azure;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Nodes;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GenerateImages
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
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

                // Get prompt for image to be generated
                Console.Clear();
                Console.WriteLine("Enter a prompt to request an image:");
                string prompt = Console.ReadLine() ?? "";

                // Call the DALL-E model
                using (var client = new HttpClient())
                {
                    var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                    var api = "openai/deployments/dalle3/images/generations?api-version=2024-02-15-preview";
                    client.BaseAddress = new Uri(oaiEndpoint);
                    client.DefaultRequestHeaders.Accept.Add(contentType);
                    client.DefaultRequestHeaders.Add("api-key", oaiKey);
                    var data = new
                    {
                        prompt = prompt,
                        n = 1,
                        size = "1024x1024"
                    };

                    var jsonData = JsonSerializer.Serialize(data);
                    var contentData = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(api, contentData);

                    // Get the revised prompt and image URL from the response
                    var stringResponse = await response.Content.ReadAsStringAsync();
                    JsonNode contentNode = JsonNode.Parse(stringResponse)!;
                    JsonNode dataCollectionNode = contentNode!["data"];
                    JsonNode dataNode = dataCollectionNode[0]!;
                    JsonNode revisedPrompt = dataNode!["revised_prompt"];
                    JsonNode url = dataNode!["url"];
                    Console.WriteLine(revisedPrompt.ToJsonString());
                    Console.WriteLine(url.ToJsonString().Replace(@"\u0026", "&"));

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
