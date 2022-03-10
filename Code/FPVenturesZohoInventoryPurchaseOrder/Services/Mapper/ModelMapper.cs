using FPVenturesZohoInventorySalesOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventorySalesOrder.Services.Mapper
{
	public class ModelMapper
	{
		public static ZohoInventorySalesOrderModel MapItemsForSalesOrder(List<InventoryItem> inventoryItems, ZohoInventoryTaxesModel zohoInventoryTaxesModel, string customerId)
		{
			ZohoInventorySalesOrderModel zohoInventorySalesOrderModel = new();
			zohoInventorySalesOrderModel.CustomerId = customerId;
			List<LineItem> lineItemList = new();
			foreach (var inventoryItem in inventoryItems)
			{
				LineItem lineItem = new();
				lineItem.Description = inventoryItem.Description;
				lineItem.ItemId = inventoryItem.ItemId;
				lineItem.Name = inventoryItem.Name;
				lineItem.SKU = inventoryItem.SKU;
				lineItem.TaxId = zohoInventoryTaxesModel.Taxes.FirstOrDefault().TaxId;
				lineItemList.Add(lineItem);
			}
			zohoInventorySalesOrderModel.LineItems = lineItemList;
			return zohoInventorySalesOrderModel;
		}

		public static ZohoInventoryInvoiceRequestModel MapSalesOrderToInvoiceModel(ZohoInventorySalesOrderResponseModel zohoInventorySalesOrderResponseModel, ZohoInventoryContactPersonResponseModel zohoInventoryContactPersonResponseModel)
		{
			ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel = new();
			zohoInventoryInvoiceRequestModel.CustomerId = zohoInventorySalesOrderResponseModel.SalesOrder.CustomerId;
			zohoInventoryInvoiceRequestModel.LineItems = zohoInventorySalesOrderResponseModel.SalesOrder.LineItems;
			zohoInventoryInvoiceRequestModel.ContactPersons = zohoInventoryContactPersonResponseModel.ContactPersons.Select(x => x.ContactPersonId).ToList();

			return zohoInventoryInvoiceRequestModel;
		}

		public static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}
	}
}
