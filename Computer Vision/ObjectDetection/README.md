In Azure AI Vision, we can create custom models to train AI algorithms for tasks such as image classification or object detection. 

Image classification is a common computer vision problem that requires software to analyze an image in order to categorize (or classify) it.

Object detection is another common computer vision problem that requires software to identify the location of specific classes of object in an image. 

Custom Azure AI Vision models have different functionality based on the type. The types of custom models include Image classification, Object detection, and Product recognition.

- [Vision Studio](https://portal.vision.cognitive.azure.com/) online interface to train and test the model 
- [Computer Vision APIs](https://centraluseuap.dev.cognitive.microsoft.com/docs/services/unified-vision-apis-public-preview-2023-02-01-preview/operations/61d65934cd35050c20f73ab6), has collection of APIs for Analyzing, creating dataset , training and testing the models.
- [SDK Recerence](https://learn.microsoft.com/en-us/azure/ai-services/computer-vision/how-to/call-analyze-image-40?tabs=csharp&pivots=programming-language-csharp)

**Object Detection**
* Create a Storage Account and enable "Allow Blob anonymous access" in the configuration section
* Create a Container and set the access level to "anonymous read access for containers and blobs"
* Run the command in the power shell to update the [COCO](COCO.md) file storage name.
  ```json
    $storageAcct = 'store100001' (Get-Content training-images/training_labels.json) -replace '<storageAccount>', $storageAcct Out-File training-images/training_labels.json
  ```
* Access vision studio and Create a dataset and select the Storage account and the container.
* Add the COCO file to the dataset.
* Create a custom model for the the image classification and select the dataset created for the training purpose
* Select the dataset for the evaluation and this is an optional. This will be used to evaluate the trained model.
* After completion of training the model. Try out the model with sample images.


* Endpoint: https://aidemo1000001.cognitiveservices.azure.com/computervision/imageanalysis:analyze?api-version=2023-02-01-preview&model-name=objdetect&language=en&gender-neutral-caption=False
    aidemo1000001 is the name of the computer vision or multi-service account resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo1000001
* Method : POST
* Body : raw 
* In the query parameters features has value "tags,Read,SmartCrops,Objects,people" and model with "objdetect"(custom trained model)

Request
```json
{
  "url": "https://store100001.blob.core.windows.net/objdect/produce.jpg"
}
```
Response
```json
{
  "customModelResult": {
    "objectsResult": {
      "values": [{
        "id": "1",
        "boundingBox": {
          "x": 486,
          "y": 135,
          "w": 272,
          "h": 297
        },
        "tags": [{
          "name": "Orange",
          "confidence": 0.8828125
        }]
      }, {
        "id": "2",
        "boundingBox": {
          "x": 659,
          "y": 351,
          "w": 357,
          "h": 353
        },
        "tags": [{
          "name": "Apple",
          "confidence": 0.87060546875
        }]
      }, {
        "id": "3",
        "boundingBox": {
          "x": 3,
          "y": 215,
          "w": 478,
          "h": 446
        },
        "tags": [{
          "name": "Banana",
          "confidence": 0.82568359375
        }]
      }, {
        "id": "4",
        "boundingBox": {
          "x": 3,
          "y": 215,
          "w": 478,
          "h": 446
        },
        "tags": [{
          "name": "Orange",
          "confidence": 0.32421875
        }]
      }, {
        "id": "5",
        "boundingBox": {
          "x": 659,
          "y": 351,
          "w": 357,
          "h": 353
        },
        "tags": [{
          "name": "Banana",
          "confidence": 0.2802734375
        }]
      }, {
        "id": "6",
        "boundingBox": {
          "x": 659,
          "y": 351,
          "w": 357,
          "h": 353
        },
        "tags": [{
          "name": "Orange",
          "confidence": 0.270263671875
        }]
      }, {
        "id": "7",
        "boundingBox": {
          "x": 5,
          "y": 215,
          "w": 477,
          "h": 447
        },
        "tags": [{
          "name": "Apple",
          "confidence": 0.2198486328125
        }]
      }]
    }
  },
  "modelVersion": "2023-02-01-preview",
  "metadata": {
    "width": 1024,
    "height": 768
  }
}
```