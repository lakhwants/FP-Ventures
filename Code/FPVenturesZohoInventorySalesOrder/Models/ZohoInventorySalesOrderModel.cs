using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class LineItem
	{
		public string item_id { get; set; }
		public string sku { get; set; }
		public string name { get; set; }
		public string description { get; set; }
		public int rate { get; set; }
		public int quantity { get; set; } = 1; 
		public string unit { get; set; } = "count";
		public string tax_id { get; set; }
	}

	public class ZohoInventorySalesOrderModel
	{
		public long customer_id { get; set; }
		public List<LineItem> line_items+ { get; set; }
	}
}
