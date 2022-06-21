namespace FPVenturesZohoInventoryVendorCredits.Models
{
	public class ZohoCRMAndInventoryConfigurationSettings
	{
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoCRMRefreshToken { get; set; }
        public string ZohoCRMVendorsPath { get; set; }
        public string ZohoCRMBaseUrl { get; set; }
        public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string ZohoInventoryBaseUrl { get; set; }
        public string ZohoInventoryVendorsPath { get; set; }
        public string ZohoInventoryOrganizationId { get; set; }
        public string ZohoInventoryRefreshToken { get; set; }
        public string DispositionCOQLQuery { get; set; }
        public string ZohoCOQLPath { get; set; }

    }
}
