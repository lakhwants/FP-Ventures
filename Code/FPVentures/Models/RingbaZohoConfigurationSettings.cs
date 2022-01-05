namespace FPVentures.Models
{
	public class RingbaZohoConfigurationSettings
	{
		public string RingbaBaseUrl { get; set; }
		public string ZohoLeadsBaseUrl { get; set; }
		public string RingbaCallLogDetailsPath { get; set; }
		public string RingbaCallLogsPath { get; set; }
		public string RingbaAccountsPath { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoAddLeadsPath { get; set; }
		public string ZohoCheckDuplicateLeadsPath { get; set; }
		public string ZohoRefreshToken { get; set; }
		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string RingbaAccessToken { get; set; }
	}
}
