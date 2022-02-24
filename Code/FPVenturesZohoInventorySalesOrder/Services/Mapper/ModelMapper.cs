using FPVenturesZohoInventorySalesOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventorySalesOrder.Services.Mapper
{
	public class ModelMapper
	{
		public static ZohoInventorySalesOrderModel MapItemsForSalesOrder(List<InventoryItem> inventoryItems, ZohoInventoryTaxesModel zohoInventoryTaxesModel)
		{
			ZohoInventorySalesOrderModel zohoInventorySalesOrderModel = new();
			zohoInventorySalesOrderModel.customer_id = 2762310000000184497;
			List<LineItem> lineItemList = new();
			foreach (var inventoryItem in inventoryItems)
			{
				LineItem lineItem = new();
				lineItem.description = inventoryItem.description;
				lineItem.item_id = inventoryItem.item_id;
				lineItem.name = inventoryItem.name;
				lineItem.sku = inventoryItem.sku;
				lineItem.tax_id = zohoInventoryTaxesModel.taxes.FirstOrDefault().tax_id;
				lineItemList.Add(lineItem);
			}
			zohoInventorySalesOrderModel.line_items = lineItemList;
			return zohoInventorySalesOrderModel;
		}

		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}
