Microsoft Azure Content Moderator is a cloud-based tool that provides machine-assisted moderation of text, images, and videos to help detect potentially offensive or unwanted content. 

[**Text Moderation**](TEXT.md): Azure Content Moderator can screen text input for offensive or risky content, including profanity, sexually explicit or suggestive content, hate speech, and other undesirable material. It can also detect personally identifiable information (PII) to help protect user privacy.

The response from the Text Moderation API includes the following information:
* A list of potentially unwanted words found in the text.
* What type of potentially unwanted words were found.
* Possible personal data found in the text.

PII
```json
 "PII": {
        "Email": [
            {
                "Detected": "john.doe@email.com",
                "SubType": "Regular",
                "Text": "john.doe@email.com",
                "Index": 64
            }
        ],
        "IPA": [],
        "Phone": [
            {
                "CountryCode": "US",
                "Text": "123-456-7890",
                "Index": 104
            }
        ],
        "Address": [],
        "SSN": []
    },
```
The profane item is returned as a Term in the JSON response, along with an index value showing where the term is in the supplied text. The operation detects profanity in more than 100 languages and match against custom and shared blacklists.
```json
"Terms": [
        {
            "Index": 77,
            "OriginalIndex": 123,
            "ListId": 0,
            "Term": "crap"
        }
    ],
```

#### Classification ####
This feature of the API can place text into specific categories based on the following specifications:

* Category 1: Possible existence of language that could be viewed as adult or sexually explicit under certain circumstances.
* Category 2: Possible occurrence of language that may be regarded as sexually suggestive or of a mature nature in specific contexts.
* Category 3: Possible existence of language that could be considered offensive in particular circumstances.

Upon receiving the JSON response, it includes a Boolean value indicating whether manual review of the text is recommended. If the value is true, it suggests reviewing the content manually to assess potential issues.

Additionally, each category is assigned a score ranging from 0 to 1, reflecting the predicted category for the analyzed text.

```json
 "Classification": {
        "ReviewRecommended": false,
        "Category1": {
            "Score": 0.00121889088768512
        },
        "Category2": {
            "Score": 0.08827821910381317
        },
        "Category3": {
            "Score": 0.039455167949199677
        }
    },
```

**Image Moderation**: The service can analyze images for adult or racy content, which might not be suitable for all audiences. It uses advanced algorithms to assess visual content and provide a score indicating the potential presence of such content.

**Video Moderation**: Similar to image moderation, video moderation screens video content frame by frame for any adult or racy content, providing timestamps and scores for sections of the video that may require review.

**Custom Lists**: Users can create and manage custom lists of words or images that are specifically tailored to the context of their community or application. This feature allows for more granular control over what content is flagged as inappropriate.


