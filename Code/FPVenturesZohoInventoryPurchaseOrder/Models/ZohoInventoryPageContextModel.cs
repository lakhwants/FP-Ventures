using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryPurchaseOrder.Models
{
	public class InventoryResponse
	{
		[JsonProperty("code")]
		public int Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }
	}

	public class PageContext : InventoryResponse
	{
		[JsonProperty("page")]
		public int Page { get; set; }

		[JsonProperty("per_page")]
		public int PerPage { get; set; }

		[JsonProperty("has_more_page")]
		public bool HasMorePage { get; set; }

		[JsonProperty("report_name")]
		public string ReportName { get; set; }

		[JsonProperty("applied_filter")]
		public string AppliedFilter { get; set; }

		[JsonProperty("sort_column")]
		public string SortColumn { get; set; }

		[JsonProperty("sort_order")]
		public string SortOrder { get; set; }

		[JsonProperty("search_criteria")]
		public List<SearchCriteria> SearchCriteria { get; set; }
	}
}
