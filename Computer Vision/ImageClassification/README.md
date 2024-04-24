In Azure AI Vision, we can create custom models to train AI algorithms for tasks such as image classification or object detection. 

Image classification is a common computer vision problem that requires software to analyze an image in order to categorize (or classify) it.

Object detection is another common computer vision problem that requires software to identify the location of specific classes of object in an image. 

Custom Azure AI Vision models have different functionality based on the type. The types of custom models include Image classification, Object detection, and Product recognition.

- [Vision Studio](https://portal.vision.cognitive.azure.com/) online interface to train and test the model 
- [Computer Vision APIs](https://centraluseuap.dev.cognitive.microsoft.com/docs/services/unified-vision-apis-public-preview-2023-02-01-preview/operations/61d65934cd35050c20f73ab6), has collection of APIs for Analyzing, creating dataset , training and testing the models.
- [SDK Recerence](https://learn.microsoft.com/en-us/azure/ai-services/computer-vision/how-to/call-analyze-image-40?tabs=csharp&pivots=programming-language-csharp)

**Image Classification**
* Create a Storage Account and enable "Allow Blob anonymous access" in the configuration section
* Create a Container and set the access level to "anonymous read access for containers and blobs"
* Run the command in the power shell to update the [COCO](COCO.md) file storage name.
  ```json
    $storageAcct = 'store100001' (Get-Content training-images/training_labels.json) -replace '<storageAccount>', $storageAcct Out-File training-images/training_labels.json
  ```
* Access vision studio and  Create a dataset and select the Storage account and the container.
* Add the COCO file to the dataset.
* Create a custom model for the the image classification and select the dataset created for the training purpose
* Select the dataset for the evaluation and this is an optional. This will be used to evaluate the trained model.
* After completion of training the model. Try out the model with sample images.

Sample Apple Image
```json
{
  "apim-request-id": "2c0ca355-0c7a-4dfe-8a6a-183c1f494054",
  "content-length": "281",
  "content-type": "application/json; charset=utf-8",
  "customModelResult": {
    "tagsResult": {
      "values": [
        {
          "name": "apple",
          "confidence": 0.9996803998947144
        },
        {
          "name": "orange",
          "confidence": 0.00016222977137658745
        },
        {
          "name": "banana",
          "confidence": 0.00015737200737930834
        }
      ]
    }
  },
  "modelVersion": "2023-02-01-preview",
  "metadata": {
    "width": 1024,
    "height": 768
  }
}
```
Sample Banana Image
```json
{
  "apim-request-id": "ecc9c8a4-8df6-4f6a-8626-f1015e9b4185",
  "content-length": "280",
  "content-type": "application/json; charset=utf-8",
  "customModelResult": {
    "tagsResult": {
      "values": [
        {
          "name": "banana",
          "confidence": 0.9996438026428223
        },
        {
          "name": "apple",
          "confidence": 0.00017823839152697474
        },
        {
          "name": "orange",
          "confidence": 0.0001779099548002705
        }
      ]
    }
  },
  "modelVersion": "2023-02-01-preview",
  "metadata": {
    "width": 1024,
    "height": 768
  }
}
```

* Endpoint: https://aidemo1000001.cognitiveservices.azure.com/computervision/imageanalysis:analyze?api-version=2023-02-01-preview&features=tags,Read,SmartCrops,Objects,people&model-name=imgclas&language=en&gender-neutral-caption=False
    aidemo1000001 is the name of the computer vision or multi-service account resource in Azure
* Headers : Content-Type: application/json
* Headers : Ocp-Apim-Subscription-Key: Key from the resources aidemo1000001
* Method : POST
* Body : raw 
* In the query parameters features has value "tags,Read,SmartCrops,Objects,people" and model with "imglas"(custom trained model)

Request
```json
{
  "url": "https://store100001.blob.core.windows.net/fruit/IMG_20200229_164759.jpg"
}

```
Response
```json
{
  "customModelResult": {
    "tagsResult": {
      "values": [{
        "name": "apple",
        "confidence": 0.999735414981842
      }, {
        "name": "banana",
        "confidence": 0.00013413297710940242
      }, {
        "name": "orange",
        "confidence": 0.00013055780436843634
      }]
    }
  },
  "objectsResult": {
    "values": [{
      "boundingBox": {
        "x": 290,
        "y": 86,
        "w": 589,
        "h": 582
      },
      "tags": [{
        "name": "Apple",
        "confidence": 0.883
      }]
    }]
  },
  "readResult": {
    "stringIndexType": "TextElements",
    "content": "",
    "pages": [{
      "height": 768.0,
      "width": 1024.0,
      "angle": 0.0,
      "pageNumber": 1,
      "words": [],
      "spans": [{
        "offset": 0,
        "length": 0
      }],
      "lines": []
    }],
    "styles": [],
    "modelVersion": "2022-04-30"
  },
  "modelVersion": "2023-02-01-preview",
  "metadata": {
    "width": 1024,
    "height": 768
  },
  "tagsResult": {
    "values": [{
      "name": "natural foods",
      "confidence": 0.9620983600616455
    }, {
      "name": "food",
      "confidence": 0.9589560031890869
    }, {
      "name": "fruit",
      "confidence": 0.9543389081954956
    }, {
      "name": "produce",
      "confidence": 0.9328787326812744
    }, {
      "name": "mcintosh",
      "confidence": 0.9206641316413879
    }, {
      "name": "accessory fruit",
      "confidence": 0.8927763104438782
    }, {
      "name": "local food",
      "confidence": 0.8861185312271118
    }, {
      "name": "superfood",
      "confidence": 0.8593957424163818
    }, {
      "name": "apple",
      "confidence": 0.7911258339881897
    }, {
      "name": "indoor",
      "confidence": 0.7850078344345093
    }, {
      "name": "red",
      "confidence": 0.6813288331031799
    }]
  },
  "smartCropsResult": {
    "values": [{
      "aspectRatio": 1.33,
      "boundingBox": {
        "x": 43,
        "y": 32,
        "w": 938,
        "h": 704
      }
    }]
  },
  "peopleResult": {
    "values": []
  }
}

```