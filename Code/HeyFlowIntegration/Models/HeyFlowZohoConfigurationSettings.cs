namespace HeyFlowIntegration.Models
{
	public class HeyFlowZohoConfigurationSettings
	{
		public string ZohoLeadsBaseUrl { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoAddLeadsPath { get; set; }
		public string ZohoCheckDuplicateLeadsPath { get; set; }
		public string ZohoRefreshToken { get; set; }
		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string HawxZohoRefreshToken { get; set; }
		public string HawxZohoClientId { get; set; }
		public string HawxZohoClientSecret { get; set; }
	}
}
