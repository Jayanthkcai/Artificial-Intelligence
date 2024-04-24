[Home](README.md)

* Endpoint: https://aidemo2000001.cognitiveservices.azure.com/text/analytics/v3.2-preview.2/keyPhrases
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
      "language": "en",
      "id": "1",
      "text": "Hello world. This is some input text that I love."
    },
    {
      "language": "fr",
      "id": "2",
      "text": "Bonjour tout le monde"
    },
    {
      "language": "es",
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
    "keyPhrases": ["Hello world", "input text"],
    "warnings": []
  }, {
    "id": "2",
    "keyPhrases": ["Bonjour", "monde"],
    "warnings": []
  }, {
    "id": "3",
    "keyPhrases": ["mucho tráfico", "día", "carretera", "ayer"],
    "warnings": []
  }],
  "errors": [],
  "modelVersion": "2022-10-01"
}
```