using FPVenturesFive9Dispostion.Models;
using FPVenturesPestRoutes.Constants;
using FPVenturesPestRoutes.Models;
using FPVenturesPestRoutes.Services.Interfaces;
using FPVenturesPestRoutes.Services.Mapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FPVenturesPestRoutes
{
	public class FPVenturesPestRoute
	{
		public readonly IZohoService _zohoService;
		public readonly IPestRouteService _pestRouteService;
		public readonly PestRouteZohoConfigurationSettings _pestRouteZohoConfigurationSettings;
		const string AzureFunctionName = "FPVenturesPestRoutes";

		public FPVenturesPestRoute(IZohoService zohoService, IPestRouteService five9Service, PestRouteZohoConfigurationSettings pestRouteZohoConfigurationSettings)
		{
			_zohoService = zohoService;
			_pestRouteService = five9Service;
			_pestRouteZohoConfigurationSettings = pestRouteZohoConfigurationSettings;
		}
		[Function(AzureFunctionName)]
		public void RunAsync([TimerTrigger("%Schedule%")] TimerInfo timerInfo, FunctionContext context)
		{
			foreach (var keyAndToken in PestRouteKeysAndTokens.KeysAndTokens)
			{
				_pestRouteZohoConfigurationSettings.PestRouteAuthenticationKey = keyAndToken.Key;
				_pestRouteZohoConfigurationSettings.PestRouteAuthenticationToken = keyAndToken.Token;

				var logger = context.GetLogger(AzureFunctionName);

				logger.LogInformation($"{AzureFunctionName} Timer - {timerInfo.ScheduleStatus.Next}");
				logger.LogInformation($"{AzureFunctionName} Function started on {DateTime.Now}");

				logger.LogInformation($"Hawx office name = {keyAndToken.OfficeName}");

				var ids = _pestRouteService.GetPestRouteCustomerIDs();
				logger.LogInformation($"Customers from Pest Route on {DateTime.Now.Date.AddDays(-1)} = {ids.CustomerIDs.Count}");

				var duplicateCustomers = _zohoService.DuplicateZohoPestRoutes(ids.CustomerIDs);
				logger.LogInformation($"Duplicate Customers = {duplicateCustomers.Count}");

				var customerDetailModels = _pestRouteService.GetPestRouteCustomerDetails(ids.CustomerIDs.Where(x => !duplicateCustomers.Any(y => y == x)).ToList());
				logger.LogInformation($"Customers to add = {customerDetailModels.Count}");

				var zohoPestRouteModel = ModelMapper.MapCustomersToPestRoutModels(customerDetailModels);
				var (successModels, errorModels) = _zohoService.AddPestRouteCustomerToZoho(zohoPestRouteModel);
				logger.LogInformation($"Customers added successfully = {successModels.Count}");
				logger.LogInformation($"Customers failed to add = {errorModels.Count}");
				LogErrorModels(logger, errorModels);

			}

		}

		private static void LogErrorModels(ILogger logger, List<ZohoPestRouteCustomerErrorModel> errorModels)
		{
			foreach (var errorModel in errorModels)
			{
				logger.LogWarning($"{nameof(errorModel.ApiName)} : {errorModel.ApiName}," +
						$"{nameof(errorModel.Message)} = {errorModel.Message}," +
						$"{nameof(errorModel.Status)} = {errorModel.Status}," +
						$"{nameof(errorModel.CustomerID)} = {errorModel.CustomerID} ," +
						$"{nameof(errorModel.CustomerName)} = {errorModel.CustomerName}," +
						$"{nameof(errorModel.Email)} = {errorModel.Email}," +
						$"{nameof(errorModel.Phone)} = {errorModel.Phone}," +
						$"{nameof(errorModel.FirstName)} = {errorModel.FirstName}," +
						$"{nameof(errorModel.CompanyName)} = {errorModel.CompanyName}," +
						$"{nameof(errorModel.CustomerEmail)} = {errorModel.CustomerEmail}," +
						$"{nameof(errorModel.BillingCountryID)} = {errorModel.BillingCountryID}," +
						$"{nameof(errorModel.BillingCity)} = {errorModel.BillingCity}," +
						$"{nameof(errorModel.BillingZIP)} = {errorModel.BillingZIP}," +
						$"{nameof(errorModel.BillingPhone)} = {errorModel.BillingPhone}," +
						$"{nameof(errorModel.Latitude)} = {errorModel.Latitude}," +
						$"{nameof(errorModel.DateAdded)} = {errorModel.DateAdded}," +
						$"{nameof(errorModel.DateUpdated)} = {errorModel.DateUpdated}," +
						$"{nameof(errorModel.Source)} = {errorModel.Source}," +
						$"{nameof(errorModel.CustomerLink)} = {errorModel.CustomerLink}," +
						$"{nameof(errorModel.CustomerSource)} = {errorModel.CustomerSource}," +
						$"{nameof(errorModel.PaymentIDs)} = {errorModel.PaymentIDs}," +
						$"{nameof(errorModel.OfficeID)} = {errorModel.OfficeID}," +
						$"{nameof(errorModel.LastName)} = {errorModel.LastName}," +
						$"{nameof(errorModel.CommercialAccount)} = {errorModel.CommercialAccount}," +
						$"{nameof(errorModel.BillingAddress)} = {errorModel.BillingAddress}," +
						$"{nameof(errorModel.BillingState)} = {errorModel.BillingState}," +
						$"{nameof(errorModel.BillingEmail)} = {errorModel.BillingEmail}," +
						$"{nameof(errorModel.Longitude)} = {errorModel.Longitude}," +
						$"{nameof(errorModel.SquareFeet)} = {errorModel.SquareFeet}," +
						$"{nameof(errorModel.DateCancelled)} = {errorModel.DateCancelled}," +
						$"{nameof(errorModel.SourceID)} = {errorModel.SourceID}," +
						$"{nameof(errorModel.RegionID)} = {errorModel.RegionID}," +
						$"{nameof(errorModel.SubscriptionIDs)} = {errorModel.SubscriptionIDs}");
			}
		}
	}
}
