using FPVentures.Models;
using FPVentures.Services;
using FPVentures.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVentures
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
					services.AddSingleton<IZohoLeadsService, ZohoLeadsService>();

				})
				.Build();

			host.Run();
		}

		private static RingbaZohoConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				RingbaBaseUrl= Environment.GetEnvironmentVariable("RingbaBaseUrl") ?? string.Empty,
				RingbaAccessToken = Environment.GetEnvironmentVariable("RingbaAccessToken") ?? string.Empty,
				RingbaAccountsPath = Environment.GetEnvironmentVariable("RingbaAccountsPath") ?? string.Empty,
				RingbaCallLogDetailsPath = Environment.GetEnvironmentVariable("RingbaCallLogDetailsPath") ?? string.Empty,
				RingbaCallLogsPath = Environment.GetEnvironmentVariable("RingbaCallLogsPath") ?? string.Empty,
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoAddLeadsPath = Environment.GetEnvironmentVariable("ZohoAddLeadsPath") ?? string.Empty,
				ZohoLeadsBaseUrl= Environment.GetEnvironmentVariable("ZohoLeadsBaseUrl") ?? string.Empty,
				ZohoCheckDuplicateLeadsPath = Environment.GetEnvironmentVariable("ZohoCheckDuplicateLeadsPath") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				ZohoRefreshToken = Environment.GetEnvironmentVariable("ZohoRefreshToken") ?? string.Empty
			};
		}
	}
}
