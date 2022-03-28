using FPVenturesZohoInventoryPurchaseOrder.Models;
using FPVenturesZohoInventoryPurchaseOrder.Services;
using FPVenturesZohoInventoryPurchaseOrder.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesZohoInventoryPurchaseOrder
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
				ZohoInventoryItemPath = Environment.GetEnvironmentVariable("ZohoInventoryItemPath") ?? string.Empty,
				ZohoInventoryRefreshToken = Environment.GetEnvironmentVariable("ZohoInventoryRefreshToken") ?? string.Empty,
				ZohoInventorySalesOrderPath = Environment.GetEnvironmentVariable("ZohoInventoryAddSalesOrderPath") ?? string.Empty,
				ZohoInventoryInvoicePath = Environment.GetEnvironmentVariable("ZohoInventoryInvoicePath") ?? string.Empty,
				ZohoInventoryTaxesPath = Environment.GetEnvironmentVariable("ZohoInventoryTaxesPath") ?? string.Empty,
				ZohoInventorySalesOrderConfirmPath = Environment.GetEnvironmentVariable("ZohoInventorySalesOrderConfirmPath") ?? string.Empty,
				ZohoInventoryOrganizationId = Environment.GetEnvironmentVariable("ZohoInventoryOrganizationId") ?? string.Empty,
				ZohoInventorySearchParameter = Environment.GetEnvironmentVariable("ZohoInventorySearchParameter") ?? string.Empty,
				ZohoInventoryContactsPath = Environment.GetEnvironmentVariable("ZohoInventoryContactsPath") ?? string.Empty,
				ZohoInventoryContactPersonPath = Environment.GetEnvironmentVariable("ZohoInventoryContactPersonPath") ?? string.Empty,
				ZohoInventoryCustomerName = Environment.GetEnvironmentVariable("ZohoInventoryCustomerName") ?? string.Empty,
				ZohoPurchaseOrderPath = Environment.GetEnvironmentVariable("ZohoPurchaseOrderPath") ?? string.Empty
			};
		}
	}
}