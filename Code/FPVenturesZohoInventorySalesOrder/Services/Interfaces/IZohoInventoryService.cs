using FPVenturesZohoInventorySalesOrder.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Services.Interfaces
{
	public interface IZohoInventoryService
	{
		public ZohoInventorySalesOrderResponseModel PostSalesOrdertoZohoInventory(ZohoInventorySalesOrderModel zohoInventorySalesOrderModel);
		public ZohoInventoryTaxesModel GetZohoInventoryTaxes();
		public List<InventoryItem> GetInventoryItems(DateTime startDate, DateTime endDate);
		public InventoryResponse ConfirmSalesOrder(string salesOrderId);
		public ZohoInventoryInvoiceResponseModel PostInvoice(ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel);
		public ZohoInventoryContactPersonResponseModel GetContactPersonFromZohoInventory(string contacts);
		public ZohoInventoryContactsResponseModel GetContactsFromZohoInventory();
	}
}
