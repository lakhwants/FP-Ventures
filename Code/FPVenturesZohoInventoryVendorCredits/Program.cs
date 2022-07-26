using FPVenturesZohoInventoryVendorCredits.Models;
using FPVenturesZohoInventoryVendorCredits.Services;
using FPVenturesZohoInventoryVendorCredits.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesZohoInventoryVendorCredits
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
					services.AddSingleton<IZohoLeadsService, ZohoLeadsService>();
				})
				.Build();

			host.Run();
		}

		private static ConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoCRMRefreshToken = Environment.GetEnvironmentVariable("ZohoCRMRefreshToken") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				DispositionCOQLQuery = Environment.GetEnvironmentVariable("DispositionCOQLQuery") ?? string.Empty,
				ZohoInventoryBaseUrl = Environment.GetEnvironmentVariable("ZohoInventoryBaseUrl") ?? string.Empty,
				ZohoInventoryRefreshToken = Environment.GetEnvironmentVariable("ZohoInventoryRefreshToken") ?? string.Empty,
				ZohoInventoryOrganizationId = Environment.GetEnvironmentVariable("ZohoInventoryOrganizationId") ?? string.Empty,
				ZohoCRMVendorsPath = Environment.GetEnvironmentVariable("ZohoCRMVendorsPath") ?? string.Empty,
				ZohoCRMBaseUrl = Environment.GetEnvironmentVariable("ZohoCRMBaseUrl") ?? string.Empty,
				ZohoInventoryVendorsPath = Environment.GetEnvironmentVariable("ZohoInventoryVendorsPath") ?? string.Empty,
				ZohoCOQLPath = Environment.GetEnvironmentVariable("ZohoCOQLPath") ?? string.Empty,
				RefundAccountId = Environment.GetEnvironmentVariable("RefundAccountId") ?? string.Empty,
				ZohoInventoryVendorCreditsPath = Environment.GetEnvironmentVariable("ZohoInventoryVendorCreditsPath") ?? string.Empty,
				ZohoInventoryItemPath = Environment.GetEnvironmentVariable("ZohoInventoryItemPath") ?? string.Empty
			};
		}
	}
}