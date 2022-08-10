using FPVenturesZohoInventorySalesOrder.Constants;
using FPVenturesZohoInventorySalesOrder.Helpers;
using FPVenturesZohoInventorySalesOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventorySalesOrder.Services.Mapper
{
    public class ModelMapper
    {
        /// <summary>
        /// Maps Items to the Sales Order Model
        /// </summary>
        /// <param name="inventoryItems">List of ZOHO inventory items</param>
        /// <param name="customerId">Customer Id(We are using HAWX Currently)</param>
        /// <param name="startDate">Start Date of the Sales Order</param>
        /// <param name="endDate">End Date of the Sales Order</param>
        /// <returns></returns>
        public static ZohoInventorySalesOrderModel MapItemsForSalesOrder(List<InventoryItem> inventoryItems, string customerId, DateTime startDate, DateTime endDate)
        {

            ZohoInventorySalesOrderModel zohoInventorySalesOrderModel = new();
            zohoInventorySalesOrderModel.CustomerId = customerId;
            zohoInventorySalesOrderModel.InvoicedStatus = "invoiced";

            List<LineItem> lineItemList = new();

            //Group items by Campaign Name
            //Currently we are creating Sales orders for HAWX only
            var groups = inventoryItems.Where(x => x.CfCampaignName != null && x.CfCampaignName.Contains("Hawx")).GroupBy(t => t.CfCampaignName).ToList();

            //Fetching all the webform leads and grouping them by month
            var webFormLeads = inventoryItems.Where(x => x.cf_isheyflowlead_unformatted || x.cf_isringbalead_unformatted || x.cf_isunbouncelead_unformatted).GroupBy(z => Convert.ToDateTime(z.cf_datetime).Month).ToList();

            MapRingbaInventoryLeads(lineItemList, groups);

            MapWebFormsInventoryLeads(lineItemList, webFormLeads);

            //Adding values to the Date custom field that indicates the Start and End date of the Sales Order
            List<CustomField> customFields = new()
            {
                new CustomField { Label = ZohoInventoryCustomFields.StartDate, Value = startDate.ToInventoryDate() },
                new CustomField { Label = ZohoInventoryCustomFields.EndDate, Value = endDate.ToInventoryDate() }
            };

            zohoInventorySalesOrderModel.CustomFields = customFields;

            zohoInventorySalesOrderModel.LineItems = lineItemList;
            return zohoInventorySalesOrderModel;
        }

        /// <summary>
        /// Map Ringba leads to line items
        /// </summary>
        /// <param name="lineItemList">List of Line Items</param>
        /// <param name="groups">Groups of Inventory Items based on Campaigns</param>
        private static void MapRingbaInventoryLeads(List<LineItem> lineItemList, List<IGrouping<string, InventoryItem>> groups)
        {
            foreach (var group in groups)
            {
                //Creating sub groups on the basis of Purchase Rate
                var subGroups = group.Where(x => x.PurchaseRate != 0).GroupBy(t => t.PurchaseRate).ToList();
                foreach (var subGroup in subGroups)
                {
                    LineItem lineItem = new();

                    lineItem.Name = group.Key + " Bid";

                    //Further creating subgroups on the basis of Purchase rate
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

        /// <summary>
        /// Map Webform leads to line items
        /// </summary>
        /// <param name="lineItemList">List of line items</param>
        /// <param name="webFormLeads">Group of Inventory items(Webform leads) based on month</param>
        private static void MapWebFormsInventoryLeads(List<LineItem> lineItemList, List<IGrouping<int, InventoryItem>> webFormLeads)
        {
            foreach (var webFormLead in webFormLeads.OrderBy(x => x.Key))
            {

                LineItem webLeadsLineItem = new();

                webLeadsLineItem.Name = "NationWide " + (Convert.ToDateTime(webFormLead.FirstOrDefault().cf_datetime)).ToString("MMMM");

                // Hardcoded as all the web leads should have a rate of 40 and may require to change at anytime based
                //based on client request
                var purchaseRate = 40;

                webLeadsLineItem.Rate = Convert.ToString(purchaseRate);

                webLeadsLineItem.Quantity = webFormLead.Count();

                lineItemList.Add(webLeadsLineItem);
            }
        }

        /// <summary>
        /// Maps SalesOrder model to the Invoice model
        /// </summary>
        /// <param name="zohoInventorySalesOrderResponseModel">ZOHO Inventory Sales Order Model</param>
        /// <param name="zohoInventoryContactPersonResponseModel">ZOHO Inventory Contact person Model</param>
        /// <param name="startDate">Start date for the SalesOrder</param>
        /// <param name="endDate">End date for the SalesOrder</param>
        /// <returns>Returns ZOHO Inventory Invoice Model</returns>
        public static ZohoInventoryInvoiceRequestModel MapSalesOrderToInvoiceModel(ZohoInventorySalesOrderResponseModel zohoInventorySalesOrderResponseModel, ZohoInventoryContactPersonResponseModel zohoInventoryContactPersonResponseModel, DateTime startDate, DateTime endDate)
        {
            ZohoInventoryInvoiceRequestModel zohoInventoryInvoiceRequestModel = new();
            zohoInventoryInvoiceRequestModel.CustomerId = zohoInventorySalesOrderResponseModel.SalesOrder.CustomerId;
            zohoInventoryInvoiceRequestModel.LineItems = zohoInventorySalesOrderResponseModel.SalesOrder.LineItems;
            zohoInventoryInvoiceRequestModel.ContactPersons = zohoInventoryContactPersonResponseModel.ContactPersons.Select(x => x.ContactPersonId).ToList();
            List<CustomField> customFields = new()
            {
                new CustomField { Label = ZohoInventoryCustomFields.StartDate, Value = startDate.ToInventoryDate() },
                new CustomField { Label = ZohoInventoryCustomFields.EndDate, Value = endDate.ToInventoryDate() }
            };

            zohoInventoryInvoiceRequestModel.CustomFields = customFields;

            return zohoInventoryInvoiceRequestModel;
        }
    }
}
