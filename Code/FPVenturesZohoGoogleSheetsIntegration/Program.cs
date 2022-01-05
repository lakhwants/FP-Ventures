using FPVenturesZohoGoogleSheetsIntegration.Models;
using FPVenturesZohoGoogleSheetsIntegration.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using ZohoGoogleSheetsIntegration.Services;

namespace FPVenturesZohoGoogleSheetsIntegration
{
	public class Program
	{
		public static void Main()
		{
			ZohoGoogleSheetsConfigurationSettings zohoGoogleSheetsConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(zohoGoogleSheetsConfigurationSettings);
					services.AddSingleton<IZohoAnalyticsService, ZohoAnalyticsService>();

				})
				.Build();

			host.Run();
		}

		private static ZohoGoogleSheetsConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				ZohoRefreshToken = Environment.GetEnvironmentVariable("ZohoRefreshToken") ?? string.Empty,
				ZohoAnalyticsGetReports = Environment.GetEnvironmentVariable("ZohoAnalyticsGetReports") ?? string.Empty
			};
		}
	}
}
