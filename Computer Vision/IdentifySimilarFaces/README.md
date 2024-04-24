**Compare and match detected faces**

When the Face service detects a face, it assigns an ID to it, which is stored in the service resource for 24 hours. This ID, represented by a GUID, doesn't reveal the individual's identity beyond their facial characteristics.

While the detected face ID remains cached, it enables the comparison of new faces with the cached identity, determining their similarity based on facial features or verifying if the same person appears in multiple images. This anonymous face comparison capability proves beneficial in scenarios requiring confirmation of the same person's presence across different instances, without disclosing their actual identity. For instance, it can be applied in systems capturing images of individuals entering and exiting a secure area to ensure that all entrants also exit.

**Facial recognition**

In situations requiring positive identification of individuals, you can develop a facial recognition model by training it with facial images.

To train a facial recognition model with the Face service:

* Set up a Person Group to define the individuals you aim to identify, such as employees.
* Populate the Person Group by adding a Person for each individual you want to recognize.
* Incorporate detected faces from various images into each person's record, ensuring coverage of different poses. These faces, referred to as persisted faces, do not expire after 24 hours.
* Initiate the training process for the model.

After training, the model is stored within your Face (or Azure AI Services) resource, enabling client applications to:

* Identify individuals depicted in images.
* Verify the identity of a detected face.
* Analyze new images to locate faces similar to known, persisted faces.

**Note**: Usage of facial recognition, comparison, and verification will require getting approved through a [Limited Access policy](https://learn.microsoft.com/en-us/azure/ai-services/cognitive-services-limited-access). 