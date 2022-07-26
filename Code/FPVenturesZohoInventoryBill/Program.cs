using FPVenturesZohoInventoryBill.Models;
using FPVenturesZohoInventoryBill.Services;
using FPVenturesZohoInventoryBill.Services.Interfaces;
using FPVenturesZohoInventoryBills.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FPVenturesZohoInventoryBills
{
    public class Program
    {
        public static void Main()
        {
            ConfigurationSettings configurationSettings = GetConfigurationSettings();
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureLogging((context, loggingBuilder) =>
                {
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton(configurationSettings);
                    services.AddSingleton<IZohoInventoryService, ZohoInventoryService>();
                    services.AddSingleton<IZohoLeadsService, ZohoLeadsService>();
                })
                 .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                .Build();

            host.Run();

        }


        private static ConfigurationSettings GetConfigurationSettings()
        {
            return new()
            {
                ZohoAccessTokenFromRefreshTokenPath = Environment.GetEnvironmentVariable("ZohoAccessTokenFromRefreshTokenPath") ?? string.Empty,
                ZohoClientId = Environment.GetEnvironmentVariable("ZohoClientId") ?? string.Empty,
                ZohoClientSecret = Environment.GetEnvironmentVariable("ZohoClientSecret") ?? string.Empty,
                ZohoCRMRefreshToken = Environment.GetEnvironmentVariable("ZohoCRMRefreshToken") ?? string.Empty,
                ZohoInventoryBaseUrl = Environment.GetEnvironmentVariable("ZohoInventoryBaseUrl") ?? string.Empty,
                ZohoInventoryRefreshToken = Environment.GetEnvironmentVariable("ZohoInventoryRefreshToken") ?? string.Empty,
                ZohoCRMBaseUrl = Environment.GetEnvironmentVariable("ZohoCRMBaseUrl") ?? string.Empty,
                ZohoCRMVendorsPath = Environment.GetEnvironmentVariable("ZohoCRMVendorsPath") ?? string.Empty,
                ZohoInventoryVendorsPath = Environment.GetEnvironmentVariable("ZohoInventoryVendorsPath") ?? string.Empty,
                ZohoInventoryOrganizationId = Environment.GetEnvironmentVariable("ZohoInventoryOrganizationId") ?? string.Empty,
                ZohoInventoryBillsAccountId = Environment.GetEnvironmentVariable("ZohoInventoryBillsAccountId") ?? string.Empty,
                ZohoInventoryItemPath = Environment.GetEnvironmentVariable("ZohoInventoryItemPath") ?? string.Empty,
                ZohoInventoryBillsPath = Environment.GetEnvironmentVariable("ZohoInventoryBillsPath") ?? string.Empty
            };
        }
    }
}