namespace FPVenturesZohoInventorySalesOrder.Models
{
    public class ConfigurationSettings
    {
        public string ZohoAccessTokenFromRefreshTokenPath { get; set; }
        public string ZohoInventoryBaseUrl { get; set; }
        public string ZohoInventoryItemPath { get; set; }
        public string ZohoInventoryAddSalesOrderPath { get; set; }
        public string ZohoInventoryContactPersonPath { get; set; }
        public string ZohoInventoryContactsPath { get; set; }
        public string ZohoInventoryInvoicePath { get; set; }
        public string ZohoInventoryInvoiceFromSalesOrderPath { get; set; }
        public string ZohoInventoryCustomerName { get; set; }
        public string ZohoInventoryOrganizationId { get; set; }
        public string ZohoInventorySalesOrderConfirmPath { get; set; }
        public string ZohoInventorySearchParameter { get; set; }
        public string ZohoInventoryTaxesPath { get; set; }
        public string ZohoInventoryRefreshToken { get; set; }
        public string ZohoClientId { get; set; }
        public string ZohoClientSecret { get; set; }
    }
}
