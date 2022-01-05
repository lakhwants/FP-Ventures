using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesPestRoutes.Models
{
	public class ZohoPestRouteModel
	{
		[JsonProperty("data")]
		public List<Data> Data { get; set; }
	}

	public class Data
	{
        [JsonProperty("Name")]
		public string CustomerName { get; set; }

        [JsonProperty("Date_Added")]
        public string DateAdded { get; set; }

        [JsonProperty("Date_Updated")]
        public string DateUpdated { get; set; }
		public string Email { get; set; }

        public decimal Latitude { get; set; }

        [JsonProperty("Appointment_IDs")]
        public string AppointmentIDs { get; set; }

        [JsonProperty("Customer_Source")]
        public string CustomerSource { get; set; }

        [JsonProperty("Source_ID")]
        public string SourceID { get; set; }

        [JsonProperty("Customer_Email")]
        public string CustomerEmail { get; set; }
        public decimal Longitude { get; set; }

        [JsonProperty("Billing_Phone")]
        public string BillingPhone { get; set; }

        [JsonProperty("Billing_City")]
        public string BillingCity { get; set; }

        [JsonProperty("Billing_Email")]
        public string BillingEmail { get; set; }

        [JsonProperty("Customer_ID")]
        public string CustomerID { get; set; }

        [JsonProperty("Company_Name")]
        public string CompanyName { get; set; }

        [JsonProperty("Commercial_Account")]
        public string CommercialAccount { get; set; }

        [JsonProperty("Subscription_IDs")]
        public string SubscriptionIDs { get; set; }

        [JsonProperty("Office_ID")]
        public string OfficeID { get; set; }

        [JsonProperty("Payment_IDs")]
        public string PaymentIDs { get; set; }

        [JsonProperty("Billing_Country_ID")]
        public string BillingCountryID { get; set; }

        [JsonProperty("Region_ID")]
        public string RegionID { get; set; }

        public string Source { get; set; }

        [JsonProperty("First_Name")]
        public string FirstName { get; set; }
        public string Phone { get; set; }

        [JsonProperty("Date_Cancelled")]
        public string DateCancelled { get; set; }

        [JsonProperty("Billing_ZIP")]
        public string BillingZIP { get; set; }

        [JsonProperty("Last_Name")]
        public string LastName { get; set; }

        [JsonProperty("Square_Feet")]
        public decimal SquareFeet { get; set; }

        [JsonProperty("Billing_Address")]
        public string BillingAddress { get; set; }

        [JsonProperty("Billing_State")]
        public string BillingState { get; set; }

        [JsonProperty("Customer_Link")]
        public string CustomerLink { get; set; }

    }
}
