namespace PestRoutesTest.Models
{
	public class PestRouteCommonFieldsModel
	{
        public class Params
        {
            public string endpoint { get; set; }
            public string action { get; set; }
            public string authenticationKey { get; set; }
            public string officeID { get; set; }
            public string authenticationToken { get; set; }
            public string customerIDs { get; set; }
        }

        public class TokenUsage
        {
            public string requestsReadToday { get; set; }
            public string requestsWriteToday { get; set; }
            public string requestsReadInLastMinute { get; set; }
            public string requestsWriteInLastMinute { get; set; }
        }

        public class TokenLimits
        {
            public int limitReadRequestsPerMinute { get; set; }
            public int limitReadRequestsPerDay { get; set; }
            public int limitWriteRequestsPerMinute { get; set; }
            public int limitWriteRequestsPerDay { get; set; }
        }
    }
}
