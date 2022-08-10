using FPVenturesZohoInventoryBill.Constants;
using FPVenturesZohoInventoryBill.Helpers;
using FPVenturesZohoInventoryBill.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventoryBill.Services.Mapper
{
    public class ModelMapper
    {
        /// <summary>
        /// Create bills for adding into the ZOHO Inventory
        /// </summary>
        /// <param name="inventoryItems">List of inventory items</param>
        /// <param name="vendorsCRM">List of ZOHO CRM Vendors</param>
        /// <param name="vendorsInventory">List of ZOHO Inventory Vendors</param>
        /// <param name="accountId">Id of the account in which finances of each bill's items will be added</param>
        /// <param name="startDate">Start date for the custom field present in the bills</param>
        /// <param name="endDate">End date for the custom field present in the bills</param>
        /// <returns>List models of ZOHO inventory Bills</returns>
        public static List<ZohoInventoryBillsModel> MapBillsModel(List<InventoryItem> inventoryItems, List<VendorCRM> vendorsCRM, List<VendorInventory> vendorsInventory, string accountId, DateTime startDate, DateTime endDate, ILogger logger)
        {
            MapItemsWithVendors(inventoryItems, vendorsCRM, vendorsInventory, logger);
            List<ZohoInventoryBillsModel> zohoInventoryBillsModels = new();

            //Remove all the items that have 0 Payout or PurchaseRate
            //Group items by their vendors
            var itemGroupsByVendors = inventoryItems.Where(y => y.PurchaseRate != 0 && y.CfCampaignName != null && y.CfCampaignName.Contains("Hawx")).GroupBy(x => x.VendorName).ToList();

            foreach (var itemGroupByVendor in itemGroupsByVendors)
            {
                //Further grroup vendors by their Publisher name
                var groupsByPublisher = itemGroupByVendor.GroupBy(x => x.CfPublisherName).ToList();

                List<LineItem> lineItems = new();
                foreach (var groupByPublisher in groupsByPublisher)
                {
                    //Further group vendors by their PurchaseRate as PurchaseRate can be changed according to requirement
                    var groupsByRate = groupByPublisher.GroupBy(x => x.PurchaseRate).ToList();
                    foreach (var groupByRate in groupsByRate)
                    {
                        LineItem lineItem = new();
                        lineItem.Name = groupByPublisher.Key;
                        lineItem.Quantity = groupByRate.Count();
                        lineItem.Rate = groupByRate.FirstOrDefault().PurchaseRate;
                        lineItem.AccountId = accountId;
                        lineItems.Add(lineItem);
                    }
                }

                ZohoInventoryBillsModel zohoInventoryBillsModel = new();
                zohoInventoryBillsModel.LineItems = lineItems;
                List<CustomField> customFields = new()
                {
                    new CustomField { Label = ZohoInventoryCustomFields.StartDate, Value = startDate.ToInventoryDate() },
                    new CustomField { Label = ZohoInventoryCustomFields.EndDate, Value = endDate.ToInventoryDate() }
                };
                zohoInventoryBillsModel.CustomFields = customFields;
                zohoInventoryBillsModel.VendorId = itemGroupByVendor.FirstOrDefault().VendorId;

                //Bill number is not auto generated and should be unique
                zohoInventoryBillsModel.BillNumber = itemGroupByVendor.FirstOrDefault().VendorAbbreviation + "-" + Convert.ToString(DateTime.Now.Ticks);
                zohoInventoryBillsModel.Date = DateTime.Now.Date.ToInventoryDate();
                zohoInventoryBillsModel.DueDate = DateTime.Now.Date.AddDays(7).ToInventoryDate();

                zohoInventoryBillsModels.Add(zohoInventoryBillsModel);
            }
            return zohoInventoryBillsModels;
        }

        /// <summary>
        /// Adds VendorID, VendorName and VendorAbbreviation to each list item
        /// </summary>
        /// <param name="inventoryItems">List of inventory items</param>
        /// <param name="vendorsCRM">List of ZOHO CRM Vendors</param>
        /// <param name="vendorsInventory">List of ZOHO Inventory Vendors</param>
        /// <returns></returns>
        private static List<InventoryItem> MapItemsWithVendors(List<InventoryItem> inventoryItems, List<VendorCRM> vendorsCRM, List<VendorInventory> vendorsInventory, ILogger logger)
        {
            try
            {
                foreach (var item in inventoryItems)
                {
                    //Gets the CRM's vendor for the item using the publisher name
                    //Publisher name is matched with the Publisher name custom field of the Vendor that contains all the publisher names of the given Vendor
                    //Check CRM's Vendor module for the publisher name field
                    var vendorCRM = vendorsCRM.Where(x => x.PublisherName != null && x.PublisherName.Contains(item.CfPublisherName)).FirstOrDefault();
                    if (vendorCRM != null)
                    {
                        //Get the Inventory vendor using the CRM vendor's VendorName to fetch the inventory vendor's ID
                        var vendorInventory = vendorsInventory.Where(x => x.ContactName == vendorCRM.VendorName).FirstOrDefault();
                        item.VendorId = vendorInventory.ContactId;
                        item.VendorName = vendorInventory.VendorName;
                        item.VendorAbbreviation = vendorCRM.VendorAbbreviation;
                    }
                }

            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                logger.LogWarning("Method Name -" + ex.GetMethodName());
                logger.LogWarning(ex.InnerException.ToString());
            }

            return inventoryItems;
        }

    }
}
