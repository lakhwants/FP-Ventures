namespace FPVenturesFive9InventoryDisposition.Models
{
    public class Five9ZohoInventoryConfigurationSettings
	{
		public string ZohoBaseUrl { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoAddDispositionsPath { get; set; }
		public string ZohoCheckDuplicateDispositionsPath { get; set; }
		public string ZohoInventoryRefreshToken { get; set; }
		public string ZohoInventoryBaseUrl { get; set; }
		public string ZohoInventoryItemPath { get; set; }
		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string Five9BaseUrl { get; set; }
		public string Five9ServiceNamespace { get; set; }
		public string Five9MethodAction { get; set; }
		public string ZohoCOQLPath { get; set; }
		public string ZohoInventoryOrganizationId { get; set; }
		public string Five9BasicAuthenticationToken { get; set; }
		public string ZohoRefreshToken { get; set; }
	}
}
