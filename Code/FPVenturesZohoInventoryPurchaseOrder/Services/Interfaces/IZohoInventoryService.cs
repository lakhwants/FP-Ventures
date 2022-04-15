using FPVenturesZohoInventoryPurchaseOrder.Models;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryPurchaseOrder.Services.Interfaces
{
	public interface IZohoInventoryService
	{
		public List<InventoryItem> GetInventoryItems(List<Data> zohoLeads);
		public ZohoInventoryPurchaseOrderResponseModel PostPurchaseOrdertoZohoInventory(ZohoInventoryPurchaseOrderModel zohoInventoryPurchaseOrderModel);
	}
}
