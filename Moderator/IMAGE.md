[**Home**](README.md)

Content Moderator for the image performs Image [Evaluation](IMAGE.md), [Find Faces](FINDFACES.md), [Match](MATCH.md) and [OCR](OCR.md)

Evaluate image to return probabilities of containing racy or adult content.

* Endpoint: https://aidemo100001.cognitiveservices.azure.com/contentmoderator/moderate/v1.0/ProcessImage/Evaluate
    aidemo100001 is the name of the Content Moderator resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo100001
* Method : POST
* Body : raw 

Request
```json
{
  "DataRepresentation":"URL",
  "Value":"https://moderatorsampleimages.blob.core.windows.net/samples/sample.jpg"
}
```
Response
```json
{
    "AdultClassificationScore": 0.02533765695989132,
    "IsImageAdultClassified": false,
    "RacyClassificationScore": 0.05301360599696636,
    "IsImageRacyClassified": false,
    "Result": false,
    "AdvancedInfo": [
        {
            "Key": "ImageDownloadTimeInMs",
            "Value": "683"
        },
        {
            "Key": "ImageSizeInBytes",
            "Value": "273405"
        }
    ],
    "Status": {
        "Code": 3000,
        "Description": "OK",
        "Exception": null
    },
    "TrackingId": "540db2ad-160b-428c-a729-ac16bb4530e7"
}
```

Instead of image URL in the request, image can be passed in Base64 format
```json
{
  "DataRepresentation": "Base64",
  "Value": "<Basse66 string>"
}
```

