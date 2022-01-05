using Newtonsoft.Json;

namespace FPVenturesPestRoutes.Models
{
	public class PestRouteCommonFieldsModel
	{
        public class Params
        {
            [JsonProperty("endpoint")]
            public string EndPoint { get; set; }

            [JsonProperty("action")]
            public string Action { get; set; }

            [JsonProperty("authenticationKey")]
            public string AuthenticationKey { get; set; }

            [JsonProperty("officeID")]
            public string OfficeID { get; set; }

            [JsonProperty("authenticationToken")]
            public string AuthenticationToken { get; set; }

            [JsonProperty("customerIDs")]
            public string CustomerIDs { get; set; }
        }

        public class TokenUsage
        {
            [JsonProperty("requestsReadToday")]
            public string RequestsReadToday { get; set; }

            [JsonProperty("requestsWriteToday")]
            public string RequestsWriteToday { get; set; }

            [JsonProperty("requestsReadInLastMinute")]
            public string RequestsReadInLastMinute { get; set; }

            [JsonProperty("requestsWriteInLastMinute")]
            public string RequestsWriteInLastMinute { get; set; }
        }

        public class TokenLimits
        {
            [JsonProperty("limitReadRequestsPerMinute")]
            public int LimitReadRequestsPerMinute { get; set; }

            [JsonProperty("limitReadRequestsPerDay")]
            public int LimitReadRequestsPerDay { get; set; }

            [JsonProperty("limitWriteRequestsPerMinute")]
            public int LimitWriteRequestsPerMinute { get; set; }

            [JsonProperty("limitWriteRequestsPerDay")]
            public int LimitWriteRequestsPerDay { get; set; }
        }
    }
}
