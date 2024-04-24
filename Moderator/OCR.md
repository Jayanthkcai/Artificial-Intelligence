[**Home**](README.md)

Content Moderator for the image performs Image [Evaluation](IMAGE.md), [Find Faces](FINDFACES.md), [Match](MATCH.md) and [OCR](OCR.md)

OCR returns any text found in the image for the language specified. If no language is specified in input then the detection defaults to English.

Extract text from images and pass the extracted text to Azure Content Moderator for text moderation will allow to analyze and moderate the extracted text.By combining OCR with Azure Content Moderator, we can create a robust content moderation solution that addresses both text and images effectively.

* Endpoint: https://aidemo100001.cognitiveservices.azure.com/contentmoderator/moderate/v1.0/ProcessImage/OCR?language=eng&CacheImage=false&enhanced=false
    aidemo100001 is the name of the Content Moderator resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo100001
* Method : POST
* Body : raw 

Request
```json
{
  "DataRepresentation":"URL",
  "Value":"https://support.foxtrotalliance.com/hc/article_attachments/360023939452/test_1.png"
}
```
Response
```json
{
  "Metadata": [{
    "Key": "ImageDownloadTimeInMs",
    "Value": "575"
  }, {
    "Key": "ImageSizeInBytes",
    "Value": "15079"
  }],
  "Language": "",
  "Text": "This is the first line of \nthis text example. \nThis is the second line \nof the same text. \n",
  "Candidates": [],
  "Status": {
    "Code": 3000,
    "Description": "OK",
    "Exception": null
  },
  "TrackingId": "5a991314-6abb-4100-995c-bbceb99fdafb"
}
```

