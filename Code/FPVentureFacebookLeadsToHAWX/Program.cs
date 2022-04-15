using FPVentureFacebookLeadsToHAWX.Models;
using FPVentureFacebookLeadsToHAWX.Services;
using FPVentureFacebookLeadsToHAWX.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVentureFacebookLeadsToHAWX
{
	public class Program
	{
		public static void Main()
		{
			ZohoHAWXConfigurationSettings zohoHawxConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(zohoHawxConfigurationSettings);
					services.AddSingleton<IHAWXZohoLeadsService, HAWXZohoLeadsService>();
					services.AddSingleton<IZohoLeadsService, ZohoLeadsService>();

				})
				.Build();

			host.Run();
		}

		private static ZohoHAWXConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoLeadsBaseUrl = Environment.GetEnvironmentVariable("ZohoLeadsBaseUrl") ?? string.Empty,
				ZohoCheckDuplicateLeadsPath = Environment.GetEnvironmentVariable("ZohoCheckDuplicateLeadsPath") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				ZohoRefreshToken = Environment.GetEnvironmentVariable("ZohoRefreshToken") ?? string.Empty,
				ZohoAddLeadsPath = Environment.GetEnvironmentVariable("ZohoAddLeadsPath") ?? string.Empty,
				ZohoHAWXClientId = Environment.GetEnvironmentVariable("ZohoHAWXClientId") ?? string.Empty,
				ZohoHAWXClientSecret = Environment.GetEnvironmentVariable("ZohoHAWXClientSecret") ?? string.Empty,
				ZohoHAWXRefreshToken = Environment.GetEnvironmentVariable("ZohoHAWXRefreshToken") ?? string.Empty,
				COQLQuery = Environment.GetEnvironmentVariable("COQLQuery") ?? string.Empty,
				ZohoCOQLPath = Environment.GetEnvironmentVariable("ZohoCOQLPath") ?? string.Empty
			};
		}
	}
}