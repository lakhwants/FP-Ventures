using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class Salesorder
	{
		[JsonProperty("cf_start_date")]
		public string cfStartDate { get; set; }

		[JsonProperty("cf_end_date")]

		public string cfEndDate { get; set; }

		public string salesorder_id { get; set; }

		[JsonProperty("salesorder_number")]
		public string SalesOrderNumber { get; set; }

		[JsonProperty("date")]
		public string Date { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("reference_number")]
		public string ReferenceNumber { get; set; }

		[JsonProperty("customer_id")]
		public string CustomerId { get; set; }

		[JsonProperty("customer_name")]
		public string CustomerName { get; set; }

		[JsonProperty("source")]
		public string Source { get; set; }

		[JsonProperty("is_taxable")]
		public bool IsTaxable { get; set; }

		[JsonProperty("line_items")]
		public List<LineItem> LineItems { get; set; }
	}

	public class ZohoInventorySalesOrderResponseModel : InventoryResponse
	{
		[JsonProperty("salesorder")]
		public Salesorder SalesOrder { get; set; }
	}

}
