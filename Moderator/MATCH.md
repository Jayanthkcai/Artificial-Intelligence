[**Home**](README.md)

Content Moderator for the image performs Image [Evaluation](IMAGE.md), [Find Faces](FINDFACES.md), [Match](MATCH.md) and [OCR](OCR.md)

Match finds a closely matching image within one of our custom Image Lists. You can create and administer your custom image lists using this [API].(https://eastus.dev.cognitive.microsoft.com/docs/services/57cf755e3f9b070c105bd2c2/operations/57cf755e3f9b070868a1f672)

The response includes the ID and tags of the matched image. There is a maximum limit of 5 image lists with each list to not exceed 10,000 images.


**Create an Image List**
* Endpoint: https://aidemo100001.cognitiveservices.azure.com/contentmoderator/lists/v1.0/imagelists
    aidemo100001 is the name of the Content Moderator resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo100001
* Method : POST
* Body : raw 

Request
```json
{
  "Name": "Swimsuits and sports",
  "Description": "Images Sample List",
  "Metadata": {
   "Key One": "Acceptable",
   "Key Two": "Potentially racy"
  },
}
```
Response
```json
{
  "Id": 1476155,
  "Name": "Swimsuits and sports",
  "Description": "Images Sample List",
  "Metadata": {
    "Key One": "Acceptable",
    "Key Two": "Potentially racy"
  }
}
```
**Add images to Image List**
* Endpoint: https://aidemo100001.cognitiveservices.azure.com/contentmoderator/lists/v1.0/imagelists/1476155/images
    aidemo100001 is the name of the Content Moderator resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo100001
* Method : POST
* Body : raw 
* listId : 1476155
* label : Sports

Request
```json
{
  "DataRepresentation":"URL",
  "Value":"https://moderatorsampleimages.blob.core.windows.net/samples/sample4.png"
}
```
Response
```json
{
  "ContentId": "1517188",
  "AdditionalInfo": [{
    "Key": "ImageDownloadTimeInMs",
    "Value": "367"
  }, {
    "Key": "ImageSizeInBytes",
    "Value": "10667"
  }, {
    "Key": "Source",
    "Value": "1476155"
  }],
  "Status": {
    "Code": 3000,
    "Description": "OK",
    "Exception": null
  },
  "TrackingId": "bee987a0-dafe-4f66-b843-007fef829a09"
}
```
**Add more images**

Label : Sports
https://moderatorsampleimages.blob.core.windows.net/samples/sample6.png
https://moderatorsampleimages.blob.core.windows.net/samples/sample9.png

Label : Swimsuit
https://moderatorsampleimages.blob.core.windows.net/samples/sample1.jpg
https://moderatorsampleimages.blob.core.windows.net/samples/sample3.png
https://moderatorsampleimages.blob.core.windows.net/samples/sample16.png
https://moderatorsampleimages.blob.core.windows.net/samples/sample4.png (Response will be conflict error message, unable to add the iamge to list)


**Match Image:**
* Endpoint: https://aidemo100001.cognitiveservices.azure.com/contentmoderator/moderate/v1.0/ProcessImage/Match?listId=1476155
    aidemo100001 is the name of the Content Moderator resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo100001
* Method : POST
* Body : raw 
* listId : 1476155

Request
```javascript
{
  "DataRepresentation":"URL",
  "Value":"https://moderatorsampleimages.blob.core.windows.net/samples/sample16.png"
}
```
Response
```json
{
  "IsMatch": true,
  "Matches": [{
    "Score": 1.0,
    "MatchId": 1517187,
    "Source": "1476155",
    "Tags": [0],
    "Label": "swimsuit"
  }],
  "Status": {
    "Code": 3000,
    "Description": "OK",
    "Exception": null
  },
  "TrackingId": "cf9fcfae-1eb7-4036-bd69-4e850e9c01d7"
}
```