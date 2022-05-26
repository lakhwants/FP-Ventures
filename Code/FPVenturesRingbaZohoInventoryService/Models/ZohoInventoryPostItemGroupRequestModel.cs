using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventoryService.Models
{

    public class ZohoInventoryPostItemGroupRequestModel
    {
        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}
