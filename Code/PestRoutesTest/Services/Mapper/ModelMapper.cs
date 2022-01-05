using PestRoutesTest.Constants;
using PestRoutesTest.Models;
using System;
using System.Collections.Generic;

namespace PestRoutesTest.Services.Mapper
{
	public class ModelMapper
	{
		public static ZohoPestRouteModel MapCustomersToPestRoutModels(List<Customer> customers)
		{
			ZohoPestRouteModel zohoLeadsModel = new ZohoPestRouteModel();

			List<Data> dataList = new List<Data>();
			foreach (var customer in customers)
			{
				DateTime defaultDateTime = new DateTime(1900, 1, 1, 0, 0, 0);
				DateTime dateValue;
				Data data = new Data
				{
					Customer_ID = customer.customerID,
					Customer_Name = string.IsNullOrEmpty(customer.fname) && string.IsNullOrEmpty(customer.lname) ? "Unknown" : customer.fname + " " + customer.lname,
					Email = customer.email,
					Phone = RemoveCountryCode(customer.phone1),
					First_Name = customer.fname,
					Company_Name = customer.companyName,
					Customer_Email = customer.email,
					Billing_Country_ID = customer.billingCountryID,
					Billing_City = customer.billingCity,
					Billing_ZIP = customer.billingZip,
					Billing_Phone = RemoveCountryCode(customer.billingPhone),
					Latitude = Convert.ToDecimal(customer.lat),
					Date_Added = GetDateString(Convert.ToDateTime(DateTime.TryParse(customer.dateAdded, out dateValue) ? dateValue : defaultDateTime)),
					Date_Updated = GetDateString(Convert.ToDateTime(DateTime.TryParse(customer.dateUpdated, out dateValue) ? dateValue : defaultDateTime)),
					Source = customer.source,
					Customer_Link = customer.customerLink,
					Customer_Source = customer.customerSource,
					Payment_IDs = customer.paymentIDs,
					Office_ID = customer.officeID,
					Last_Name = customer.lname,
					Commercial_Account = customer.commercialAccount,
					Billing_Address = customer.billingAddress,
					Billing_State = customer.billingState,
					Billing_Email = customer.billingEmail,
					Longitude = Convert.ToDecimal(customer.lng),
					Square_Feet = Convert.ToDecimal(customer.squareFeet),
					Date_Cancelled = GetDateString(DateTime.TryParse(customer.dateCancelled, out dateValue) ? dateValue : defaultDateTime),
					Source_ID = customer.sourceID,
					Region_ID = customer.regionID,
					Subscription_IDs = customer.subscriptionIDs
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

		public static ZohoErrorModel MapCustomersToZohoErrorModel(Data errorlist)
		{

			ZohoErrorModel zohoErrorModel = new ZohoErrorModel()
			{
				Customer_ID = errorlist.Customer_ID,
				Customer_Name = errorlist.Customer_Name,
				Email = errorlist.Email,
				Phone = errorlist.Phone,
				First_Name = errorlist.First_Name,
				Company_Name = errorlist.Company_Name,
				Customer_Email = errorlist.Customer_Email,
				Billing_Country_ID = errorlist.Billing_Country_ID,
				Billing_City = errorlist.Billing_City,
				Billing_ZIP = errorlist.Billing_ZIP,
				Billing_Phone = errorlist.Billing_Phone,
				Latitude = errorlist.Latitude,
				Date_Added = errorlist.Date_Added,
				Date_Updated = errorlist.Date_Updated,
				Source = errorlist.Source,
				Customer_Link = errorlist.Customer_Link,
				Customer_Source = errorlist.Customer_Source,
				Payment_IDs = errorlist.Payment_IDs,
				Office_ID = errorlist.Office_ID,
				Last_Name = errorlist.Last_Name,
				Commercial_Account = errorlist.Commercial_Account,
				Billing_Address = errorlist.Billing_Address,
				Billing_State = errorlist.Billing_State,
				Billing_Email = errorlist.Billing_Email,
				Longitude = errorlist.Longitude,
				Square_Feet = errorlist.Square_Feet,
				Date_Cancelled = errorlist.Date_Cancelled,
				Source_ID = errorlist.Source_ID,
				Region_ID = errorlist.Region_ID,
				Subscription_IDs = errorlist.Subscription_IDs
			};

			return zohoErrorModel;
		}

		static string GetDateString(DateTime date)
		{
			return date.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK");
		}

	}
}
