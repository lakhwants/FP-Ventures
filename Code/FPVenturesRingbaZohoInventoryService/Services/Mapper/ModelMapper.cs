using FPVenturesRingbaZohoInventory.Constants;
using FPVenturesRingbaZohoInventoryService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesRingbaZohoInventoryService.Services.Mapper
{
    public class ModelMapper
    {
        public static List<List<ZohoInventoryModel>> MapRingbaCallsToZohoInventoryItems(List<IGrouping<string, Record>> ringbaCallLogsGroups, ZohoInventoryItemGroupsListResponseModel zohoInventoryItemGroupsList, ZohoInventoryVendorsResponseModel zohoInventoryVendorsResponseModel, ZohoCRMVendorsResponseModel zohoCRMVendorsResponseModel)
        {
            List<List<ZohoInventoryModel>> zohoInventoryRecordGroups = new();
            foreach (var ringbaCallLogs in ringbaCallLogsGroups)
            {
                List<ZohoInventoryModel> zohoInventoryRecords = new();

                foreach (var ringbaCallLog in ringbaCallLogs)
                {

                    ZohoInventoryModel zohoInventoryModel = new ZohoInventoryModel();
                    List<CustomField> customFields = new()
                    {
                        new CustomField { Label = ZohoInventoryCustomFields.Name, Value = string.IsNullOrEmpty(ringbaCallLog.TargetName) ? (string.IsNullOrEmpty(ringbaCallLog.TargetGroupName) ? ringbaCallLog.CampaignName : ringbaCallLog.TargetGroupName) : ringbaCallLog.TargetName },
                        new CustomField { Label = ZohoInventoryCustomFields.CampaignName, Value = ringbaCallLog.CampaignName },
                        new CustomField { Label = ZohoInventoryCustomFields.PublisherName, Value = ringbaCallLog.PublisherName },
                        new CustomField { Label = ZohoInventoryCustomFields.InboundCallID, Value = ringbaCallLog.InboundCallId },
                        new CustomField { Label = ZohoInventoryCustomFields.CallerID, Value = RemoveCountryCode(ringbaCallLog.InboundPhoneNumber) },
                        new CustomField { Label = ZohoInventoryCustomFields.Duration, Value = Convert.ToString(TimeSpan.FromSeconds(ringbaCallLog.CallLengthInSeconds)) },
                        new CustomField { Label = ZohoInventoryCustomFields.TaggedState, Value = ringbaCallLog.TaggedState },
                        new CustomField { Label = ZohoInventoryCustomFields.CallDateTime, Value = GetDateString(ringbaCallLog.CallDt) },
                        new CustomField { Label = ZohoInventoryCustomFields.Payout, Value = !string.IsNullOrEmpty(Convert.ToString(ringbaCallLog.PayoutAmount)) ? Convert.ToString(ringbaCallLog.PayoutAmount) : 0.ToString() },
                        new CustomField { Label = ZohoInventoryCustomFields.DialedNumber, Value = ringbaCallLog.TaggedNumber },
                        new CustomField { Label = ZohoInventoryCustomFields.ICPCost, Value = !string.IsNullOrEmpty(Convert.ToString(ringbaCallLog.IcpCost)) ? Convert.ToString(ringbaCallLog.IcpCost) : 0.ToString() },
                        new CustomField { Label = ZohoInventoryCustomFields.Revenue, Value = !string.IsNullOrEmpty(Convert.ToString(ringbaCallLog.ConversionAmount)) ? Convert.ToString(ringbaCallLog.ConversionAmount) : 0.ToString() },
                        new CustomField { Label = ZohoInventoryCustomFields.TaggedCity, Value = ringbaCallLog.TaggedCity },
                        new CustomField { Label = ZohoInventoryCustomFields.TaggedLocationType, Value = ringbaCallLog.TaggedLocationType },
                        new CustomField { Label = ZohoInventoryCustomFields.TaggedNumber, Value = ringbaCallLog.TaggedNumber },
                        new CustomField { Label = ZohoInventoryCustomFields.TaggedNumberType, Value = ringbaCallLog.TaggedNumberType },
                        new CustomField { Label = ZohoInventoryCustomFields.TaggedPhoneProvider, Value = ringbaCallLog.TaggedTelco },
                        new CustomField { Label = ZohoInventoryCustomFields.TaggedTimeZone, Value = ringbaCallLog.TaggedTimeZone },
                        new CustomField { Label = ZohoInventoryCustomFields.TargetID, Value = ringbaCallLog.TargetId },
                        new CustomField { Label = ZohoInventoryCustomFields.TargetName, Value = ringbaCallLog.TargetName },
                        new CustomField { Label = ZohoInventoryCustomFields.TargetNumber, Value = RemoveCountryCode(ringbaCallLog.TargetNumber) },
                        new CustomField { Label = ZohoInventoryCustomFields.TelcoCost, Value = !string.IsNullOrEmpty(Convert.ToString(ringbaCallLog.TelcoCost)) ? Convert.ToString(ringbaCallLog.TelcoCost) : 0.ToString() },
                        new CustomField { Label = ZohoInventoryCustomFields.TotalCost, Value = !string.IsNullOrEmpty(Convert.ToString(ringbaCallLog.TotalCost)) ? Convert.ToString(ringbaCallLog.TotalCost) : 0.ToString() },
                        new CustomField { Label = ZohoInventoryCustomFields.IsDuplicate, Value = ringbaCallLog.IsDuplicate.ToString() },
                        new CustomField { Label = ZohoInventoryCustomFields.WinningBid, Value = !string.IsNullOrEmpty(Convert.ToString(ringbaCallLog.RingTreeWinningBid)) ? Convert.ToString(ringbaCallLog.RingTreeWinningBid) : 0.ToString() },
                        new CustomField { Label = ZohoInventoryCustomFields.WinningBidTargetID, Value = ringbaCallLog.RingTreeWinningBidTargetId },
                        new CustomField { Label = ZohoInventoryCustomFields.WinningBidTargetName, Value = ringbaCallLog.RingTreeWinningBidTargetName },
                        new CustomField { Label = ZohoInventoryCustomFields.LeadSource, Value = ringbaCallLog.PublisherName },
                        new CustomField { Label = ZohoInventoryCustomFields.Date, Value = ringbaCallLog.CallDt.Date.ToString("yyyy-MM-dd") },
                    };
                    zohoInventoryModel.Name = string.IsNullOrEmpty(ringbaCallLog.TargetName) ? (string.IsNullOrEmpty(ringbaCallLog.TargetGroupName) ? ringbaCallLog.CampaignName : ringbaCallLog.TargetGroupName) : ringbaCallLog.TargetName;
                    zohoInventoryModel.ProductType = "goods";
                    zohoInventoryModel.ItemType = "inventory";
                    zohoInventoryModel.Source = "user";
                    zohoInventoryModel.Status = "active";
                    zohoInventoryModel.PurchaseRate = !string.IsNullOrEmpty(Convert.ToString(ringbaCallLog.PayoutAmount)) ? Convert.ToDouble(ringbaCallLog.PayoutAmount) : 0;
                    zohoInventoryModel.Description = "Ringba Lead";
                    zohoInventoryModel.CustomFields = customFields;
                    zohoInventoryModel.PurchaseAccountName = "Cost Of Goods Sold";
                    zohoInventoryModel.IsReturnable = true;
                    zohoInventoryModel.GroupId = zohoInventoryItemGroupsList.ItemGroups.Where(x => x.GroupName == ringbaCallLog.PublisherName).FirstOrDefault().GroupId;
                    zohoInventoryModel.Rate = !string.IsNullOrEmpty(Convert.ToString(ringbaCallLog.ConversionAmount)) ? Convert.ToDouble(ringbaCallLog.ConversionAmount) : 0;
                    var vendor = zohoCRMVendorsResponseModel.Data.Where(x => x.PublisherName != null && x.PublisherName.Contains(ringbaCallLog.PublisherName)).FirstOrDefault();
                    if (vendor != null)
                    {
                        zohoInventoryModel.VendorId = zohoInventoryVendorsResponseModel.Contacts.Where(x => x.ContactName == vendor.VendorName).FirstOrDefault().ContactId;
                    }
                    zohoInventoryModel.SKU = CreateSKU(ringbaCallLog, vendor);
                    zohoInventoryRecords.Add(zohoInventoryModel);
                }
                zohoInventoryRecordGroups.Add(zohoInventoryRecords);
            }
            return zohoInventoryRecordGroups;
        }

        public static List<ZohoInventoryPostItemGroupRequestModel> MapNewItemGroups(List<IGrouping<string, Record>> newItemGroups)
        {
            int counter = 0;
            List<ZohoInventoryPostItemGroupRequestModel> zohoInventoryPostItemGroupRequestModels = new();

            //Demo items as it is mandatory to add an item in the starting. Also first time do not take customfields
            foreach (var newItemGroup in newItemGroups)
            {
                ZohoInventoryPostItemGroupRequestModel zohoInventoryPostItemGroupRequestModel = new();
                zohoInventoryPostItemGroupRequestModel.GroupName = newItemGroup.Key;
                zohoInventoryPostItemGroupRequestModel.Unit = "count";

                List<Item> items = new();
                Item item = new();
                item.Name = "Demo" + newItemGroup.Key;
                item.SKU = "Demo" + newItemGroup.Key;
                item.ProductType = "goods";
                item.Source = "user";
                item.Status = "active";
                item.Rate = 0;
                items.Add(item);

                zohoInventoryPostItemGroupRequestModel.Items = items;

                zohoInventoryPostItemGroupRequestModels.Add(zohoInventoryPostItemGroupRequestModel);
                counter++;
            }

            return zohoInventoryPostItemGroupRequestModels;
        }

        public static string GetDateString(DateTime date)
        {
            return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
        }

        public static string RemoveCountryCode(string phone)
        {
            if (string.IsNullOrEmpty(phone))
                return null;

            foreach (var country in CountryCodes.PhoneCountryCodes)
            {
                phone = phone.Replace(country, "");
            }
            return phone;
        }

        public static string CreateSKU(Record ringbaCallLog, VendorCRM vendor)
        {
            string ticks = Convert.ToString(ringbaCallLog.CallDt.Ticks);

            if (vendor == null)
            {
                return "NA" + "_" + RemoveCountryCode(ringbaCallLog.InboundPhoneNumber) + "_" + ticks + "_" + Convert.ToString(TimeSpan.FromSeconds(ringbaCallLog.CallLengthInSeconds)) + "_" + ringbaCallLog.TaggedState;
            }

            return vendor.VendorAbbreviation + "_" + RemoveCountryCode(ringbaCallLog.InboundPhoneNumber) + "_" + ticks + "_" + Convert.ToString(TimeSpan.FromSeconds(ringbaCallLog.CallLengthInSeconds)) + "_" + ringbaCallLog.TaggedState;
        }
    }
}
