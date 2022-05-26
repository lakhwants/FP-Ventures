using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventoryService.Models
{
    public class ItemGroup
    {
        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class ZohoInventoryItemGroupReponseModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("item_group")]
        public ItemGroup ItemGroup { get; set; }
    }


}
