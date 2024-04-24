[Home](README.md)


* Endpoint: https://aidemo2000001.cognitiveservices.azure.com/text/analytics/v3.2-preview.2/languages
    aidemo2000001 is the name of the Language resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo2000001
* Method : POST
* Body : raw 

Request
```json
{
  "documents": [
    {
      "countryHint": "US",
      "id": "1",
      "text": "Hello world"
    },
    {
      "id": "2",
      "text": "Bonjour tout le monde"
    },
    {
      "id": "3",
      "text": "La carretera estaba atascada. Había mucho tráfico el día de ayer."
    }
  ]
}
```

Response
```json
{
  "documents": [{
    "id": "1",
    "detectedLanguage": {
      "name": "English",
      "iso6391Name": "en",
      "confidenceScore": 0.98
    },
    "warnings": []
  }, {
    "id": "2",
    "detectedLanguage": {
      "name": "French",
      "iso6391Name": "fr",
      "confidenceScore": 1.0
    },
    "warnings": []
  }, {
    "id": "3",
    "detectedLanguage": {
      "name": "Spanish",
      "iso6391Name": "es",
      "confidenceScore": 1.0
    },
    "warnings": []
  }],
  "errors": [],
  "modelVersion": "2023-12-01"
}
```