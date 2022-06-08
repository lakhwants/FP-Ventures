using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesRingbaInventoryUpdateService.Models
{
    public class CustomField
	{
		[JsonProperty("label")]
		public string Label { get; set; }

		[JsonProperty("value")]
		public string Value { get; set; }
	}

	public class ZohoInventoryModel
	{
		[JsonProperty("custom_fields")]
		public List<CustomField> CustomFields { get; set; }

		[JsonProperty("item_id")]
        public string ItemId { get; set; }
		
        [JsonProperty("sku")]
		public string SKU { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

    }
}
