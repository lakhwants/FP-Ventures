using FPVenturesPestRoutes.Constants;
using FPVenturesPestRoutes.Models;
using System;
using System.Collections.Generic;

namespace FPVenturesPestRoutes.Services.Mapper
{
	public class ModelMapper
	{
		public static ZohoPestRouteModel MapCustomersToPestRoutModels(List<Customer> customers)
		{
			ZohoPestRouteModel zohoLeadsModel = new();

			List<Data> dataList = new();
			foreach (var customer in customers)
			{
				DateTime defaultDateTime = new(1900, 1, 1, 0, 0, 0);
				DateTime dateValue;
				Data data = new()
				{
					CustomerID = customer.CustomerID,
					CustomerName = string.IsNullOrEmpty(customer.Fname) && string.IsNullOrEmpty(customer.Lname) ? "Unknown" : customer.Fname + " " + customer.Lname,
					Email = customer.Email,
					Phone = RemoveCountryCode(customer.Phone1),
					FirstName = customer.Fname,
					CompanyName = customer.CompanyName,
					CustomerEmail = customer.Email,
					BillingCountryID = customer.BillingCountryID,
					BillingCity = customer.BillingCity,
					BillingZIP = customer.BillingZip,
					BillingPhone = RemoveCountryCode(customer.BillingPhone),
					Latitude = Convert.ToDecimal(customer.Lat),
					DateAdded = GetDateString(Convert.ToDateTime(DateTime.TryParse(customer.DateAdded, out dateValue) ? dateValue : defaultDateTime)),
					DateUpdated = GetDateString(Convert.ToDateTime(DateTime.TryParse(customer.DateUpdated, out dateValue) ? dateValue : defaultDateTime)),
					Source = customer.Source,
					CustomerLink = customer.CustomerLink,
					CustomerSource = customer.CustomerSource,
					PaymentIDs = customer.PaymentIDs,
					OfficeID = customer.OfficeID,
					LastName = customer.Lname,
					CommercialAccount = customer.CommercialAccount,
					BillingAddress = customer.BillingAddress,
					BillingState = customer.BillingState,
					BillingEmail = customer.BillingEmail,
					Longitude = Convert.ToDecimal(customer.Lng),
					SquareFeet = Convert.ToDecimal(customer.SquareFeet),
					DateCancelled = GetDateString(DateTime.TryParse(customer.DateCancelled, out dateValue) ? dateValue : defaultDateTime),
					SourceID = customer.SourceID,
					RegionID = customer.RegionID,
					SubscriptionIDs = customer.SubscriptionIDs
				};

				dataList.Add(data);
			}
			zohoLeadsModel.Data = dataList;
			return zohoLeadsModel;
		}

		public static string RemoveCountryCode(string phone)
		{
			if (string.IsNullOrEmpty(phone))
				return null;

			foreach (var country in CountryCodes.PhoneCountryCodes)
			{
				phone = phone.Replace(country, "");
			}
			return phone;
		}

		public static ZohoPestRouteCustomerErrorModel MapCustomersToZohoErrorModel(Data errorlist)
		{

			ZohoPestRouteCustomerErrorModel zohoErrorModel = new()
			{
				CustomerID = errorlist.CustomerID,
				CustomerName = errorlist.CustomerName,
				Email = errorlist.Email,
				Phone = errorlist.Phone,
				FirstName = errorlist.FirstName,
				CompanyName = errorlist.CompanyName,
				CustomerEmail = errorlist.CustomerEmail,
				BillingCountryID = errorlist.BillingCountryID,
				BillingCity = errorlist.BillingCity,
				BillingZIP = errorlist.BillingZIP,
				BillingPhone = errorlist.BillingPhone,
				Latitude = errorlist.Latitude,
				DateAdded = errorlist.DateAdded,
				DateUpdated = errorlist.DateUpdated,
				Source = errorlist.Source,
				CustomerLink = errorlist.CustomerLink,
				CustomerSource = errorlist.CustomerSource,
				PaymentIDs = errorlist.PaymentIDs,
				OfficeID = errorlist.OfficeID,
				LastName = errorlist.LastName,
				CommercialAccount = errorlist.CommercialAccount,
				BillingAddress = errorlist.BillingAddress,
				BillingState = errorlist.BillingState,
				BillingEmail = errorlist.BillingEmail,
				Longitude = errorlist.Longitude,
				SquareFeet = errorlist.SquareFeet,
				DateCancelled = errorlist.DateCancelled,
				SourceID = errorlist.SourceID,
				RegionID = errorlist.RegionID,
				SubscriptionIDs = errorlist.SubscriptionIDs
			};

			return zohoErrorModel;
		}

		static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}

	}
}
