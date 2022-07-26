using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBills.Models
{
    public class LineItem
    {
        public string name { get; set; }
        public long account_id { get; set; }
        public double rate { get; set; }
        public int quantity { get; set; }
    }

    public class CustomField
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ZohoInventoryBillsModel
    {
        public string vendor_id { get; set; }
        public string bill_number { get; set; }
        public string date { get; set; }
        public string due_date { get; set; }
        public List<LineItem> line_items { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }
    }


}
