using FPVenturesZohoInventoryPurchaseOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventoryPurchaseOrder.Services.Mapper
{
	public class ModelMapper
	{
		public static ZohoInventoryPurchaseOrderRequestModel MapItemsForPurchaseOrder(List<InventoryItem> inventoryItems)
		{
			ZohoInventoryPurchaseOrderRequestModel zohoInventoryPurchaseOrderRequestModel = new();
			List<LineItem> lineItems = new();
			foreach (var inventoryItem in inventoryItems)
			{
				LineItem lineItem = new();
				lineItem.item_id = inventoryItem.ItemId;
				lineItems.Add(lineItem);
			}
			zohoInventoryPurchaseOrderRequestModel.line_items = lineItems;
			return zohoInventoryPurchaseOrderRequestModel;
		}

		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}
