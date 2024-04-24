[Home](README.md)


* Endpoint: https://aidemo2000001.cognitiveservices.azure.com/text/analytics/v3.2-preview.2/sentiment?stringIndexType=TextElement_v8
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
			"id": "1",
			"language": "en",
			"text": "Great atmosphere. Close to plenty of restaurants, hotels, and transit! Staff are friendly and helpful."
		}
	]
}
```
Response
```json
{
  "documents": [{
    "id": "1",
    "sentiment": "positive",
    "confidenceScores": {
      "positive": 0.99,
      "neutral": 0.01,
      "negative": 0.0
    },
    "sentences": [{
      "sentiment": "positive",
      "confidenceScores": {
        "positive": 0.99,
        "neutral": 0.01,
        "negative": 0.0
      },
      "offset": 0,
      "length": 18,
      "text": "Great atmosphere. "
    }, {
      "sentiment": "neutral",
      "confidenceScores": {
        "positive": 0.42,
        "neutral": 0.55,
        "negative": 0.04
      },
      "offset": 18,
      "length": 53,
      "text": "Close to plenty of restaurants, hotels, and transit! "
    }, {
      "sentiment": "positive",
      "confidenceScores": {
        "positive": 0.99,
        "neutral": 0.0,
        "negative": 0.0
      },
      "offset": 71,
      "length": 31,
      "text": "Staff are friendly and helpful."
    }],
    "warnings": []
  }],
  "errors": [],
  "modelVersion": "2022-11-01"
}
```
