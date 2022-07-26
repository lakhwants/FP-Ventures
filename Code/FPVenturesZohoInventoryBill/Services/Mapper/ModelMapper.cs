using FPVenturesZohoInventoryBill.Constants;
using FPVenturesZohoInventoryBill.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesZohoInventoryBill.Services.Mapper
{
    public class ModelMapper
    {
        public static List<ZohoInventoryBillsModel> MapBillsModel(List<InventoryItem> inventoryItems, List<VendorCRM> vendorsCRM, List<VendorInventory> vendorsInventory, string accountId, DateTime startDate, DateTime endDate)
        {
            MapItemsWithVendors(inventoryItems, vendorsCRM, vendorsInventory);
            List<ZohoInventoryBillsModel> zohoInventoryBillsModels = new();

            var itemGroupsByVendors = inventoryItems.Where(y => y.PurchaseRate != 0 && y.CfCampaignName != null && y.CfCampaignName.Contains("Hawx")).GroupBy(x => x.VendorName).ToList();

            foreach (var itemGroupByVendor in itemGroupsByVendors)
            {
                var groupsByPublisher = itemGroupByVendor.GroupBy(x => x.CfPublisherName).ToList();

                List<LineItem> lineItems = new();
                foreach (var groupByPublisher in groupsByPublisher)
                {
                    var groupsByRate = groupByPublisher.GroupBy(x => x.PurchaseRate).ToList();
                    foreach (var groupByRate in groupsByRate)
                    {
                        LineItem lineItem = new();
                        lineItem.name = groupByPublisher.Key;
                        lineItem.quantity = groupByRate.Count();
                        lineItem.rate = groupByRate.FirstOrDefault().PurchaseRate;
                        lineItem.account_id = accountId;
                        lineItems.Add(lineItem);
                    }
                }

                ZohoInventoryBillsModel zohoInventoryBillsModel = new();
                zohoInventoryBillsModel.line_items = lineItems;
                List<CustomField> customFields = new()
                {
                    new CustomField { Label = ZohoInventoryCustomFields.StartDate, Value = startDate.ToString("yyyy-MM-dd") },
                    new CustomField { Label = ZohoInventoryCustomFields.EndDate, Value = endDate.ToString("yyyy-MM-dd") }
                };
                zohoInventoryBillsModel.CustomFields = customFields;
                zohoInventoryBillsModel.vendor_id = itemGroupByVendor.FirstOrDefault().VendorId;
                zohoInventoryBillsModel.bill_number = itemGroupByVendor.FirstOrDefault().VendorAbbreviation + "-" + Convert.ToString(DateTime.Now.Ticks);
                zohoInventoryBillsModel.date = DateTime.Now.Date.ToString("yyyy-MM-dd");
                zohoInventoryBillsModel.due_date = DateTime.Now.Date.AddDays(7).ToString("yyyy-MM-dd");

                zohoInventoryBillsModels.Add(zohoInventoryBillsModel);
            }
            return zohoInventoryBillsModels;
        }

        private static List<InventoryItem> MapItemsWithVendors(List<InventoryItem> inventoryItems, List<VendorCRM> vendorsCRM, List<VendorInventory> vendorsInventory)
        {
            try
            {
                foreach (var item in inventoryItems)
                {
                    var vendorCRM = vendorsCRM.Where(x => x.PublisherName != null && x.PublisherName.Contains(item.CfPublisherName)).FirstOrDefault();
                    if (vendorCRM != null)
                    {
                        var vendorInventory = vendorsInventory.Where(x => x.ContactName == vendorCRM.VendorName).FirstOrDefault();
                        item.VendorId = vendorInventory.ContactId;
                        item.VendorName = vendorInventory.VendorName;
                        item.VendorAbbreviation = vendorCRM.VendorAbbreviation;
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }

            return inventoryItems;
        }


        public static string GetDateString(DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }
    }
}
