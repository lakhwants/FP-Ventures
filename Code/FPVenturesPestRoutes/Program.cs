using FPVenturesFive9Dispostion.Models;
using FPVenturesPestRoutes.Services;
using FPVenturesPestRoutes.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesPestRoutes
{
	public class Program
	{
		public static void Main()
		{
			PestRouteZohoConfigurationSettings pestRouteZohoConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(pestRouteZohoConfigurationSettings);
					services.AddSingleton<IZohoService, ZohoService>();
					services.AddSingleton<IPestRouteService, PestRouteService>();

				})
				.Build();

			host.Run();
		}

		public static PestRouteZohoConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoAddPestRouteCustomerPath = Environment.GetEnvironmentVariable("ZohoAddPestRouteCustomerPath") ?? string.Empty,
				ZohoBaseUrl = Environment.GetEnvironmentVariable("ZohoBaseUrl") ?? string.Empty,
				ZohoCheckDuplicatePestRouteCustomerPath = Environment.GetEnvironmentVariable("ZohoCheckDuplicatePestRouteCustomerPath") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				ZohoRefreshToken = Environment.GetEnvironmentVariable("ZohoRefreshToken") ?? string.Empty,
				PestRouteBaseUrl = Environment.GetEnvironmentVariable("PestRouteBaseUrl") ?? string.Empty,
				PestRouteGetCutomerDetailsPath = Environment.GetEnvironmentVariable("PestRouteGetCutomerDetailsPath") ?? string.Empty,
				PestRouteSearchCustomerPath = Environment.GetEnvironmentVariable("PestRouteSearchCustomerPath") ?? string.Empty
			};
		}
	}
}
