using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBill.Models
{
    public class LineItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }
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
        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty("bill_number")]
        public string BillNumber { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("due_date")]
        public string DueDate { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomField> CustomFields { get; set; }
    }


}
