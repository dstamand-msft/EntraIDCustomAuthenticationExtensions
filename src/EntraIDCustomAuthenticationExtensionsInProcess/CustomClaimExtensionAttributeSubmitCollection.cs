using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EntraIDCustomAuthenticationExtensionsInProcess
{
    public class CustomClaimExtensionAttributeSubmitCollection
    {
        private readonly ILogger<CustomClaimExtensionAttributeSubmitCollection> _logger;

        public CustomClaimExtensionAttributeSubmitCollection(ILogger<CustomClaimExtensionAttributeSubmitCollection> logger)
        {
            _logger = logger;
        }

        [FunctionName("onAttributeCollectionSubmit")]
        // see https://learn.microsoft.com/en-us/entra/identity-platform/custom-extension-attribute-collection?context=%2Fentra%2Fexternal-id%2Fcustomers%2Fcontext%2Fcustomers-context&tabs=start-continue%2Csubmit-continue#13-configure-the-http-trigger-for-onattributecollectionsubmit
        // for the type of responses to Entra depending of user input
        public async Task<object> Run([HttpTrigger] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic request = JsonConvert.DeserializeObject(requestBody);

            var actions = new List<ContinueWithDefaultBehavior>{
                new() { type = "microsoft.graph.attributeCollectionSubmit.continueWithDefaultBehavior" }
            };

            var dataObject = new Data
            {
                type = "microsoft.graph.onAttributeCollectionSubmitResponseData",
                actions = actions
            };

            dynamic response = new ResponseObject
            {
                data = dataObject
            };

            // Send the response
            return response;
        }

        public class ResponseObject
        {
            public Data data { get; set; }
        }

        [JsonObject]
        public class Data
        {
            [JsonProperty("@odata.type")]
            public string type { get; set; }

            public List<ContinueWithDefaultBehavior> actions { get; set; }
        }

        [JsonObject]
        public class ContinueWithDefaultBehavior
        {
            [JsonProperty("@odata.type")]
            public string type { get; set; }
        }
    }
}
