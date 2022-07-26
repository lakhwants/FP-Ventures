using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Models
{
    public class LineItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("account_id")]
        public string AccountID { get; set; } = "2762310000000192193";
    }

    public class ZohoInventoryPostVendorCreditModel
    {
        public string vendor_id { get; set; }
        public string date { get; set; }
        public List<LineItem> line_items { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }
    }


}
