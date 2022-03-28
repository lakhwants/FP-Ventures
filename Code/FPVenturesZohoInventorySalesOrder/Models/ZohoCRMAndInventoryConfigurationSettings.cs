namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class ZohoCRMAndInventoryConfigurationSettings
	{
		public string ZohoLeadsBaseUrl { get; set; }
		public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
		public string ZohoInventoryBaseUrl { get; set; }
		public string ZohoInventoryItemPath { get; set; }
		public string ZohoRefreshToken { get; set; }
		public string ZohoInventoryRefreshToken { get; set; }
		public string ZohoClientId { get; set; }
		public string ZohoClientSecret { get; set; }
		public string COQLQuery { get; set; }
		public string ZohoCOQLPath { get; set; }
		public string ZohoInventoryAddSalesOrderPath { get; set; }
		public string ZohoInventoryInvoicePath { get; set; }		
		public string ZohoInventoryTaxesPath { get; set; }
		public string ZohoInventorySalesOrderConfirmPath { get; set; }
		public string ZohoInventoryOrganizationId { get; set; }
		public string ZohoInventorySearchParameter { get; set; }
		public string ZohoInventoryContactsPath { get; set; }
		public string ZohoInventoryContactPersonPath { get; set; }
		public string ZohoInventoryCustomerName { get; set; }

	}
}
