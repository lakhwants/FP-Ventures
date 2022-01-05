namespace FPVenturesFive9UnbounceDisposition.Models
{
	public class Five9ZohoConfigurationSettings
	{
		public string ZohoBaseUrl { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoAddDispositionsPath { get; set; }
		public string ZohoCheckDuplicateDispositionsPath { get; set; }
		public string ZohoCheckDuplicateLeadsPath { get; set; }
		public string ZohoRefreshToken { get; set; }
		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string Five9BaseUrl { get; set; }
		public string Five9ServiceNamespace { get; set; }
		public string Five9MethodAction { get; set; }
	}
}
