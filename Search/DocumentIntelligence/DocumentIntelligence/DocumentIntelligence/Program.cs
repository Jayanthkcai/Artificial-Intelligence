using System;
using System.Threading.Tasks;
using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;

namespace DocumentIntelligence
{
    class Program
    {
       
      
        static async Task Main(string[] args)
        {
            string endpoint = "https://aidemodocumenint100001.cognitiveservices.azure.com/";
            string apiKey = "7f80a672744449fea7c954fa229bfd83";

            Uri fileUri = new Uri("https://store200001.blob.core.windows.net/docintel/sample-invoice (1).pdf");

            // Create the client
            var cred = new AzureKeyCredential(apiKey);
            var client = new DocumentAnalysisClient(new Uri(endpoint), cred);

            // Analyze the invoice
            AnalyzeDocumentOperation operation = await client.AnalyzeDocumentFromUriAsync(WaitUntil.Completed, "prebuilt-invoice", fileUri);
            await operation.WaitForCompletionAsync();

            // Display invoice information to the user
            AnalyzeResult result = operation.Value;

            foreach (AnalyzedDocument invoice in result.Documents)
            {
                if (invoice.Fields.TryGetValue("VendorName", out DocumentField? vendorNameField))
                {
                    if (vendorNameField.FieldType == DocumentFieldType.String)
                    {
                        string vendorName = vendorNameField.Value.AsString();
                        Console.WriteLine($"Vendor Name: '{vendorName}', with confidence {vendorNameField.Confidence}.");
                    }
                }

                if (invoice.Fields.TryGetValue("CustomerName", out DocumentField? customerNameField))
                {
                    if (customerNameField.FieldType == DocumentFieldType.String)
                    {
                        string customerName = customerNameField.Value.AsString();
                        Console.WriteLine($"Customer Name: '{customerName}', with confidence {customerNameField.Confidence}.");
                    }
                }

                if (invoice.Fields.TryGetValue("InvoiceTotal", out DocumentField? invoiceTotalField))
                {
                    if (invoiceTotalField.FieldType == DocumentFieldType.Currency)
                    {
                        CurrencyValue invoiceTotal = invoiceTotalField.Value.AsCurrency();
                        Console.WriteLine($"Invoice Total: '{invoiceTotal.Symbol}{invoiceTotal.Amount}', with confidence {invoiceTotalField.Confidence}.");
                    }
                }
            }
            Console.WriteLine("\nAnalysis complete.\n");
        }
    }
}
