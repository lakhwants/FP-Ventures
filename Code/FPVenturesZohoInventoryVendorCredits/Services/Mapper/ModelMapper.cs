using FPVenturesZohoInventoryVendorCredits.Constants;
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

        public static List<ZohoInventoryPostVendorCreditModel> MapVendorCreditsModel(List<InventoryItem> inventoryItems, List<VendorCRM> vendorsCRM, List<VendorInventory> vendorsInventory, List<DispositionModel> dispositions, DateTime startDate, DateTime endDate, ILogger logger)
        {

            var refundItems = inventoryItems.Where(x => dispositions.Any(y => y.ANI == x.CfCallerId) && x.PurchaseRate != 0).ToList();
            List<ZohoInventoryPostVendorCreditModel> zohoInventoryPostVendorCreditModels = new();

            foreach (var vendor in vendorsInventory)
            {
                try
                {
                    ZohoInventoryPostVendorCreditModel zohoInventoryPostVendorCreditModel = new();

                    //var vendorInventory = vendorsInventory.Where(x => x.VendorName == vendor.VendorName).FirstOrDefault();
                    //if (vendorInventory == null)
                    //    continue;

                    zohoInventoryPostVendorCreditModel.vendor_id = vendorsInventory.Where(x => x.VendorName == vendor.VendorName).FirstOrDefault().ContactId;
                    List<LineItem> lineItems = new();
                    var items = refundItems.Where(x => vendorsCRM.Any(y => x.CfPublisherName != null && y.PublisherName != null && y.PublisherName.ToLower().Contains(x.CfPublisherName.ToLower()) && y.VendorName.ToLower() == vendor.VendorName.ToLower())).ToList();
                    if (!items.Any())
                    {
                        continue;
                    }
                    var refundItemGroups = items.GroupBy(x => x.CfPublisherName).ToList();

                    foreach (var refundItemGroup in refundItemGroups)
                    {
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
                        new CustomField { Label = ZohoInventoryCustomFields.StartDate, Value = startDate.ToString("yyyy-MM-dd") },
                        new CustomField { Label = ZohoInventoryCustomFields.EndDate, Value = endDate.ToString("yyyy-MM-dd") }
                    };

                    zohoInventoryPostVendorCreditModel.CustomFields = customFields;

                    zohoInventoryPostVendorCreditModels.Add(zohoInventoryPostVendorCreditModel);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex.Message);
                    logger.LogWarning("Method Name -" + new StackTrace(ex).GetFrame(0).GetMethod().Name);
                    logger.LogWarning(ex.InnerException.ToString());
                }
            }


            return zohoInventoryPostVendorCreditModels;
        }

        public static string GetDateString(DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss+00:00");
        }
    }
}
