namespace DeleteUnAnsweredDispositionsFromZohoCRM.Models
{
	public class ZohoCRMConfigurationSettings
	{
		public string ZohoLeadsPath { get; set; }
		public string ZohoLeadsBaseUrl { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoInventoryBaseUrl { get; set; }
		public string ZohoInventoryAddItemPath { get; set; }
		public string ZohoRefreshToken { get; set; }
		public string ZohoInventoryRefreshToken { get; set; }
		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string COQLQuery { get; set; }
		public string ZohoCOQLPath { get; set; }

		public string ZohoDispositionsPath { get; set; }

	}
}
