using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Models
{
    public class ZohoInventoryVendorCreditResponseModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("vendor_credit")]
        public VendorCredit VendorCredit { get; set; }
    }

    public class VendorCredit
    {
        [JsonProperty("vendor_credit_id")]
        public string VendorCreditId { get; set; }

        [JsonProperty("vendor_credit_number")]
        public string VendorCreditNumber { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty("vendor_name")]
        public string VendorName { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }
    }
}
