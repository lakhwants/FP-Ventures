using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{

    public class Contact
    {
        [JsonProperty("contact_id")]
        public string ContactId { get; set; }

        [JsonProperty("contact_name")]
        public string ContactName { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("contact_type")]
        public string ContactType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("customer_sub_type")]
        public string CustomerSubType { get; set; }

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

    public class ZohoInventoryContactsResponseModel : PageContext
    {
        [JsonProperty("contacts")]
        public List<Contact> Contacts { get; set; }

    }


}
