namespace FPVenturesFive9Dispostion.Models
{
	public class PestRouteZohoConfigurationSettings
	{
		public string ZohoBaseUrl { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoAddPestRouteCustomerPath { get; set; }
		public string ZohoCheckDuplicatePestRouteCustomerPath { get; set; }
		public string ZohoRefreshToken { get; set; }
		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string PestRouteBaseUrl { get; set; }
		public string PestRouteSearchCustomerPath { get; set; }
		public string PestRouteGetCutomerDetailsPath { get; set; }
		public string PestRouteAuthenticationKey { get; set; }
		public string PestRouteAuthenticationToken { get; set; }
	}
}
