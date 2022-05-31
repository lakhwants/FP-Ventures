using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{

	public class ValueColumn
	{
		[JsonProperty("column")]
		public string Column { get; set; }

	}

	public class OrderByColumn
	{
		[JsonProperty("column")]
		public string Column { get; set; }

		[JsonProperty("direction")]
		public string Direction { get; set; }
	}

	public class CallLogsColumnModel
	{
		[JsonProperty("reportEnd")]
		public DateTime ReportEnd { get; set; }
		[JsonProperty("reportStart")]
		public DateTime ReportStart { get; set; }
		[JsonProperty("valueColumns")]
		public List<ValueColumn> ValueColumns { get; set; }

		[JsonProperty("orderByColumns")]
		public List<OrderByColumn> OrderByColumns { get; set; }
		public int Size { get; set; }
	}

}
