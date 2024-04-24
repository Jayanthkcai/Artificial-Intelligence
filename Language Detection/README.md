To get acquainted with the standard procedure for setting up and utilizing Azure AI services as a developer.

One should have Azure Subscription in the Azure portal https://portal.azure.com

1. Open Azure Portal

2. Click on All Services in the left pane

3. Click on AI + MAchine Learning in the Categories section of the right pane

4. Click Azure AI services multi-service account and create the service.

5. Go to the resource created and view the "Endpoint" and "Manage Keys" OR Visit "Keys and Endpoint" in the Resource Management section of the resource left pane.

6. Download the Code form "01 - AI Service - Language Detection/LanguageDetection" and add ensure the package Azure.AI.TextAnalytics version 5.3.0 is referenced to the project.

7. Copy the Endpoint and Key1 from "Keys and Endpoint" of the reources and update in the appsettings.json of the project

8. Code in the Main function retrieves the endpoint and key for Azure AI services multi-service resource from the appsettings.json. Sends REST requests to the Text Analytics service.

9. Program accepts user input, and uses the GetLanguage function to call the Text Analytics language detection REST API for your Azure AI services endpoint to detect the language of the text that was entered.

10. Request input data will have a a collection of document objects in JSON format, each of which has an unique id and text.

11. Key of the service included in the request header will authenticate the client application.

12. Response from the service is a JSON object, which the client application can parse.


```json
Input :
Enter some text ('quit' to stop)
This is for AI-102

Out Put : 
{
  "documents": [
    {
      "id": 1,
      "text": "This is for AI-102"
    }
  ]
}
{
  "documents": [
    {
      "id": "1",
      "detectedLanguage": {
        "name": "English",
        "iso6391Name": "en",
        "confidenceScore": 1.0
      },
      "warnings": []
    }
  ],
  "errors": [],
  "modelVersion": "2023-12-01"
}

<b>Language : French</b>
Enter some text ('quit' to stop)
C’est pour AI-102
{
  "documents": [
    {
      "id": 1,
      "text": "C'est pour AI-102"
    }
  ]
}
{
  "documents": [
    {
      "id": "1",
      "detectedLanguage": {
        "name": "French",
        "iso6391Name": "fr",
        "confidenceScore": 1.0
      },
      "warnings": []
    }
  ],
  "errors": [],
  "modelVersion": "2023-12-01"
}

<b>Language : Portuguese</b>
Enter some text ('quit' to stop)
Isto é para AI-102
{
  "documents": [
    {
      "id": 1,
      "text": "Isto é para AI-102"
    }
  ]
}
{
  "documents": [
    {
      "id": "1",
      "detectedLanguage": {
        "name": "Portuguese",
        "iso6391Name": "pt",
        "confidenceScore": 1.0
      },
      "warnings": []
    }
  ],
  "errors": [],
  "modelVersion": "2023-12-01"
}
```