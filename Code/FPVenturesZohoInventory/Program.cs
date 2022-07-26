using FPVenturesZohoInventory.Models;
using FPVenturesZohoInventory.Services;
using FPVenturesZohoInventory.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesZohoInventory
{
	public class Program
	{
		public static void Main()
		{
			ZohoCRMAndInventoryConfigurationSettings zohoCRMAndInventoryConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(zohoCRMAndInventoryConfigurationSettings);
					services.AddSingleton<IZohoInventoryService, ZohoInventoryService>();
					services.AddSingleton<IZohoLeadsService, ZohoLeadsService>();
				})
				.Build();

			host.Run();
		}

		private static ZohoCRMAndInventoryConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoLeadsBaseUrl = Environment.GetEnvironmentVariable("ZohoLeadsBaseUrl") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				ZohoRefreshToken = Environment.GetEnvironmentVariable("ZohoRefreshToken") ?? string.Empty,
				COQLQuery = Environment.GetEnvironmentVariable("COQLQuery") ?? string.Empty,
				ZohoCOQLPath = Environment.GetEnvironmentVariable("ZohoCOQLPath") ?? string.Empty,
				ZohoInventoryBaseUrl = Environment.GetEnvironmentVariable("ZohoInventoryBaseUrl") ?? string.Empty,
				ZohoInventoryAddItemPath = Environment.GetEnvironmentVariable("ZohoInventoryAddItemPath") ?? string.Empty,
				ZohoInventoryRefreshToken = Environment.GetEnvironmentVariable("ZohoInventoryRefreshToken") ?? string.Empty,
				ZohoInventoryItemPath = Environment.GetEnvironmentVariable("ZohoInventoryItemPath") ?? string.Empty
			};
		}
	}
}