using FPVenturesZohoInventoryVendorCredits.Constants;
using FPVenturesZohoInventoryVendorCredits.Helpers;
using FPVenturesZohoInventoryVendorCredits.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FPVenturesZohoInventoryVendorCredits.Services.Mapper
{
    public class ModelMapper
    {
        /// <summary>
        /// Maps vendor credits for ZOHO Inventory
        /// </summary>
        /// <param name="inventoryItems">List of inventory items</param>
        /// <param name="vendorsCRM">List of CRM Vendors</param>
        /// <param name="vendorsInventory">List of Inventory Vendors</param>
        /// <param name="dispositions">List of Dispositions from CRM</param>
        /// <param name="startDate">Start date for custom field</param>
        /// <param name="endDate">End date for custom field</param>
        /// <param name="logger">Logger object</param>
        /// <returns></returns>
        public static List<ZohoInventoryPostVendorCreditModel> MapVendorCreditsModel(List<InventoryItem> inventoryItems, List<VendorCRM> vendorsCRM, List<VendorInventory> vendorsInventory, List<DispositionModel> dispositions, DateTime startDate, DateTime endDate, ILogger logger)
        {
            // Filtering refundable items
            var refundItems = inventoryItems.Where(x => dispositions.Any(y => y.ANI == x.CfCallerId) && x.PurchaseRate != 0).ToList();
            List<ZohoInventoryPostVendorCreditModel> zohoInventoryPostVendorCreditModels = new();

            foreach (var vendor in vendorsInventory)
            {
                try
                {
                    ZohoInventoryPostVendorCreditModel zohoInventoryPostVendorCreditModel = new();

                    // Fetching Inventory Vendor for its ID
                    var vendorInventory = vendorsInventory.Where(x => x.VendorName == vendor.VendorName).FirstOrDefault();
                    if (vendorInventory == null)
                        continue;

                    zohoInventoryPostVendorCreditModel.vendor_id = vendorInventory.ContactId;
                    List<LineItem> lineItems = new();

                    var items = refundItems.Where(x => vendorsCRM.Any(y => x.CfPublisherName != null && y.PublisherName != null && y.PublisherName.ToLower().Contains(x.CfPublisherName.ToLower()) && y.VendorName.ToLower() == vendor.VendorName.ToLower())).ToList();
                    if (!items.Any())
                    {
                        continue;
                    }

                    //Grouping items by publishers
                    var refundItemGroups = items.GroupBy(x => x.CfPublisherName).ToList();

                    foreach (var refundItemGroup in refundItemGroups)
                    {
                        // Further creating subgroups based on purchase rate
                        var refundItemSubGroups = refundItemGroup.Where(x => x.PurchaseRate != 0).GroupBy(t => t.PurchaseRate).ToList();
                        foreach (var refundItemSubGroup in refundItemSubGroups)
                        {
                            LineItem lineItem = new();
                            lineItem.Name = refundItemGroup.Key;
                            lineItem.Quantity = refundItemSubGroup.Count();
                            lineItem.Rate = Convert.ToString(refundItemSubGroup.FirstOrDefault().PurchaseRate);
                            lineItems.Add(lineItem);
                        }
                    }
                    zohoInventoryPostVendorCreditModel.line_items = lineItems;

                    List<CustomField> customFields = new()
                    {
                        new CustomField { Label = ZohoInventoryCustomFields.StartDate, Value = startDate.ToInventoryDate() },
                        new CustomField { Label = ZohoInventoryCustomFields.EndDate, Value = endDate.ToInventoryDate() }
                    };

                    zohoInventoryPostVendorCreditModel.CustomFields = customFields;

                    zohoInventoryPostVendorCreditModels.Add(zohoInventoryPostVendorCreditModel);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex.Message);
                    logger.LogWarning("Method Name -" + ex.GetMethodName());
                    logger.LogWarning(ex.InnerException.ToString());
                }
            }


            return zohoInventoryPostVendorCreditModels;
        }
    }
}
