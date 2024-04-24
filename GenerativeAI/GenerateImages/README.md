With Azure OpenAI, we can use large language models to make content from natural language prompts. For example, there's the DALL-E image generation model, which can create pictures based on descriptions.

Using AI to make graphics has lots of uses, like creating illustrations for articles or making logos for products. Basically, we can describe what we want, and the AI makes it happen.

# DALLE-E
DALL-E is a neural network based model that can generate graphical data from natural language input. Put more simply, you can provide DALL-E with a description and it can generate an appropriate image.

The pictures made by DALL-E are brand new; they're not taken from a set of images. So, DALL-E isn't like a search engine for finding pictures. Instead, it's an AI model that creates fresh images using the information it learned during training.

# Use the Azure OpenAI REST API to consume DALL-E models
To use the service, you'll require the endpoint and authorization key for your Azure OpenAI Service resource. To start the image generation, send a POST request to the endpoint with the authorization key in the header. The request should include the following parameters in a JSON body:

"prompt": Description of the image to create.
"n": Number of images to generate.
"size": Resolution of the image(s) (256x256, 512x512, or 1024x1024).

**Request**
```json
{
    "prompt": "A badger wearing a tuxedo",
    "n": 1,
    "size": "512x512"
}
```
**Response**
```json
{
    "created": 1686780744,
    "expires": 1686867152,
    "id": "6d765598-eeee-4f49-885d-03ee1c8f9b02",
    "result": {
        "created": 1686780744,
        "data": [
            {
                "url": "https://dalleproduse.....png"
            }
        ]
    },
    "status": "succeeded"
}
```
-------------------------------------------------------------------------------------------------
1. Create an Azure OpenAI resource, choose and DALLE-E model in OpenAI Studio.
2. Once the deployment is succeeded enter the prompt as "An elephant on a skateboard" to see the image generated
    ![elephant-on-skate-board](images/elephant-on-skate-board.png)
3. Modify the prompt to provide more specific decription "An elephant on a skateboard in the style of Picasso"
    ![picasso](images/picasso.png)
4. When using the playground, you can adjust the settings to specify:
    - The resolution (size) of the generated images. Available sizes are 256x256, 512x512, 1024x1024 (which is the default value), or 1024x1792.
    - The image style to be generated (such as vivid or natural).
    - The image quality (choose from standard or hd).
5. Install the  Azure AI OpenAI SDK package for the choice of your development language.
    - .NET : Azure.AI.OpenAI
    - Python : openai==1.13.3
6. Run the application and enter the description "A giraffe flying a kite" when prompted
    ```markdown
    Enter a prompt to request an image:
    A giraffe flying a kite
    "Imagine a long-necked, towering giraffe with an enchanting pattern of amber and cream spots. This gentle giant is in the middle of an open grassland under a bright, azure sky scattered with cotton-like clouds. It\u0027s not just grazing nor is it just appreciating the beauty of nature. The giraffe is doing something surprisingly whimsical. It\u0027s ambitiously flying a colorful kite! The kite dances and swirls in the wind, its brilliant shades of red, blue, green, and yellow like a vivid rainbow against the clear sky. A spectacle in the wild that you would not expect to see."
    "https://dalleproduse.blob.core.windows.net/private/images/8a0fb391-efcc-4584-9bd1-7a65c21bdfe5/generated_00.png?se=2024-04-22T11%3A32%3A53Z&sig=TDaCXyQ3%2BmxbwUwouF%2B7Cotox3T8kqgRo008Xkbwir4%3D&ske=2024-04-27T22%3A56%3A41Z&skoid=09ba021e-c417-441c-b203-c81e5dcd7b7f&sks=b&skt=2024-04-20T22%3A56%3A41Z&sktid=33e01921-4d64-4f8c-a055-5bdaffd5e33d&skv=2020-10-02&sp=r&spr=https&sr=b&sv=2020-10-02"
    ```
    ![kite](images/kite.png)
------------------------------------------------------------------------------------------------------
7. **Endpoint** : https://aidemoopenai100001.openai.azure.com/openai/deployments/dalle3/images/generations?api-version=2024-02-15-preview
   **Headers**
    - Content-Type : application/json
    - api-key : [Primary key of the Open API from the Key and Endpoint section]

   **Body**

    ```json
    {
    "prompt": "A multi-colored umbrella on the beach, disposable camera",//description of the image
    "n": 1, //#The number of images to generate. Only n=1 is supported for DALL-E 3.
    "size": "1024x1024",//Size, supported values are “1792x1024”, “1024x1024” and “1024x1792” 
    "quality": "hd",// Options are “hd” and “standard”; defaults to standard 
    "style": "vivid" // Options are “natural” and “vivid”; defaults to “vivid”
    }
    ```
    **Response**
    ```json
    {
    "created": 1713699774,
    "data": [
        {
            "content_filter_results": {
                "hate": {
                    "filtered": false,
                    "severity": "safe"
                },
                "self_harm": {
                    "filtered": false,
                    "severity": "safe"
                },
                "sexual": {
                    "filtered": false,
                    "severity": "safe"
                },
                "violence": {
                    "filtered": false,
                    "severity": "safe"
                }
            },
            "prompt_filter_results": {
                "hate": {
                    "filtered": false,
                    "severity": "safe"
                },
                "profanity": {
                    "detected": false,
                    "filtered": false
                },
                "self_harm": {
                    "filtered": false,
                    "severity": "safe"
                },
                "sexual": {
                    "filtered": false,
                    "severity": "safe"
                },
                "violence": {
                    "filtered": false,
                    "severity": "safe"
                }
            },
            "revised_prompt": "Imagine a beach scene on a sunny day. The yellow sand is shining under the beating sun, and the blue waves are crashing onto the shore. A multi-colored umbrella, showcasing hues of red, blue, green, yellow, and purple, is strategically placed providing a patch of shade on the beach. Near this vibrant umbrella, on the sun-warmed sand, lies a disposable camera with its lens reflecting the bright sunlight. The disposable camera, with its black body and bright stickers, is ready to capture the perfect beach day.",
            "url": "https://dalleproduse.blob.core.windows.net/private/images/cec6ca88-6d47-4e29-b6ee-e3cd9983d097/generated_00.png?se=2024-04-22T11%3A43%3A11Z&sig=hP6D5yvaXvrW%2FwwxdXEEE0pWvXdXU%2B1Kuifr%2FPE%2FLhs%3D&ske=2024-04-28T10%3A33%3A50Z&skoid=09ba021e-c417-441c-b203-c81e5dcd7b7f&sks=b&skt=2024-04-21T10%3A33%3A50Z&sktid=33e01921-4d64-4f8c-a055-5bdaffd5e33d&skv=2020-10-02&sp=r&spr=https&sr=b&sv=2020-10-02"
            }
        ]
    }
    ```
    ![hdimage](images/hdimage.png)