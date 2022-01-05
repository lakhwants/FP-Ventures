using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PestRoutesTest.Models
{
	public class ZohoPestRouteModel
	{
		[JsonProperty("data")]
		public List<Data> Data { get; set; }
	}

	public class Data
	{
		public string Customer_Name { get; set; }
		public string Date_Added { get; set; }
		public string Date_Updated { get; set; }
		public string Email { get; set; }

        public decimal Latitude { get; set; }
        public string Name { get; set; }

        public string Appointment_IDs { get; set; }

        public string Customer_Source { get; set; }
        public string Source_ID { get; set; }

        public string Customer_Email { get; set; }
        public decimal Longitude { get; set; }
        public string Billing_Phone { get; set; }
        public string Billing_City { get; set; }
        public string Billing_Email { get; set; }
        public string Customer_ID { get; set; }
        public string Company_Name { get; set; }
        public string Commercial_Account { get; set; }
        public string Subscription_IDs { get; set; }
        public string Office_ID { get; set; }
        public string Payment_IDs { get; set; }
        public string Billing_Country_ID { get; set; }
        public string Region_ID { get; set; }

        public string Source { get; set; }
        public string First_Name { get; set; }
        public string Phone { get; set; }
        public string Date_Cancelled { get; set; }

        public string Billing_ZIP { get; set; }
        public string Last_Name { get; set; }
        public decimal Square_Feet { get; set; }

        public string Billing_Address { get; set; }
        public string Billing_State { get; set; }
        public string Customer_Link { get; set; }

    }
}
