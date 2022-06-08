using FPVenturesRingbaInventoryUpdateService.Constants;
using FPVenturesRingbaInventoryUpdateService.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesRingbaInventoryUpdateService.Services.Mapper
{
    public class ModelMapper
    {

        public static List<Record> CreateSKUForCallLogs(List<Record> records, ZohoCRMVendorsResponseModel zohoCRMVendorsResponseModel)
        {
            foreach (var record in records)
            {
                var vendor = zohoCRMVendorsResponseModel.Data.Where(x => x.PublisherName != null && x.PublisherName.Contains(record.PublisherName)).FirstOrDefault();
                record.SKU = CreateSKUForRingbaRecords(record, vendor);
            }
            return records;
        }
        public static List<ZohoInventoryModel> MapRingbaCallsToZohoInventoryItems(List<Record> records, List<InventoryItem> inventoryItems, ZohoCRMVendorsResponseModel zohoCRMVendorsResponseModel)
        {
            List<ZohoInventoryModel> updateZohoInventoryRecords = new();

            foreach (var inventoryItem in inventoryItems)
            {
              
                ZohoInventoryModel zohoInventoryModel = new ZohoInventoryModel();

                var ringbaRecord = records.Where(x => x.SKU == inventoryItem.SKU).FirstOrDefault();

                if (ringbaRecord == null)
                    continue;

                List<CustomField> customFields = new()
                {
                    new CustomField { Label = ZohoInventoryCustomFields.TaggedState, Value = ringbaRecord.TaggedState },
                    new CustomField { Label = ZohoInventoryCustomFields.Date, Value = ringbaRecord.CallDt.Date.ToString("yyyy-MM-dd") },
                };

                var vendor = zohoCRMVendorsResponseModel.Data.Where(x => x.PublisherName != null && x.PublisherName.Contains(ringbaRecord.PublisherName)).FirstOrDefault();

                zohoInventoryModel.Name = string.IsNullOrEmpty(ringbaRecord.TargetName) ? (string.IsNullOrEmpty(ringbaRecord.TargetGroupName) ? ringbaRecord.CampaignName : ringbaRecord.TargetGroupName) : ringbaRecord.TargetName;

                zohoInventoryModel.CustomFields = customFields;
                zohoInventoryModel.SKU = CreateSKU(ringbaRecord, vendor);
                zohoInventoryModel.ItemId = inventoryItem.ItemId;
                updateZohoInventoryRecords.Add(zohoInventoryModel);
            }
            return updateZohoInventoryRecords;
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

        public static string CreateSKUForRingbaRecords(Record ringbaCallLog, VendorCRM vendor)
        {
            string ticks = Convert.ToString(ringbaCallLog.CallDt.Ticks);

            if (vendor == null)
            {
                return "NA" + "_" + RemoveCountryCode(ringbaCallLog.InboundPhoneNumber) + "_" + ticks + "_" + Convert.ToString(TimeSpan.FromSeconds(ringbaCallLog.CallLengthInSeconds)) + "_";
            }

            return vendor.VendorAbbreviation + "_" + RemoveCountryCode(ringbaCallLog.InboundPhoneNumber) + "_" + ticks + "_" + Convert.ToString(TimeSpan.FromSeconds(ringbaCallLog.CallLengthInSeconds)) + "_";
        }
    }
}
