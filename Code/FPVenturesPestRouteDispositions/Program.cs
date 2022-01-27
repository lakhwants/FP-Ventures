using FPVenturesFive9PestRouteDispositions.Models;
using FPVenturesFive9PestRouteDispositions.Services;
using FPVenturesFive9PestRouteDispositions.Services.Interfaces;
using FPVenturesPestRouteDispositions.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesFive9PestRouteDispositions
{
	public class Program
	{
		public static void Main()
		{
			Five9ZohoConfigurationSettings five9ZohoConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(five9ZohoConfigurationSettings);
					services.AddSingleton<IZohoService, ZohoService>();
					services.AddSingleton<IFive9Service, Five9Service>();

				})
				.Build();

			host.Run();
		}

		public static Five9ZohoConfigurationSettings GetConfigurationSettings()
		{
			return new()
			{
				ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
				ZohoAddDispositionsPath = Environment.GetEnvironmentVariable("ZohoAddDispositionsPath") ?? string.Empty,
				ZohoBaseUrl = Environment.GetEnvironmentVariable("ZohoBaseUrl") ?? string.Empty,
				ZohoCheckDuplicateDispositionsPath = Environment.GetEnvironmentVariable("ZohoCheckDuplicateDispositionsPath") ?? string.Empty,
				ZohoCheckDuplicateLeadsPath=Environment.GetEnvironmentVariable("ZohoCheckDuplicateLeadsPath") ?? string.Empty,
				ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
				ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
				ZohoRefreshToken = Environment.GetEnvironmentVariable("ZohoRefreshToken") ?? string.Empty,
				Five9BaseUrl=Environment.GetEnvironmentVariable("Five9BaseUrl") ?? string.Empty,
				Five9ServiceNamespace=Environment.GetEnvironmentVariable("Five9ServiceNamespace") ?? string.Empty,
				Five9MethodAction=Environment.GetEnvironmentVariable("Five9MethodAction") ?? string.Empty
			};
		}
	}
}
