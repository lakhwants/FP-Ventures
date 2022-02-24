using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class Salesorder : LineItem
	{
		public string salesorder_id { get; set; }
		public string salesorder_number { get; set; }
		public string date { get; set; }
		public string status { get; set; }
		public string reference_number { get; set; }
		public string customer_id { get; set; }
		public string customer_name { get; set; }
		public string source { get; set; }
		public bool is_taxable { get; set; }
		public List<LineItem> line_items { get; set; }
	}

	public class ZohoInventorySalesOrderResponseModel
	{
		public int code { get; set; }
		public string message { get; set; }
		public Salesorder salesorder { get; set; }
	}

}
