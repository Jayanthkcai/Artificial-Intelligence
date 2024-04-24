[Home](README.md)

* Endpoint: https://aidemo2000001.cognitiveservices.azure.com/text/analytics/v3.2-preview.2/entities/recognition/general?stringIndexType=TextElement_v8
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
      "text": "trip",
      "category": "Event",
      "offset": 18,
      "length": 4,
      "confidenceScore": 0.82
    }, {
      "text": "Seattle",
      "category": "Location",
      "subcategory": "City",
      "offset": 26,
      "length": 7,
      "confidenceScore": 1.0
    }, {
      "text": "last week",
      "category": "DateTime",
      "subcategory": "DateRange",
      "offset": 34,
      "length": 9,
      "confidenceScore": 1.0
    }],
    "warnings": []
  }, {
    "id": "2",
    "entities": [{
      "text": "Microsoft",
      "category": "Organization",
      "offset": 10,
      "length": 9,
      "confidenceScore": 1.0
    }],
    "warnings": []
  }, {
    "id": "3",
    "entities": [{
      "text": "Space Needle",
      "category": "Location",
      "offset": 10,
      "length": 12,
      "confidenceScore": 0.92
    }, {
      "text": "2",
      "category": "Quantity",
      "subcategory": "Number",
      "offset": 23,
      "length": 1,
      "confidenceScore": 0.8
    }],
    "warnings": []
  }],
  "errors": [],
  "modelVersion": "2023-09-01"
}
```