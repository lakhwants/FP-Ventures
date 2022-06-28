using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesInventoryWebLeadsIntegration.Models
{

	public class Record
    {
        public string Company { get; set; }
        public string Email { get; set; }

        [JsonProperty("$state")]
        public string State { get; set; }

        [JsonProperty("First_Name")]
        public string FirstName { get; set; }
        public string Phone { get; set; }

        [JsonProperty("Last_Name")]
        public string LastName { get; set; }

        [JsonProperty("Lead_Source")]
        public string LeadSource { get; set; } = "Nationwide pest";
	}

    public class HawxZohoLeadsModel
    {
        [JsonProperty("data")]
        public List<Record> Data { get; set; }
    }

}
