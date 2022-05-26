using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVentureFacebookLeadsToHAWX.Models
{
	public class ZohoLeadsModel	
	{
		[JsonProperty("data")]
		public List<Data> Data { get; set; }
	}

	public class Data
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("Last_Name")]
		public string LastName { get; set; }

		[JsonProperty("First_Name")]
		public string FirstName { get; set; }

		[JsonProperty("Email")]
		public string Email { get; set; }

		[JsonProperty("State")]
		public string State { get; set; }

		public string Company { get; set; }

		[JsonProperty("Mobile")]
		public string Mobile { get; set; }

		[JsonProperty("Phone")]
		public string Phone { get; set; }

		[JsonProperty("IsFacebookLead")]
		public bool IsFacebookLead { get; set; }

		[JsonProperty("Payout")]
		public string Payout { get; set; }

		[JsonProperty("Unbounce_Page_Name")]
		public string UnbouncePageName { get; set; }
	}

}
