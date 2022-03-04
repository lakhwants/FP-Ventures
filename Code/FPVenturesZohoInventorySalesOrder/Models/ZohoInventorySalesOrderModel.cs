using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class LineItem : CustomField
	{
		[JsonProperty("item_id")]
		public string ItemId { get; set; }

		[JsonProperty("sku")]
		public string SKU { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("rate")]
		public string Rate { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; } = 1;

		[JsonProperty("unit")]
		public string Unit { get; set; } = "count";

		[JsonProperty("tax_id")]
		public string TaxId { get; set; }
	}

	public class ZohoInventorySalesOrderModel
	{
		[JsonProperty("customer_id")]
		public string CustomerId { get; set; }

		[JsonProperty("line_items")]
		public List<LineItem> LineItems { get; set; }
	}
}
