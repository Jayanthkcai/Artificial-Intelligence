using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Azure.AI.Vision.ImageAnalysis;
using Azure;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Drawing;
using System.Text;

namespace Analyze
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Get config settings from AppSettings
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                IConfigurationRoot configuration = builder.Build();
                string aiSvcEndpoint = configuration["AIServicesEndpoint"];
                string aiSvcKey = configuration["AIServicesKey"];

                // Get image
                string imageFile = "../../../images/street.jpg";
                if (args.Length > 0)
                {
                    imageFile = args[0];
                }

                // Authenticate Azure AI Vision client
                ImageAnalysisClient client = new ImageAnalysisClient(
                new Uri(aiSvcEndpoint),
                new AzureKeyCredential(aiSvcKey));

                // Analyze image
                AnalyzeImage(imageFile, client);

                // Remove the background or generate a foreground matte from the image
                await BackgroundForeground(imageFile, aiSvcEndpoint, aiSvcKey);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void AnalyzeImage(string imageFile, ImageAnalysisClient client)
        {
            Console.WriteLine($"\nAnalyzing {imageFile} \n");

            // Use a file stream to pass the image data to the analyze call
            using FileStream stream = new FileStream(imageFile,
                                                     FileMode.Open);

            // Get result with specified features to be retrieved
            ImageAnalysisResult result = client.Analyze(
            BinaryData.FromStream(stream),
            VisualFeatures.Caption |
            VisualFeatures.DenseCaptions |
            VisualFeatures.Objects |
            VisualFeatures.Tags |
            VisualFeatures.People |
            VisualFeatures.Read);

            // Display analysis results
            DisplayAnalysisResults(result);

            // Get image dense captions
            GetImageDenseCaptions(result);

            // Get image tags
            GetImageTags(result);

            // Get objects in the image
           // GetObjectsInImage(result, imageFile, stream);

            // Get people in the image
           // GetPeopleInImage(result, imageFile);
        }

        static void GetPeopleInImage(ImageAnalysisResult result, string imageFile)
        {
            // Get people in the image
            if (result.People.Values.Count > 0)
            {
                Console.WriteLine($" People:");

                // Prepare image for drawing
                Image image = Image.FromFile(imageFile);
                Graphics graphics = Graphics.FromImage(image);
                Pen pen = new Pen(Color.Cyan, 3);
                Font font = new Font("Arial", 16);
                SolidBrush brush = new SolidBrush(Color.WhiteSmoke);

                foreach (DetectedPerson person in result.People.Values)
                {
                    // Draw object bounding box
                    var r = person.BoundingBox;
                    Rectangle rect = new Rectangle(r.X, r.Y, r.Width, r.Height);
                    graphics.DrawRectangle(pen, rect);

                    // Return the confidence of the person detected
                    Console.WriteLine($"   Bounding box {person.BoundingBox.ToString()}, Confidence: {person.Confidence:F2}");
                }

                // Save annotated image
                String output_file = "../../../images/persons.jpg";
                image.Save(output_file);
                Console.WriteLine("  Results saved in " + output_file + "\n");
            }
        }

        static void GetObjectsInImage(ImageAnalysisResult result, string imageFile, FileStream stream)
        {
            // Get objects in the image
            if (result.Objects.Values.Count > 0)
            {
                Console.WriteLine(" Objects:");

                // Prepare image for drawing
                stream.Close();
                Image image = Image.FromFile(imageFile);
                Graphics graphics = Graphics.FromImage(image);
                Pen pen = new Pen(Color.Cyan, 3);
                Font font = new Font("Arial", 16);
                SolidBrush brush = new SolidBrush(Color.WhiteSmoke);

                foreach (DetectedObject detectedObject in result.Objects.Values)
                {
                    Console.WriteLine($"   \"{detectedObject.Tags[0].Name}\"");

                    // Draw object bounding box
                    var r = detectedObject.BoundingBox;
                    Rectangle rect = new Rectangle(r.X, r.Y, r.Width, r.Height);
                    graphics.DrawRectangle(pen, rect);
                    graphics.DrawString(detectedObject.Tags[0].Name, font, brush, (float)r.X, (float)r.Y);
                }

                // Save annotated image
                String output_file = "objects.jpg";
                image.Save(output_file);
                Console.WriteLine("  Results saved in " + output_file + "\n");
            }
        }

        static void GetImageTags(ImageAnalysisResult result)
        {
            // Get image tags
            if (result.Tags.Values.Count > 0)
            {
                Console.WriteLine($"\n Tags:");
                foreach (DetectedTag tag in result.Tags.Values)
                {
                    Console.WriteLine($"   '{tag.Name}', Confidence: {tag.Confidence:F2}");
                }
            }
        }
        static void DisplayAnalysisResults(ImageAnalysisResult result)
        {
            // Display analysis results
            if (result.Caption.Text != null)
            {
                Console.WriteLine(" Caption:");
                Console.WriteLine($"   \"{result.Caption.Text}\", Confidence {result.Caption.Confidence:0.00}\n");
            }
        }

        static void GetImageDenseCaptions(ImageAnalysisResult result)
        {
            // Get image dense captions
            Console.WriteLine(" Dense Captions:");
            foreach (DenseCaption denseCaption in result.DenseCaptions.Values)
            {
                Console.WriteLine($"   Caption: '{denseCaption.Text}', Confidence: {denseCaption.Confidence:0.00}");
            }
        }
        static async Task BackgroundForeground(string imageFile, string endpoint, string key)
        {
            // Remove the background from the image or generate a foreground matte
            Console.WriteLine($" Background removal:");
            // Define the API version and mode
            string apiVersion = "2023-02-01-preview";
            string mode = "backgroundRemoval"; // Can be "foregroundMatting" or "backgroundRemoval"

            string url = $"computervision/imageanalysis:segment?api-version={apiVersion}&mode={mode}";

            // Make the REST call
            using (var client = new HttpClient())
            {
                var contentType = new MediaTypeWithQualityHeaderValue("application/json");
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);

                // You can change the url to use other images in the images folder,
                // such as "building.jpg" or "person.jpg" to see different results.
                var data = new
                {
                    url = "https://github.com/MicrosoftLearning/mslearn-ai-vision/blob/main/Labfiles/01-analyze-images/Python/image-analysis/images/street.jpg?raw=true"
                };

                var jsonData = JsonSerializer.Serialize(data);
                var contentData = new StringContent(jsonData, Encoding.UTF8, contentType.MediaType);
                var response = await client.PostAsync(url, contentData);

                if (response.IsSuccessStatusCode)
                {
                    File.WriteAllBytes("background.png", response.Content.ReadAsByteArrayAsync().Result);
                    Console.WriteLine("  Results saved in background.png\n");
                }
                else
                {
                    Console.WriteLine($"API error: {response.ReasonPhrase} - Check your body url, key, and endpoint.");
                }
            }
        }

    }
}
