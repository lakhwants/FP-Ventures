using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesRingbaInventoryUpdateService.Models
{
    public class VendorInventory
    {
        [JsonProperty("contact_id")]
        public string ContactId { get; set; }

        [JsonProperty("contact_name")]
        public string ContactName { get; set; }

        [JsonProperty("vendor_name")]
        public string VendorName { get; set; }

        [JsonProperty("company_name")]
        public string CompanyName { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("is_linked_with_zohocrm")]
        public bool IsLinkedWithZohocrm { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }
    }

    public class ZohoInventoryVendorsResponseModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("contacts")]
        public List<VendorInventory> Contacts { get; set; }

        [JsonProperty("page_context")]
        public PageContext PageContext { get; set; }
    }


}
