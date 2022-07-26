using FPVenturesRingbaZohoInventory.Services;
using FPVenturesRingbaZohoInventoryService.Constants;
using FPVenturesRingbaZohoInventoryService.Models;
using FPVenturesRingbaZohoInventoryService.Services.Interfaces;
using FPVenturesRingbaZohoInventoryService.Services.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FPVenturesRingbaZohoInventoryService
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        static void Main(string[] args)
        {

            Configuration = new ConfigurationBuilder().
               SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
           .AddJsonFile("local.settings.json", false)
           .Build();

            RingbaZohoConfigurationSettings ringbaZohoConfigurationSettings = GetConfigurationSettings();

            var serviceProvider = new ServiceCollection()
                                    .AddSingleton(ringbaZohoConfigurationSettings)
                                    .AddSingleton<IRingbaService, RingbaService>()
                                   .AddSingleton<IZohoInventoryService, ZohoInventoryService>()
                                   .AddSingleton<IZohoLeadsService, ZohoLeadsService>()
                                  .BuildServiceProvider();


            var s = Configuration.GetSection("RingbaAccessToken").Value;

            var _ringbaService = serviceProvider.GetService<IRingbaService>();
            var _zohoInventoryService = serviceProvider.GetService<IZohoInventoryService>();
            var _zohoLeadsService = serviceProvider.GetService<IZohoLeadsService>();

            //DateTime endDate = new DateTime(2021, 12, 2);
            //DateTime startDate = endDate.AddDays(-10);
            DateTime endDate = new DateTime(2022, 7, 19);
            DateTime startDate = new DateTime(2022, 7, 13);



            List<Record> callLogs;
            callLogs = _ringbaService.GetCallLogs(startDate, endDate);

            var detailedCallLogWithoutSKU = _ringbaService.GetCallLogDetails(callLogs);


            var vendorsCRM = _zohoLeadsService.GetVendors();


            var vendorInventory = _zohoInventoryService.GetVendors();


            MapSKU(detailedCallLogWithoutSKU, vendorsCRM);


            var existingItems = _zohoInventoryService.GetInventoryItems(startDate, endDate);

            var detailedCallLogs = detailedCallLogWithoutSKU.Where(x => !existingItems.Any(y => y.SKU == x.SKU)).ToList();

            var callLogGroupsByPublishers = detailedCallLogs.GroupBy(x => x.PublisherName).ToList();




            var itemGroupList = _zohoInventoryService.GetItemGroupsList();


            var groupsToAdd = callLogGroupsByPublishers.Where(x => !itemGroupList.ItemGroups.Any(y => y.GroupName == x.Key)).ToList();


            var newGroups = ModelMapper.MapNewItemGroups(groupsToAdd);

            var addedGroups = _zohoInventoryService.CreateItemGroups(newGroups);


            var inventoryItems = ModelMapper.MapRingbaCallsToZohoInventoryItems(callLogGroupsByPublishers, _zohoInventoryService.GetItemGroupsList(), vendorInventory, vendorsCRM);


            var zohoInventoryResponseModel = _zohoInventoryService.AddLeadsToZohoInventory(inventoryItems);

            _zohoInventoryService.DeleteItem(addedGroups);
        }

        private static void MapSKU(List<Record> ringbaRecords, ZohoCRMVendorsResponseModel zohoCRMVendorsResponseModel)
        {
            foreach (var ringbaCallLog in ringbaRecords)
            {

                var vendor = zohoCRMVendorsResponseModel.Data.Where(x => x.PublisherName != null && x.PublisherName.Contains(ringbaCallLog.PublisherName)).FirstOrDefault();

                ringbaCallLog.SKU = CreateSKU(ringbaCallLog, vendor);

            }
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
        private static RingbaZohoConfigurationSettings GetConfigurationSettings()
        {
            return new()
            {
                RingbaBaseUrl = Configuration.GetSection("RingbaBaseUrl").Value ?? string.Empty,
                RingbaAccessToken = Configuration.GetSection("RingbaAccessToken").Value ?? string.Empty,
                RingbaAccountsPath = Configuration.GetSection("RingbaAccountsPath").Value ?? string.Empty,
                RingbaCallLogDetailsPath = Configuration.GetSection("RingbaCallLogDetailsPath").Value ?? string.Empty,
                RingbaCallLogsPath = Configuration.GetSection("RingbaCallLogsPath").Value ?? string.Empty,
                ZohoAccessTokenFromRefreshTokenPath = Configuration.GetSection("ZohoAccessTokenFromRefreshTokenPath").Value ?? string.Empty,
                ZohoClientId = Configuration.GetSection("ZohoClientId").Value ?? string.Empty,
                ZohoClientSecret = Configuration.GetSection("ZohoClientSecret").Value ?? string.Empty,
                ZohoCRMRefreshToken = Configuration.GetSection("ZohoCRMRefreshToken").Value ?? string.Empty,
                ZohoInventoryBaseUrl = Configuration.GetSection("ZohoInventoryBaseUrl").Value ?? string.Empty,
                ZohoInventoryAddItemPath = Configuration.GetSection("ZohoInventoryAddItemPath").Value ?? string.Empty,
                ZohoInventoryRefreshToken = Configuration.GetSection("ZohoInventoryRefreshToken").Value ?? string.Empty,
                ZohoCRMBaseUrl = Configuration.GetSection("ZohoCRMBaseUrl").Value ?? string.Empty,
                ZohoCRMVendorsPath = Configuration.GetSection("ZohoCRMVendorsPath").Value ?? string.Empty,
                ZohoInventoryItemGroupsPath = Configuration.GetSection("ZohoInventoryItemGroupsPath").Value ?? string.Empty,
                ZohoInventoryVendorsPath = Configuration.GetSection("ZohoInventoryVendorsPath").Value ?? string.Empty,
                ZohoInventoryOrganizationId = Configuration.GetSection("ZohoInventoryOrganizationId").Value ?? string.Empty,
            };
        }
    }
}
