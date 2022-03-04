using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{

    public class ZohoInventoryInvoiceRequestModel
    {
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("contact_persons")]
        public List<string> ContactPersons { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }

    }

}
