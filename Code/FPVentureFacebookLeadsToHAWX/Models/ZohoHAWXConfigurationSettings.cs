namespace FPVentureFacebookLeadsToHAWX.Models
{
	public class ZohoHAWXConfigurationSettings
	{
		public string ZohoLeadsBaseUrl { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoAddLeadsPath { get; set; }
		public string ZohoCheckDuplicateLeadsPath { get; set; }
		public string ZohoRefreshToken { get; set; }
		public string ZohoHAWXRefreshToken { get; set; }

		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }

		public string ZohoHAWXClientId { get; set; }
		public string ZohoHAWXClientSecret { get; set; }

		public string COQLQuery { get; set; }

		public string ZohoCOQLPath { get; set; }
	}
}
