using FPVenturesFive9InventoryDisposition.Models;
using FPVenturesFive9InventoryDisposition.Services;
using FPVenturesFive9InventoryDisposition.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesFive9InventoryDisposition
{
    public class Program
	{
		public static void Main()
		{
			Five9ZohoInventoryConfigurationSettings five9ZohoConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(five9ZohoConfigurationSettings);
					services.AddSingleton<IZohoInventoryService, ZohoInventoryService>();
					services.AddSingleton<IFive9Service, Five9Service>();
					services.AddSingleton<IZohoService, ZohoService>();

				})
				.Build();

			host.Run();
		}

		public static Five9ZohoInventoryConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoAddDispositionsPath = Environment.GetEnvironmentVariable("ZohoAddDispositionsPath") ?? string.Empty,
				ZohoBaseUrl = Environment.GetEnvironmentVariable("ZohoBaseUrl") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				Five9BaseUrl=Environment.GetEnvironmentVariable("Five9BaseUrl") ?? string.Empty,
				Five9ServiceNamespace=Environment.GetEnvironmentVariable("Five9ServiceNamespace") ?? string.Empty,
				Five9MethodAction=Environment.GetEnvironmentVariable("Five9MethodAction") ?? string.Empty
			};
		}
	}
}
