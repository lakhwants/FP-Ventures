using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBills.Models
{
	public class InventoryItem
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("is_linked_with_zohocrm")]
        public bool IsLinkedWithZohocrm { get; set; }

        [JsonProperty("zcrm_product_id")]
        public string ZcrmProductId { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("tax_percentage")]
        public int TaxPercentage { get; set; }

        [JsonProperty("purchase_account_id")]
        public string PurchaseAccountId { get; set; }

        [JsonProperty("purchase_account_name")]
        public string PurchaseAccountName { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("purchase_rate")]
        public double PurchaseRate { get; set; }

        [JsonProperty("sku")]
        public string SKU { get; set; }

        [JsonProperty("cf_vendor")]
        public string CfVendor { get; set; }

        [JsonProperty("cf_last_name")]
        public string CfLastName { get; set; }

        [JsonProperty("cf_phone")]
        public string CfPhone { get; set; }

        [JsonProperty("cf_caller_id")]
        public string CfCallerId { get; set; }

        [JsonProperty("cf_campaign_name")]
        public string CfCampaignName { get; set; }

        [JsonProperty("cf_publisher_name")]
        public string CfPublisherName { get; set; }

        [JsonProperty("cf_target_name")]
        public string CfTargetName { get; set; }

        [JsonProperty("cf_duration")]
        public string CfDuration { get; set; }

        [JsonProperty("cf_tagged_state")]
        public string CfTaggedState { get; set; }

        [JsonProperty("cf_call_date_time")]
        public DateTime CfCallDateTime { get; set; }

        [JsonProperty("cf_payout")]
        public string CfPayout { get; set; }

        public string VendorName { get; set; }

        public string VendorId { get; set; }

        public string VendorAbbreviation { get; set; }
    }

    public class SearchCriteria
    {
        [JsonProperty("cf_payout")]
        public string ColumnName { get; set; }

        [JsonProperty("cf_payout")]
        public string CfPayout { get; set; }

        [JsonProperty("search_text_formatted")]
        public string SearchTextFormatted { get; set; }

        [JsonProperty("comparator")]
        public string Comparator { get; set; }
    }

    public class ZohoInventoryItemModel : InventoryResponse
    {
        [JsonProperty("items")]
        public List<InventoryItem> Items { get; set; }
        public PageContext page_context { get; set; }
    }

    public class InventoryResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
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


}
