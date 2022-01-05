using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesPestRoutes.Models
{
    public class PestRouteCustomerIDsModel : PestRouteCommonFieldsModel
    {
        [JsonProperty("@params")]
        public Params Params { get; set; }

        [JsonProperty("tokenUsage")]
        public TokenUsage TokenUsage { get; set; }

        [JsonProperty("tokenLimits")]
        public TokenLimits TokenLimits { get; set; }

        [JsonProperty("requestAction")]
        public string RequestAction { get; set; }

        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("idName")]
        public string IdName { get; set; }

        [JsonProperty("processingTime")]
        public string ProcessingTime { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("customerIDs")]
        public List<int> CustomerIDs { get; set; }

        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }
    }


}
