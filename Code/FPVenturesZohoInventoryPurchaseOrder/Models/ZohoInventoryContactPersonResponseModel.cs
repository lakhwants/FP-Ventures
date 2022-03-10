using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{

    public class ContactPerson
    {
        [JsonProperty("contact_person_id")]
        public string ContactPersonId { get; set; }

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

        [JsonProperty("is_primary_contact")]
        public bool IsPrimaryContact { get; set; }
    }
    public class ZohoInventoryContactPersonResponseModel : PageContext
	{
        [JsonProperty("contact_persons")]
        public List<ContactPerson> ContactPersons { get; set; }
	}
}
