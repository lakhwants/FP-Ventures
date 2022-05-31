using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class LineItem : CustomField
	{

		[JsonProperty("name")]
		public string Name { get; set; }

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

		[JsonProperty("invoiced_status")]
        public string InvoicedStatus { get; set; }

        [JsonProperty("customer_id")]
		public string CustomerId { get; set; }

		[JsonProperty("line_items")]
		public List<LineItem> LineItems { get; set; }
	}
}
