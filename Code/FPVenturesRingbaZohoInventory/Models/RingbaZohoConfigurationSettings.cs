namespace FPVenturesRingbaZohoInventory.Models
{
	public class RingbaZohoConfigurationSettings
	{
		public string RingbaBaseUrl { get; set; }
		public string RingbaCallLogDetailsPath { get; set; }
		public string RingbaCallLogsPath { get; set; }
		public string RingbaAccountsPath { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoCRMRefreshToken { get; set; }
        public string ZohoCRMVendorsPath { get; set; }
        public string ZohoCRMBaseUrl { get; set; }
        public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string RingbaAccessToken { get; set; }
		public string ZohoInventoryBaseUrl { get; set; }
		public string ZohoInventoryAddItemPath { get; set; }
        public string ZohoInventoryItemGroupsPath { get; set; }
        public string ZohoInventoryVendorsPath { get; set; }
        public string ZohoInventoryOrganizationId { get; set; }
        public string ZohoInventoryRefreshToken { get; set; }
	}
}
