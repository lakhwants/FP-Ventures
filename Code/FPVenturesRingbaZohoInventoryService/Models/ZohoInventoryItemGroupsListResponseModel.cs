using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesRingbaZohoInventoryService.Models
{
    public class Itemgroup
    {
        [JsonProperty("group_id")]
        public string GroupId { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("last_modified_time")]
        public string LastModifiedTime { get; set; }

        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public class PageContext
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
    }

    public class ZohoInventoryItemGroupsListResponseModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("itemgroups")]
        public List<Itemgroup> ItemGroups { get; set; }

        [JsonProperty("page_context")]
        public PageContext PageContext { get; set; }
    }


}
