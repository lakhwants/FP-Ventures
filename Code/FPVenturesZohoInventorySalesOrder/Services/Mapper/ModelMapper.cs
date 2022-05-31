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
            zohoInventorySalesOrderModel.InvoicedStatus = "invoiced";

            List<LineItem> lineItemList = new();

            var groups = inventoryItems.Where(x => x.CfCampaignName != null && x.CfCampaignName.Contains("Hawx")).GroupBy(t => t.CfCampaignName).ToList();

            foreach (var group in groups)
            {
                LineItem lineItem = new();

                lineItem.Name = group.FirstOrDefault().CfCampaignName + " Bid";
                var purchaseRate = group.Where(x => x.PurchaseRate != 0).FirstOrDefault();

                if (purchaseRate == null)
                    continue;
                else
                    lineItem.Rate = Convert.ToString(purchaseRate.PurchaseRate);

                lineItem.Quantity = group.Where(x => x.PurchaseRate != 0).Count();
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
