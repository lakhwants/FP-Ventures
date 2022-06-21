using FPVenturesZohoInventoryVendorCredits.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventoryVendorCredits.Services.Mapper
{
    public class ModelMapper
    {
        public static List<ZohoInventoryPostVendorCreditModel> MapVendorCreditsModel(List<InventoryItem> inventoryItems, List<VendorCRM> vendorsCRM, List<VendorInventory> vendorsInventory, List<DispositionModel> dispositions)
        {
            var refundItems = inventoryItems.Where(x => dispositions.Any(y => y.ANI == x.CfCallerId) && x.PurchaseRate != 0).ToList();
            List<ZohoInventoryPostVendorCreditModel> zohoInventoryPostVendorCreditModels = new();

            foreach (var vendor in vendorsInventory)
            {

                try
                {
                    ZohoInventoryPostVendorCreditModel zohoInventoryPostVendorCreditModel = new();
                    zohoInventoryPostVendorCreditModel.vendor_id = vendorsInventory.Where(x => x.VendorName == vendor.VendorName).FirstOrDefault().VendorName;
                    List<LineItem> lineItems = new();
                    var items = refundItems.Where(x => vendorsCRM.Any(y => x.CfPublisherName != null && y.PublisherName != null && y.PublisherName.Contains(x.CfPublisherName) && y.VendorName == vendor.VendorName)).ToList();
                    if (!items.Any())
                    {
                        continue;
                    }
                    var refundItemGroups = items.GroupBy(x => x.CfPublisherName).ToList();

                    foreach (var refundItemGroup in refundItemGroups)
                    {
                        LineItem lineItem = new();
                        lineItem.Name = refundItemGroup.Key;
                        lineItem.Quantity = refundItemGroup.Count();
                        lineItem.Rate = Convert.ToString(refundItemGroup.Sum(x => x.PurchaseRate));
                        lineItems.Add(lineItem);
                    }
                    zohoInventoryPostVendorCreditModel.line_items = lineItems;
                    zohoInventoryPostVendorCreditModels.Add(zohoInventoryPostVendorCreditModel);
                }
                catch (Exception ex)
                {

                    throw;
                }
            }

            return zohoInventoryPostVendorCreditModels;
        }

        public static string GetDateString(DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }
    }
}
