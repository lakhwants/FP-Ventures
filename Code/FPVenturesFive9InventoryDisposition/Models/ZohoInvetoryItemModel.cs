using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesFive9InventoryDisposition.Models
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

        [JsonProperty("is_combo_product")]
        public bool IsComboProduct { get; set; }

        [JsonProperty("is_linked_with_zohocrm")]
        public bool IsLinkedWithZohocrm { get; set; }

        [JsonProperty("zcrm_product_id")]
        public string ZcrmProductId { get; set; }

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

        [JsonProperty("purchase_description")]
        public string PurchaseDescription { get; set; }

        [JsonProperty("purchase_rate")]
        public double PurchaseRate { get; set; }

        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("is_taxable")]
        public bool IsTaxable { get; set; }

        [JsonProperty("tax_exemption_id")]
        public string TaxExemptionId { get; set; }

        [JsonProperty("tax_exemption_code")]
        public string tax_exemption_code { get; set; }

        [JsonProperty("stock_on_hand")]
        public int stock_on_hand { get; set; }

        [JsonProperty("has_attachment")]
        public bool has_attachment { get; set; }

        [JsonProperty("is_returnable")]
        public bool is_returnable { get; set; }

        [JsonProperty("available_stock")]
        public int available_stock { get; set; }

        [JsonProperty("actual_available_stock")]
        public double actual_available_stock { get; set; }

        [JsonProperty("sku")]
        public string SKU { get; set; }

        [JsonProperty("upc")]
        public string UPC { get; set; }

        [JsonProperty("ean")]
        public string EAN { get; set; }

        [JsonProperty("isbn")]
        public string IsBN { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }

        [JsonProperty("show_in_storefront")]
        public bool ShowInStorefront { get; set; }

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
