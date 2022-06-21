using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Models
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
		[JsonProperty("item_type")]
		public string ItemType { get; set; }

        [JsonProperty("purchase_account_name")]
        public string PurchaseAccountName { get; set; }

        [JsonProperty("vendor_id")]
		public string VendorId{ get; set; }

		[JsonProperty("group_id")]
		public string GroupId { get; set; }

        [JsonProperty("track_serial_number")]
		public bool TrackSerialNumber { get; set; }

		[JsonProperty("purchase_rate")]
		public double PurchaseRate { get; set; }

		[JsonProperty("rate")]
		public double Rate { get; set; }

        [JsonProperty("sku")]
		public string SKU { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("source")]
		public string Source { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("product_type")]
		public string ProductType { get; set; }

		[JsonProperty("created_time")]
		public DateTime CreatedTime { get; set; }

		[JsonProperty("last_modified_time")]
		public DateTime LastModifiedTime { get; set; }

		[JsonProperty("custom_fields")]
		public List<CustomField> CustomFields { get; set; }

		[JsonProperty("is_returnable")]
        public bool IsReturnable { get; set; }
    }
}
