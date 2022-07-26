using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class InventoryItem
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("item_name")]
        public string ItemName { get; set; }

        [JsonProperty("unit")]
        public string Unit { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

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

        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

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

        [JsonProperty("cf_leads_date_time")]
        public DateTime CfLeadsDateTime { get; set; }

        [JsonProperty("cf_payout")]
        public string CfPayout { get; set; }

        public string cf_datetime { get; set; }
        public string cf_datetime_unformatted { get; set; }

        public string cf_isringbalead { get; set; }
        public bool cf_isringbalead_unformatted { get; set; }
        public string cf_isheyflowlead { get; set; }
        public bool cf_isheyflowlead_unformatted { get; set; }
        public string cf_isunbouncelead { get; set; }
        public bool cf_isunbouncelead_unformatted { get; set; }
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


}
