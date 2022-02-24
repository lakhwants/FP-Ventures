using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class PageContext
	{
		public int page { get; set; }
		public int per_page { get; set; }
		public bool has_more_page { get; set; }
		public string report_name { get; set; }
		public string applied_filter { get; set; }
		public string sort_column { get; set; }
		public string sort_order { get; set; }
		public List<SearchCriteria> search_criteria { get; set; }
	}
}
