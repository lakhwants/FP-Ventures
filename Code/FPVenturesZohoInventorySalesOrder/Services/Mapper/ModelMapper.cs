using FPVenturesZohoInventorySalesOrder.Constants;
using FPVenturesZohoInventorySalesOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventorySalesOrder.Services.Mapper
{
    public class ModelMapper
    {
        public static ZohoInventorySalesOrderModel MapItemsForSalesOrder(List<InventoryItem> inventoryItems, string customerId, DateTime startDate, DateTime endDate)
        {

            ZohoInventorySalesOrderModel zohoInventorySalesOrderModel = new();
            zohoInventorySalesOrderModel.CustomerId = customerId;
            zohoInventorySalesOrderModel.InvoicedStatus = "invoiced";

            List<LineItem> lineItemList = new();

            var groups = inventoryItems.Where(x => x.CfCampaignName != null && x.CfCampaignName.Contains("Hawx")).GroupBy(t => t.CfCampaignName).ToList();

            var webFormLeads = inventoryItems.Where(x => x.cf_isheyflowlead_unformatted || x.cf_isringbalead_unformatted || x.cf_isunbouncelead_unformatted).GroupBy(z => Convert.ToDateTime(z.cf_datetime).Month).ToList();

            MapRingbaInventoryLeads(lineItemList, groups);

            MapWebFormsInventoryLeads(lineItemList, webFormLeads);

            List<CustomField> customFields = new()
            {
                new CustomField { Label = ZohoInventoryCustomFields.StartDate, Value = startDate.ToString("yyyy-MM-dd") },
                new CustomField { Label = ZohoInventoryCustomFields.EndDate, Value = endDate.ToString("yyyy-MM-dd") }
            };

            zohoInventorySalesOrderModel.CustomFields = customFields;

            zohoInventorySalesOrderModel.LineItems = lineItemList;
            return zohoInventorySalesOrderModel;
        }

        private static void MapRingbaInventoryLeads(List<LineItem> lineItemList, List<IGrouping<string, InventoryItem>> groups)
        {
            foreach (var group in groups)
            {
                var subGroups = group.Where(x => x.PurchaseRate != 0).GroupBy(t => t.PurchaseRate).ToList();
                foreach (var subGroup in subGroups)
                {
                    LineItem lineItem = new();

                    lineItem.Name = group.Key + " Bid";

                    var purchaseRate = subGroup.Where(x => x.PurchaseRate != 0).FirstOrDefault();

                    if (purchaseRate == null)
                        continue;
                    else
                        lineItem.Rate = Convert.ToString(purchaseRate.PurchaseRate);

                    lineItem.Quantity = subGroup.Where(x => x.PurchaseRate != 0).Count();

                    lineItemList.Add(lineItem);
                }
            }
        }

        private static void MapWebFormsInventoryLeads(List<LineItem> lineItemList, List<IGrouping<int, InventoryItem>> webFormLeads)
        {
            foreach (var webFormLead in webFormLeads.OrderBy(x=>x.Key))
            {

                LineItem webLeadsLineItem = new();

                webLeadsLineItem.Name = "NationWide " + (Convert.ToDateTime(webFormLead.FirstOrDefault().cf_datetime)).ToString("MMMM");

                var purchaseRate = 40;

                webLeadsLineItem.Rate = Convert.ToString(purchaseRate);

                webLeadsLineItem.Quantity = webFormLead.Count();

                lineItemList.Add(webLeadsLineItem);
            }
        }

        public static ZohoInventoryInvoiceRequestModel MapSalesOrderToInvoiceModel(ZohoInventorySalesOrderResponseModel zohoInventorySalesOrderResponseModel, ZohoInventoryContactPersonResponseModel zohoInventoryContactPersonResponseModel, DateTime startDate, DateTime endDate)
        {
            ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel = new();
            zohoInventoryInvoiceRequestModel.CustomerId = zohoInventorySalesOrderResponseModel.SalesOrder.CustomerId;
            zohoInventoryInvoiceRequestModel.LineItems = zohoInventorySalesOrderResponseModel.SalesOrder.LineItems;
            zohoInventoryInvoiceRequestModel.ContactPersons = zohoInventoryContactPersonResponseModel.ContactPersons.Select(x => x.ContactPersonId).ToList();
            List<CustomField> customFields = new()
            {
                new CustomField { Label = ZohoInventoryCustomFields.StartDate, Value = startDate.ToString("yyyy-MM-dd") },
                new CustomField { Label = ZohoInventoryCustomFields.EndDate, Value = endDate.ToString("yyyy-MM-dd") }
            };

            zohoInventoryInvoiceRequestModel.CustomFields = customFields;

            return zohoInventoryInvoiceRequestModel;
        }

        public static string GetDateString(DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }
    }
}
