Using Azure AI services lets app developers concentrate on building their own code without worrying about the underlying setup, thanks to Microsoft's scalable and managed services. However, there are times when organizations need more control over their infrastructure and the data shared between services.

Many of the Azure AI services' APIs can be containerized and run on an organization's own infrastructure, like local Docker servers, Azure Container Instances, or Azure Kubernetes Services clusters. These containerized services still need to connect with an Azure-based account for billing purposes, but the application data itself doesn't get sent back to Azure's backend services. This setup gives organizations more control over their container deployment configurations, allowing for tailored solutions in areas such as authentication, scalability, and more.

Create Azure AI services multi-service account.

<b>Language Detection Docker Container</b> </br>

Pull the docker image to the local machine
docker pull mcr.microsoft.com/azure-cognitive-services/textanalytics/language:latest

run the docker command
docker run --rm -it -p 5000:5000 --memory 4g --cpus 1 mcr.microsoft.com/azure-cognitive-services/textanalytics/language Eula=accept Billing=<End point of the AI service> ApiKey=<APIKEY of the AI Service>

After a minute access http://localhost:5000 from the browser to know the REST-based query prediction endpoint APIs.
Capture the Swagger URL of the endpoints and test it in the postman tool providing the required headers, method type and data

<b>Azure Container Instance</b> </br>

Create container instance providing the image "mcr.microsoft.com/azure-cognitive-services/textanalytics/language:latest”, size Size: 1 vcpu, 12 GB memory, and Networking type as Public, unique name for DNS name label and port number.

In the Advanced section update the info 

* Restart policy: On failure
* Environment variables:</br>

Mark as secure | Key      | Value</br>
-------------- | ---      | -----
Yes            |	ApiKey  | Either key for your Azure AI services resource</br>
Yes	           |	Billing |	The endpoint URI for your Azure AI services resource</br>
No             |  Eula    | accept</br>

Once the container instance has been created, ensure it is running and then access it using either its IP address or its Fully Qualified Domain Name (FQDN).

There is no need to specify the Azure AI services endpoint or key. The request is processed by the containerized service. The container in turn communicates periodically with the service in Azure to report usage for billing, but does not send request data.


* Endpoint : http://<your_ACI_IP_address_or_FQDN>:5000/text/analytics/v3.0/languages 
* Content-Type: application/json 
* ocp-apim-subscription-key: AI service key to be passed in the headder 
* Post Data: {'documents':[{'id':1,'text':'This is for AI-102'},{'id':2,'text':"C'est pour AI-102"}]} 


```json
Response: 
{
  "documents": [
    {
      "id": "1",
      "detectedLanguages": [
        {<
          "name": "English",
          "iso6391Name": "en",
          "score": 0.95
        }
      ]
    },
    {
      "id": "2",
      "detectedLanguages": [
        {
          "name": "French",
          "iso6391Name": "fr",
          "score": 0.90
        }
      ]
    }
  ],
  "errors": []
}
```

