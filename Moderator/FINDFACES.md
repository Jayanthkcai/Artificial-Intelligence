[**Home**](README.md)

Content Moderator for the image performs Image [Evaluation](IMAGE.md), [Find Faces](FINDFACES.md), [Match](MATCH.md) and [OCR](OCR.md)

Find Faces analyzes the provided image and identifies the locations of faces within the image.  Provides a convenient way to detect and locate faces within images, enabling to build powerful image processing and moderation workflows in our applications.


* Endpoint: https://aidemo100001.cognitiveservices.azure.com/contentmoderator/moderate/v1.0/ProcessImage/FindFaces
    aidemo100001 is the name of the Content Moderator resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo100001
* Method : POST
* Body : raw 

Request
```json
{
  "DataRepresentation":"URL",
  "Value":"https://magnumworkshop.com/wp-content/uploads/2020/11/kimsymmetry.jpg"
}
```
Response
```json
{
    "Faces": [
        {
            "Left": 81,
            "Right": 295,
            "Top": 218,
            "Bottom": 432
        },
        {
            "Left": 498,
            "Right": 705,
            "Top": 222,
            "Bottom": 429
        }
    ],
    "Count": 2,
    "Result": true,
    "AdvancedInfo": [
        {
            "Key": "ImageDownloadTimeInMs",
            "Value": "265"
        },
        {
            "Key": "ImageSizeInBytes",
            "Value": "24942"
        }
    ],
    "Status": {
        "Code": 3000,
        "Description": "OK",
        "Exception": null
    },
    "TrackingId": "44488d6c-cade-4f84-bb90-5eac9f7f6c59"
}
```
