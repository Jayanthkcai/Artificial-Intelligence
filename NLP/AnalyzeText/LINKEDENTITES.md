[Home](README.md)


* Endpoint: https://aidemo2000001.cognitiveservices.azure.com/text/analytics/v3.2-preview.2/entities/linking?stringIndexType=TextElement_v8
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
      "text": "I had a wonderful trip to Seattle last week."
    },
    {
      "language": "en",
      "id": "2",
      "text": "I work at Microsoft."
    },
    {
      "language": "en",
      "id": "3",
      "text": "I visited Space Needle 2 times."
    },
    {
      "language": "en",
      "id": "4",
      "text": "Venus a star in the night sky."
    },
    {
      "language": "en",
      "id": "5",
      "text": "Venus mentioned in a mythology."
    }
  ]
}
```
Response
```json
{
  "documents": [{
    "id": "1",
    "entities": [{
      "bingId": "5fbba6b8-85e1-4d41-9444-d9055436e473",
      "name": "Seattle",
      "matches": [{
        "text": "Seattle",
        "offset": 26,
        "length": 7,
        "confidenceScore": 0.19
      }],
      "language": "en",
      "id": "Seattle",
      "url": "https://en.wikipedia.org/wiki/Seattle",
      "dataSource": "Wikipedia"
    }],
    "warnings": []
  }, {
    "id": "2",
    "entities": [{
      "bingId": "a093e9b9-90f5-a3d5-c4b8-5855e1b01f85",
      "name": "Microsoft",
      "matches": [{
        "text": "Microsoft",
        "offset": 10,
        "length": 9,
        "confidenceScore": 0.25
      }],
      "language": "en",
      "id": "Microsoft",
      "url": "https://en.wikipedia.org/wiki/Microsoft",
      "dataSource": "Wikipedia"
    }],
    "warnings": []
  }, {
    "id": "3",
    "entities": [{
      "bingId": "f8dd5b08-206d-2554-6e4a-893f51f4de7e",
      "name": "Space Needle",
      "matches": [{
        "text": "Space Needle",
        "offset": 10,
        "length": 12,
        "confidenceScore": 0.17
      }],
      "language": "en",
      "id": "Space Needle",
      "url": "https://en.wikipedia.org/wiki/Space_Needle",
      "dataSource": "Wikipedia"
    }],
    "warnings": []
  }, {
    "id": "4",
    "entities": [{
      "bingId": "89253af3-5b63-e620-9227-f839138139f6",
      "name": "Venus",
      "matches": [{
        "text": "Venus",
        "offset": 0,
        "length": 5,
        "confidenceScore": 0.09
      }],
      "language": "en",
      "id": "Venus",
      "url": "https://en.wikipedia.org/wiki/Venus",
      "dataSource": "Wikipedia"
    }],
    "warnings": []
  }, {
    "id": "5",
    "entities": [{
      "bingId": "e010988d-5ea4-9c84-1567-35896cf31db2",
      "name": "Venus (mythology)",
      "matches": [{
        "text": "Venus",
        "offset": 0,
        "length": 5,
        "confidenceScore": 0.17
      }],
      "language": "en",
      "id": "Venus (mythology)",
      "url": "https://en.wikipedia.org/wiki/Venus_(mythology)",
      "dataSource": "Wikipedia"
    }],
    "warnings": []
  }],
  "errors": [],
  "modelVersion": "2021-06-01"
}
```