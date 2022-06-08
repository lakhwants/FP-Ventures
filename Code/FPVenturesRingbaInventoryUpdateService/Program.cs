using FPVenturesRingbaInventoryUpdateService.Models;
using FPVenturesRingbaInventoryUpdateService.Services;
using FPVenturesRingbaInventoryUpdateService.Services.Interfaces;
using FPVenturesRingbaInventoryUpdateService.Services.Mapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace FPVenturesRingbaInventoryUpdateService
{
    class Program
    {
        public static IConfigurationRoot Configuration;
        static void Main(string[] args)
        {

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("local.settings.json", false)
                .Build();

            RingbaZohoConfigurationSettings ringbaZohoConfigurationSettings = GetConfigurationSettings();

            var serviceProvider = new ServiceCollection()
                                    .AddSingleton(ringbaZohoConfigurationSettings)
                                    .AddSingleton<IRingbaService, RingbaService>()
                                   .AddSingleton<IZohoInventoryService, ZohoInventoryService>()
                                   .AddSingleton<IZohoLeadsService, ZohoLeadsService>()
                                  .BuildServiceProvider();


            var _ringbaService = serviceProvider.GetService<IRingbaService>();
            var _zohoInventoryService = serviceProvider.GetService<IZohoInventoryService>();
            var _zohoLeadsService = serviceProvider.GetService<IZohoLeadsService>();

            DateTime startDate = new DateTime(2022, 5, 1);
            DateTime endDate = startDate.AddDays(7);

            List<Record> callLogs;
            callLogs = _ringbaService.GetCallLogs(startDate, endDate);

            var detailedCallLogs = _ringbaService.GetCallLogDetails(callLogs);

            var vendors = _zohoLeadsService.GetVendors();
            var ringbaCalls = ModelMapper.CreateSKUForCallLogs(detailedCallLogs,vendors);

            var inventoryItems = _zohoInventoryService.GetInventoryItems(startDate,endDate);

            var itemsToUpdate = ModelMapper.MapRingbaCallsToZohoInventoryItems(ringbaCalls, inventoryItems, vendors);

            var reponse = _zohoInventoryService.PutLeadsToZohoInventory(itemsToUpdate);




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
