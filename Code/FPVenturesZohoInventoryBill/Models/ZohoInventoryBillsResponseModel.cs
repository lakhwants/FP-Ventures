using Newtonsoft.Json;

namespace FPVenturesZohoInventoryBill.Models
{
    public class Bill
    {
        [JsonProperty("bill_id")]
        public string BillId { get; set; }

        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty("vendor_name")]
        public string VendorName { get; set; }
    }

    public class ZohoInventoryBillsResponseModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("bill")]
        public Bill Bill { get; set; }
    }


}
