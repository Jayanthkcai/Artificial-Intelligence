using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

// Import namespaces
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace AnalyzeFace
{
    class Program
    {
        private static ComputerVisionClient cvClient;
        private static FaceClient faceClient;
        static async Task Main(string[] args)
        {
            try
            {
                // Get config settings from AppSettings
                IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
                IConfigurationRoot configuration = builder.Build();
                string cogSvcEndpoint = configuration["AIServicesEndpoint"];
                string cogSvcKey = configuration["AIServiceKey"];

                // Authenticate Face client
                Microsoft.Azure.CognitiveServices.Vision.ComputerVision.ApiKeyServiceClientCredentials credentials = new Microsoft.Azure.CognitiveServices.Vision.ComputerVision.ApiKeyServiceClientCredentials(cogSvcKey);
                cvClient = new ComputerVisionClient(credentials)
                {
                    Endpoint = cogSvcEndpoint
                };
                faceClient = new FaceClient(credentials)
                {
                    Endpoint = cogSvcEndpoint
                };
                // Menu for face functions
                Console.WriteLine("1: Detect faces\n2: Analyze images\nAny other key to quit");
                Console.WriteLine("Enter a number:");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "1":
                        await DetectFaces("../../../images/people.jpg");
                        break;
                    case "2":
                        await AnalyzeImage("../../../images/people.jpg");
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async Task DetectFaces(string imageFile)
        {
            Console.WriteLine($"Detecting faces in {imageFile}");

            // Specify features to be retrieved (faces)
            List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
             {
                 VisualFeatureTypes.Faces
             };

            // Get faces
            using (var imageData = File.OpenRead(imageFile))
            {
                var analysis = await cvClient.AnalyzeImageInStreamAsync(imageData, features);

                // Get faces
                if (analysis.Faces.Count > 0)
                {
                    Console.WriteLine($"{analysis.Faces.Count} faces detected.");

                    // Prepare image for drawing
                    Image image = Image.FromFile(imageFile);
                    Graphics graphics = Graphics.FromImage(image);
                    Pen pen = new Pen(Color.LightGreen, 3);
                    Font font = new Font("Arial", 3);
                    SolidBrush brush = new SolidBrush(Color.LightGreen);

                    // Draw and annotate each face
                    foreach (var face in analysis.Faces)
                    {
                        var r = face.FaceRectangle;
                        Rectangle rect = new Rectangle(r.Left, r.Top, r.Width, r.Height);
                        graphics.DrawRectangle(pen, rect);
                        string annotation = $"Person at approximately {r.Left}, {r.Top}";
                        graphics.DrawString(annotation, font, brush, r.Left, r.Top);
                    }

                    // Save annotated image
                    String output_file = @"../../../images/detected_faces.jpg";
                    image.Save(output_file);
                    Console.WriteLine(" Results saved in " + output_file);
                }
            }


        }

        static async Task AnalyzeImage(string imageFile)
        {
            Console.WriteLine($"\nAnalyzing {imageFile} \n");

            // Specify facial features to be retrieved
            IList<FaceAttributeType> features = new List<FaceAttributeType>
             {
                 FaceAttributeType.Occlusion,
                 FaceAttributeType.Blur,
                 FaceAttributeType.Glasses
             };

            // Get faces
            using (var imageData = File.OpenRead(imageFile))
            {
                var detected_faces = await faceClient.Face.DetectWithStreamAsync(imageData, returnFaceAttributes: features, returnFaceId: false);

                if (detected_faces.Count > 0)
                {
                    Console.WriteLine($"{detected_faces.Count} faces detected.");

                    // Prepare image for drawing
                    Image image = Image.FromFile(imageFile);
                    Graphics graphics = Graphics.FromImage(image);
                    Pen pen = new Pen(Color.LightGreen, 3);
                    Font font = new Font("Arial", 4);
                    SolidBrush brush = new SolidBrush(Color.Black);
                    int faceCount = 0;

                    // Draw and annotate each face
                    foreach (var face in detected_faces)
                    {
                        faceCount++;
                        Console.WriteLine($"\nFace number {faceCount}");

                        // Get face properties
                        Console.WriteLine($" - Mouth Occluded: {face.FaceAttributes.Occlusion.MouthOccluded}");
                        Console.WriteLine($" - Eye Occluded: {face.FaceAttributes.Occlusion.EyeOccluded}");
                        Console.WriteLine($" - Blur: {face.FaceAttributes.Blur.BlurLevel}");
                        Console.WriteLine($" - Glasses: {face.FaceAttributes.Glasses}");

                        // Draw and annotate face
                        var r = face.FaceRectangle;
                        Rectangle rect = new Rectangle(r.Left, r.Top, r.Width, r.Height);
                        graphics.DrawRectangle(pen, rect);
                        string annotation = $"Face ID: {face.FaceId}";
                        graphics.DrawString(annotation, font, brush, r.Left, r.Top);
                    }

                    // Save annotated image
                    String output_file = @"../../../images/image_analysis-1.jpg";
                    image.Save(output_file);
                    Console.WriteLine(" Results saved in " + output_file);
                }
            }
        }

    }
}
