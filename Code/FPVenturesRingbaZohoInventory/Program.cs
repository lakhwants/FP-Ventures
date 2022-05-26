using FPVenturesRingbaZohoInventory.Models;
using FPVenturesRingbaZohoInventory.Services;
using FPVenturesRingbaZohoInventory.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesRingbaZohoInventory
{
    public class Program
    {
        public static void Main()
        {
			RingbaZohoConfigurationSettings ringbaZohoConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(ringbaZohoConfigurationSettings);
					services.AddSingleton<IRingbaService, RingbaService>();
					services.AddSingleton<IZohoInventoryService, ZohoInventoryService>();
					services.AddSingleton<IZohoLeadsService, ZohoLeadsService>();

				})
				.Build();

			host.Run();

		}
		
		private static RingbaZohoConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				RingbaBaseUrl = Environment.GetEnvironmentVariable("RingbaBaseUrl") ?? string.Empty,
				RingbaAccessToken = Environment.GetEnvironmentVariable("RingbaAccessToken") ?? string.Empty,
				RingbaAccountsPath = Environment.GetEnvironmentVariable("RingbaAccountsPath") ?? string.Empty,
				RingbaCallLogDetailsPath = Environment.GetEnvironmentVariable("RingbaCallLogDetailsPath") ?? string.Empty,
				RingbaCallLogsPath = Environment.GetEnvironmentVariable("RingbaCallLogsPath") ?? string.Empty,
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				ZohoCRMRefreshToken = Environment.GetEnvironmentVariable("ZohoCRMRefreshToken") ?? string.Empty,
				ZohoInventoryBaseUrl = Environment.GetEnvironmentVariable("ZohoInventoryBaseUrl") ?? string.Empty,
				ZohoInventoryAddItemPath = Environment.GetEnvironmentVariable("ZohoInventoryAddItemPath") ?? string.Empty,
				ZohoInventoryRefreshToken = Environment.GetEnvironmentVariable("ZohoInventoryRefreshToken") ?? string.Empty,
				ZohoCRMBaseUrl = Environment.GetEnvironmentVariable("ZohoCRMBaseUrl") ?? string.Empty,
				ZohoCRMVendorsPath = Environment.GetEnvironmentVariable("ZohoCRMVendorsPath") ?? string.Empty,
				ZohoInventoryItemGroupsPath = Environment.GetEnvironmentVariable("ZohoInventoryItemGroupsPath") ?? string.Empty,
				ZohoInventoryVendorsPath = Environment.GetEnvironmentVariable("ZohoInventoryVendorsPath") ?? string.Empty,
				ZohoInventoryOrganizationId = Environment.GetEnvironmentVariable("ZohoInventoryOrganizationId") ?? string.Empty,
			};
		}
	}
}