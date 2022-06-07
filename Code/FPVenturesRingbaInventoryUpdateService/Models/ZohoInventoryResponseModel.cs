using Newtonsoft.Json;

namespace FPVenturesRingbaInventoryUpdateService.Models
{

    public class Item
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sku")]
        public string SKU { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }
    }

    public class ZohoInventoryResponseModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }
    }

}
