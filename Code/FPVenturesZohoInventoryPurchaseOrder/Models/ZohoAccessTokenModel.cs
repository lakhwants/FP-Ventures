using Newtonsoft.Json;

namespace FPVenturesZohoInventoryPurchaseOrder.Models
{
	public class ZohoAccessTokenModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("api_domain")]
        public string ApiDomain { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }

}
