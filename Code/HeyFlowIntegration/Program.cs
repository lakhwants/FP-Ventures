using HeyFlowIntegration.Models;
using HeyFlowIntegration.Services;
using HeyFlowIntegration.Services.Interfaces;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace HeyFlowIntegration
{
	public class Program
	{
		public static void Main()
		{
			HeyFlowZohoConfigurationSettings zohoHawxConfigurationSettings = GetConfigurationSettings();
			var host = new HostBuilder()
				.ConfigureFunctionsWorkerDefaults()
				.ConfigureLogging((context, loggingBuilder) =>
				{
				})
				.ConfigureServices(services =>
				{
					services.AddSingleton(zohoHawxConfigurationSettings);
					services.AddSingleton<IZohoLeadsService, ZohoLeadsService>();

				})
				.Build();

			host.Run();
		}

		private static HeyFlowZohoConfigurationSettings GetConfigurationSettings()
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

				HawxZohoClientId= Environment.GetEnvironmentVariable("HawxZohoClientId") ?? string.Empty,
				HawxZohoClientSecret = Environment.GetEnvironmentVariable("HawxZohoClientSecret") ?? string.Empty,
				HawxZohoRefreshToken = Environment.GetEnvironmentVariable("HawxZohoRefreshToken") ?? string.Empty,
			};
		}
	}
}