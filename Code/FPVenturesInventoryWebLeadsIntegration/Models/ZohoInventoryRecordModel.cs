using Newtonsoft.Json;

namespace FPVenturesInventoryWebLeadsIntegration.Models
{
    public class ZohoInventoryRecordModel
    {
        public string Company { get; set; }

        [JsonProperty("cf_email")]
        public string Email { get; set; }

        [JsonProperty("cf_state")]
        public string State { get; set; }

        [JsonProperty("cf_phone")]
        public string Phone { get; set; }

        [JsonProperty("cf_name")]
        public string Name { get; set; }

        [JsonProperty("Lead_Source")]
        public string LeadSource { get; set; } = "Nationwide pest";
    }
}
