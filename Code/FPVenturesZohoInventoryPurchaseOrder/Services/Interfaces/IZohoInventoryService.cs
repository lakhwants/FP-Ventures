using FPVenturesZohoInventoryPurchaseOrder.Models;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryPurchaseOrder.Services.Interfaces
{
	public interface IZohoInventoryService
	{
		public ZohoInventoryPurchaseOrderResponseModel PostPurchaseOrdertoZohoInventory(ZohoInventoryPurchaseOrderRequestModel zohoInventoryPurchaseOrderRequestModel);
		public ZohoInventoryTaxesModel GetZohoInventoryTaxes();
		public List<InventoryItem> GetInventoryItems(List<Data> zohoLeads);
		public InventoryResponse ConfirmSalesOrder(string salesOrderId);
		public ZohoInventoryInvoiceResponseModel PostInvoice(ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel);
		public ZohoInventoryContactPersonResponseModel GetContactPersonFromZohoInventory(string contacts);
		public ZohoInventoryContactsResponseModel GetContactsFromZohoInventory();
	}
}
