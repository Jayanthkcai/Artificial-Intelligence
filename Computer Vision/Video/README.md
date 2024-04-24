Azure Video Indexer is a service to extract insights from video, including face identification, text recognition, object labels, scene segmentations, and more.

The Azure Video Indexer service offers tools for extracting valuable insights from videos, including:

* Facial Recognition: Identifying individuals present in the video, subject to Limited Access approval.
* Optical Character Recognition (OCR): Extracting text embedded within the video.
* Speech Transcription: Converting spoken dialogue into written text for transcription purposes.
* Topics Identification: Recognizing and highlighting key subjects discussed within the video content.
* Sentiment Analysis: Analyzing the overall emotional tone, determining positivity or negativity within segments of the video.
* Labels: Providing descriptive tags to identify significant objects or themes present throughout the video.
* Content Moderation: Detecting potentially sensitive content such as adult or violent themes.
* Scene Segmentation: Breaking down the video into distinct scenes for better organization and analysis.

Azure Video Indexer offers predefined models for recognizing well-known celebrities, performing Optical Character Recognition (OCR), and transcribing spoken phrases into text. Additionally, you can enhance the recognition capabilities of Video Indexer by creating custom models for:

People: By adding images of individuals' faces you wish to recognize in videos and training a model, Video Indexer will identify these individuals across all your videos. 
Note that this feature requires [Limited Access](https://learn.microsoft.com/en-us/azure/ai-services/cognitive-services-limited-access) approval and adherence to Responsible AI standards.

Language: Custom models can be trained to detect and transcribe specific terminology used within your organization, even if it's not commonly used.

Brands: Training a model enables recognition of specific names as brands, aiding in the identification of products, projects, or companies relevant to your business.


**Analyze Video with Video Analyzer**

* Access the portal https://www.videoindexer.ai
* Create account or sign up for a free account and sign in using your Microsoft account 
* In the video analyzer portal, select the option of Upload and select enter URL (https://aka.ms/responsible-ai-video) and click add.
Change the default name to "Responsible AI," review the default settings, and select the checkbox to verify compliance with Microsoft’s policies for facial recognition before uploading the file.
* Once the file has been uploaded, allow few minutes for Video Analyzer to automatically index it.


The indexing process extracts insights from the video, which can be viewed in the portal.

**Review video insights**

At the top right of the portal, select the View symbol (which looks similar to 🗇), and in the list of insights, in addition to Transcript, select OCR and Speakers.

Observe that the Timeline pane now includes:
* Transcript of audio narration.
* Text visible in the video.
* Indications of speakers who appear in the video. Some well-known people are automatically recognized by name, others are indicated by number (for example Speaker #1).

Switch back to the Insights pane and view the insights show there. They include:
* Individual people who appear in the video.
* Topics discussed in the video.
* Labels for objects that appear in the video.
* Named entities, such as people and brands that appear in the video.
* Key scenes.

With the Insights pane visible, select the View symbol again, and in the list of insights, add Keywords and Sentiments to the pane.

The insights found can help you determine the main themes in the video. For example, the topics for this video show that it is clearly about technology, social responsibility, and ethics.

**Search for insights**

In the Insights pane, input "Bee" in the Search box, and if necessary, scroll down to view results encompassing various types of insight. Note the detection of one matching label, along with its location in the video highlighted below. Proceed to select the initial part of the segment signifying the bee's presence and review the corresponding video footage at that moment (careful selection may be required as the bee appears briefly). Finally, clear the Search box to display all insights available for the video.

**Video Analyzer as widgets**

The Video Analyzer portal serves as a valuable interface for overseeing video indexing projects. Yet, there might arise instances where you wish to share the video and its insights with individuals who lack access to your Video Analyzer account. In such cases, Video Analyzer offers widgets that can be embedded into a webpage, facilitating easy accessibility to the content for a wider audience.

 Simple HTML page where you can incorporate the Video Analyzer Player and Insights widgets. Make sure to include the reference to the vb.widgets.mediator.js script in the header section, as this script facilitates interaction among multiple Video Analyzer widgets on the page.

In the Video Analyzer portal, Under the video player, select </> Embed to view the HTML iframe code to embed the widgets.

Save the file and open in the browser, using the Insights widget search for insights and jump to the video.


```markdown
<html>
    <header>
        <title>Analyze Video</title>
        <script src="https://breakdown.blob.core.windows.net/public/vb.widgets.mediator.js"></script>
    </header>
    <body>
        <h1>Video Analysis</h1>
        <table>
            <tr>
                <td style="vertical-align:top;">
                    <!--Player widget goes here -->
                    <iframe width="1280" height="720" src="https://www.videoindexer.ai/embed/player/51e8093c-1daf-4cc7-a0f8-42b39602c8fb/92352554c0/?accessToken=eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJWZXJzaW9uIjoiMi4wLjAuMCIsIktleVZlcnNpb24iOiIzOWRkZTA2ZmY3MjY0MzM3ODkxZTY4ZThjY2U2YjlkOCIsIkFjY291bnRJZCI6IjUxZTgwOTNjLTFkYWYtNGNjNy1hMGY4LTQyYjM5NjAyYzhmYiIsIkFjY291bnRUeXBlIjoiVHJpYWwiLCJWaWRlb0lkIjoiOTIzNTI1NTRjMCIsIlBlcm1pc3Npb24iOiJSZWFkZXIiLCJFeHRlcm5hbFVzZXJJZCI6IjE6bGl2ZS5jb206MDAwNjQwMDA4QjM0ODAzNCIsIlVzZXJUeXBlIjoiTWljcm9zb2Z0Q29ycEFhZCIsIklzc3VlckxvY2F0aW9uIjoiVHJpYWwiLCJuYmYiOjE3MTA4NzE1NTQsImV4cCI6MTcxMDg3NTQ1NCwiaXNzIjoiaHR0cHM6Ly9hcGkudmlkZW9pbmRleGVyLmFpLyIsImF1ZCI6Imh0dHBzOi8vYXBpLnZpZGVvaW5kZXhlci5haS8ifQ.dhFG_LgbYbpOqRjwg2zEyAz72rlq6RbYqcUWdV7JP5uVfejjr53T3lM8sgxQY5PwIuxcF9xet0dbOZIoVlXMTlDEIekqr7o5RbHkFFYAT636KrjVZs0gKYKkQSZAjit6_iUES_wciMU2RyX-7EG5QIJ96ioGLHJzjZtwlUHv_ukxxz2NiT8CS0_liunmNGyTlNwAQFV-wT3NvKNyWN3Vaw6aySoyrGb5afya-h38pQHKpgy89Mf27TTE-4vdhvpaA-i6PBQ-vIDsYRJt1gAJHI1BRz4STtAAtTXGEyvqnPGBeZnkPQ5eYCSr2UrAQGpfbLSupLHLT49JRUc8LEhYFw&locale=en&location=trial" frameborder="0" allowfullscreen></iframe> 
                </td>
                <td style="vertical-align:top;">
                    <!-- Insights widget goes here -->
                    <iframe width="580" height="780" src="https://www.videoindexer.ai/embed/insights/51e8093c-1daf-4cc7-a0f8-42b39602c8fb/92352554c0/?accessToken=eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJWZXJzaW9uIjoiMi4wLjAuMCIsIktleVZlcnNpb24iOiIzOWRkZTA2ZmY3MjY0MzM3ODkxZTY4ZThjY2U2YjlkOCIsIkFjY291bnRJZCI6IjUxZTgwOTNjLTFkYWYtNGNjNy1hMGY4LTQyYjM5NjAyYzhmYiIsIkFjY291bnRUeXBlIjoiVHJpYWwiLCJWaWRlb0lkIjoiOTIzNTI1NTRjMCIsIlBlcm1pc3Npb24iOiJSZWFkZXIiLCJFeHRlcm5hbFVzZXJJZCI6IjE6bGl2ZS5jb206MDAwNjQwMDA4QjM0ODAzNCIsIlVzZXJUeXBlIjoiTWljcm9zb2Z0Q29ycEFhZCIsIklzc3VlckxvY2F0aW9uIjoiVHJpYWwiLCJuYmYiOjE3MTA4NzE1NTQsImV4cCI6MTcxMDg3NTQ1NCwiaXNzIjoiaHR0cHM6Ly9hcGkudmlkZW9pbmRleGVyLmFpLyIsImF1ZCI6Imh0dHBzOi8vYXBpLnZpZGVvaW5kZXhlci5haS8ifQ.dhFG_LgbYbpOqRjwg2zEyAz72rlq6RbYqcUWdV7JP5uVfejjr53T3lM8sgxQY5PwIuxcF9xet0dbOZIoVlXMTlDEIekqr7o5RbHkFFYAT636KrjVZs0gKYKkQSZAjit6_iUES_wciMU2RyX-7EG5QIJ96ioGLHJzjZtwlUHv_ukxxz2NiT8CS0_liunmNGyTlNwAQFV-wT3NvKNyWN3Vaw6aySoyrGb5afya-h38pQHKpgy89Mf27TTE-4vdhvpaA-i6PBQ-vIDsYRJt1gAJHI1BRz4STtAAtTXGEyvqnPGBeZnkPQ5eYCSr2UrAQGpfbLSupLHLT49JRUc8LEhYFw&locale=en&location=trial" frameborder="0" allowfullscreen></iframe>
                </td>
            </tr>
        </table>
    </body>
</html>
```




