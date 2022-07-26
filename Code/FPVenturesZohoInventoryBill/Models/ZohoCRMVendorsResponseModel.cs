using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBill.Models
{

    public class VendorCRM
    {
        [JsonProperty("Vendor_Abbreviation")]
        public string VendorAbbreviation { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        [JsonProperty("Vendor_Name")]
        public string VendorName { get; set; }
        public string Phone { get; set; }
        public string Street { get; set; }

        [JsonProperty("Zip_Code")]
        public string ZipCode { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("Publisher_Name")]
        public string PublisherName { get; set; }

    }

    public class Info
    {
        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("more_records")]
        public bool MoreRecords { get; set; }
    }

    public class ZohoCRMVendorsResponseModel
    {
        [JsonProperty("data")]
        public List<VendorCRM> Data { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }
}
