using FPVenturesZohoInventorySalesOrder.Models;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Services.Interfaces
{
	public interface IZohoInventoryService
	{
		public ZohoInventorySalesOrderResponseModel AddSalesOrdertoZohoInventory(ZohoInventorySalesOrderModel zohoInventorySalesOrderModel);
		public ZohoInventoryTaxesModel GetZohoInventoryTaxes();
		public List<InventoryItem> GetInventoryItems(List<Data> zohoLeads);
	}
}
