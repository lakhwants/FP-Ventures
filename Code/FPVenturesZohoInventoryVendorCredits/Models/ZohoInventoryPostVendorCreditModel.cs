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
    }

    public class ZohoInventoryPostVendorCreditModel
    {
        public string vendor_id { get; set; }
        public string date { get; set; }
        public bool is_inclusive_tax { get; set; }
        public List<LineItem> line_items { get; set; }
    }


}
