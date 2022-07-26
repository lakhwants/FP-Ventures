using FPVenturesZohoInventorySalesOrder.Models;
using FPVenturesZohoInventorySalesOrder.Services;
using FPVenturesZohoInventorySalesOrder.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesZohoInventorySalesOrder
{
	public class Program
	{
		public static void Main()
		{
			ConfigurationSettings zohoCRMAndInventoryConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(zohoCRMAndInventoryConfigurationSettings);
					services.AddSingleton<IZohoInventoryService, ZohoInventoryService>();
				})
				.Build();

			host.Run();
		}

		private static ConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoInventoryBaseUrl = Environment.GetEnvironmentVariable("ZohoInventoryBaseUrl") ?? string.Empty,
				ZohoInventoryItemPath = Environment.GetEnvironmentVariable("ZohoInventoryItemPath") ?? string.Empty,
				ZohoInventoryRefreshToken = Environment.GetEnvironmentVariable("ZohoInventoryRefreshToken") ?? string.Empty,
				ZohoInventoryAddSalesOrderPath = Environment.GetEnvironmentVariable("ZohoInventoryAddSalesOrderPath") ?? string.Empty,
				ZohoInventoryInvoicePath = Environment.GetEnvironmentVariable("ZohoInventoryInvoicePath") ?? string.Empty,
				ZohoInventoryTaxesPath = Environment.GetEnvironmentVariable("ZohoInventoryTaxesPath") ?? string.Empty,
				ZohoInventorySalesOrderConfirmPath = Environment.GetEnvironmentVariable("ZohoInventorySalesOrderConfirmPath") ?? string.Empty,
				ZohoInventoryOrganizationId = Environment.GetEnvironmentVariable("ZohoInventoryOrganizationId") ?? string.Empty,
				ZohoInventoryContactsPath = Environment.GetEnvironmentVariable("ZohoInventoryContactsPath") ?? string.Empty,
				ZohoInventoryContactPersonPath = Environment.GetEnvironmentVariable("ZohoInventoryContactPersonPath") ?? string.Empty,
				ZohoInventoryInvoiceFromSalesOrderPath = Environment.GetEnvironmentVariable("ZohoInventoryInvoiceFromSalesOrderPath") ?? string.Empty,
				ZohoInventoryCustomerName = Environment.GetEnvironmentVariable("ZohoInventoryCustomerName") ?? string.Empty
			};
		}
	}
}