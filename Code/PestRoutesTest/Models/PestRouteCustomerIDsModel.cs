using System.Collections.Generic;

namespace PestRoutesTest.Models
{

	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
	//public class Params
 //   {
 //       public string endpoint { get; set; }
 //       public string action { get; set; }
 //       public string authenticationKey { get; set; }
 //       public string authenticationToken { get; set; }
 //   }

 //   public class TokenUsage
 //   {
 //       public string requestsReadToday { get; set; }
 //       public string requestsWriteToday { get; set; }
 //       public string requestsReadInLastMinute { get; set; }
 //       public string requestsWriteInLastMinute { get; set; }
 //   }

 //   public class TokenLimits
 //   {
 //       public int limitReadRequestsPerMinute { get; set; }
 //       public int limitReadRequestsPerDay { get; set; }
 //       public int limitWriteRequestsPerMinute { get; set; }
 //       public int limitWriteRequestsPerDay { get; set; }
 //   }

    public class PestRouteCustomerIDsModel : PestRouteCommonFieldsModel
    {
        public Params @params { get; set; }
        public TokenUsage tokenUsage { get; set; }
        public TokenLimits tokenLimits { get; set; }
        public string requestAction { get; set; }
        public string endpoint { get; set; }
        public bool success { get; set; }
        public string idName { get; set; }
        public string processingTime { get; set; }
        public int count { get; set; }
        public List<int> customerIDs { get; set; }
        public string propertyName { get; set; }
    }


}
