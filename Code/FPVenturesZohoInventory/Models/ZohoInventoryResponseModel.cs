using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventory.Models
{
    public class CustomFieldMetaDataModel
   {
        [JsonProperty("customfield_id")]
        public string CustomFieldID { get; set; }

        [JsonProperty("show_in_store")]
        public bool ShowInStore { get; set; }

        [JsonProperty("show_in_portal")]
        public bool ShowInPortal { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("show_on_pdf")]
        public bool ShowOnPdf { get; set; }

        [JsonProperty("edit_on_portal")]
        public bool EditOnPortal { get; set; }

        [JsonProperty("edit_on_store")]
        public bool EditOnStore { get; set; }

        [JsonProperty("show_in_all_pdf")]
        public bool ShowInAllPdf { get; set; }

        [JsonProperty("value_formatted")]
        public string ValueFormatted { get; set; }

        [JsonProperty("search_entity")]
        public string SearchEntity { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("placeholder")]
        public string Placeholder { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("is_dependent_field")]
        public bool IsDependentField { get; set; }
    }

    public class CustomFieldHash
    {
        [JsonProperty("cf_vendor")]
        public string Vendor { get; set; }

        [JsonProperty("cf_vendor_unformatted")]
        public string VendorUnformatted { get; set; }

        [JsonProperty("cf_first_name")]
        public string FirstName { get; set; }

        [JsonProperty("cf_first_name_unformatted")]
        public string FirstNameUnformatted { get; set; }

        [JsonProperty("cf_last_name")]
        public string LastName { get; set; }

        [JsonProperty("cf_last_name_unformatted")]
        public string LastNameUnformatted { get; set; }

        [JsonProperty("cf_email")]
        public string Email { get; set; }

        [JsonProperty("cf_email_unformatted")]
        public string EmailUnformatted { get; set; }

        [JsonProperty("cf_phone")]
        public string Phone { get; set; }

        [JsonProperty("cf_phone_unformatted")]
        public string PhoneUnformatted { get; set; }

        [JsonProperty("cf_caller_id")]
        public string CallerID { get; set; }

        [JsonProperty("cf_caller_id_unformatted")]
        public string CallerIDUnformatted { get; set; }

        [JsonProperty("cf_campaign_name")]
        public string CampaignName { get; set; }

        [JsonProperty("is_dependent_field")]
        public string CampaignNameUnformatted { get; set; }

        [JsonProperty("is_dependent_field")]
        public string PublisherName { get; set; }

        [JsonProperty("cf_publisher_name_unformatted")]
        public string PublisherNameUnformatted { get; set; }

        [JsonProperty("cf_inbound_call_id")]
        public string InboundCallID { get; set; }

        [JsonProperty("cf_inbound_call_id_unformatted")]
        public string InboundCallIDUnformatted { get; set; }

        [JsonProperty("cf_duration")]
        public string Duration { get; set; }

        [JsonProperty("cf_duration_unformatted")]
        public string DurationUnformatted { get; set; }

        [JsonProperty("cf_payout")]
        public string Payout { get; set; }

        [JsonProperty("cf_payout_unformatted")]
        public double PayoutUnformatted { get; set; }

        [JsonProperty("cf_tagged_state")]
        public string TaggedState { get; set; }

        [JsonProperty("cf_tagged_state_unformatted")]
        public string TaggedStateUnformatted { get; set; }

        [JsonProperty("cf_leads_date_time")]
        public string LeadsDateTime { get; set; }

        [JsonProperty("cf_leads_date_time_unformatted")]
        public string LeadsDateTimeUnformatted { get; set; }

        [JsonProperty("cf_call_date_time")]
        public string CallDateTime { get; set; }

        [JsonProperty("cf_call_date_time_unformatted")]
        public string CallDateTimeUnformatted { get; set; }
    }

    public class PackageDetails
    {
        [JsonProperty("length")]
        public string Length { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("weight_unit")]
        public string WeightUnit { get; set; }

        [JsonProperty("dimension_unit")]
        public string DimensionUnit { get; set; }
    }

    public class Item
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sku")]
        public string Sku { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("category_id")]
        public string CategoryId { get; set; }

        [JsonProperty("category_name")]
        public string CategoryName { get; set; }

        [JsonProperty("image_name")]
        public string ImageName { get; set; }

        [JsonProperty("image_type")]
        public string ImageType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("is_linked_with_zohocrm")]
        public bool IsLinkedWithZohocrm { get; set; }

        [JsonProperty("zcrm_product_id")]
        public string ZohoCRMProductId { get; set; }

        [JsonProperty("crm_owner_id")]
        public string CRMOwnerId { get; set; }

        [JsonProperty("unit")]
        public string unit { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rate")]
        public double Rate { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }

        [JsonProperty("account_name")]
        public string AccountName { get; set; }

        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("tax_percentage")]
        public int TaxPercentage { get; set; }

        [JsonProperty("tax_type")]
        public string TaxType { get; set; }

        [JsonProperty("is_default_tax_applied")]
        public bool IsDefaultTaxApplied { get; set; }

        [JsonProperty("associated_template_id")]
        public string AssociatedTemplateId { get; set; }

        [JsonProperty("documents")]
        public List<object> Documents { get; set; }

        [JsonProperty("purchase_description")]
        public string PurchaseDescription { get; set; }

        [JsonProperty("pricebook_rate")]
        public double PriceBookRate { get; set; }

        [JsonProperty("sales_rate")]
        public double SalesRate { get; set; }

        [JsonProperty("purchase_rate")]
        public double PurchaseRate { get; set; }

        [JsonProperty("purchase_account_id")]
        public string PurchaseAccountId { get; set; }

        [JsonProperty("purchase_account_name")]
        public string PurchaseAccountName { get; set; }

        [JsonProperty("inventory_account_id")]
        public string InventoryAccountId { get; set; }

        [JsonProperty("inventory_account_name")]
        public string InventoryAccountName { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("offline_created_date_with_time")]
        public string OfflineCreatedDateWithTime { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("item_type")]
        public string ItemType { get; set; }

        [JsonProperty("product_type")]
        public string ProductType { get; set; }

        [JsonProperty("is_returnable")]
        public bool IsReturnable { get; set; }

        [JsonProperty("minimum_order_quantity")]
        public string MinimumOrderQuantity { get; set; }

        [JsonProperty("maximum_order_quantity")]
        public string MaximumOrderQuantity { get; set; }

        [JsonProperty("initial_stock")]
        public string InitialStock { get; set; }

        [JsonProperty("initial_stock_rate")]
        public string InitialStockRate { get; set; }

        [JsonProperty("vendor_id")]
        public string VendorId { get; set; }

        [JsonProperty("vendor_name")]
        public string VendorName { get; set; }

        [JsonProperty("custom_fields")]
        public List<CustomFieldMetaDataModel> CustomFields { get; set; }

        [JsonProperty("custom_field_hash")]
        public CustomFieldHash CustomFieldHash { get; set; }

        [JsonProperty("upc")]
        public string UPC { get; set; }

        [JsonProperty("ean")]
        public string EAN { get; set; }

        [JsonProperty("isbn")]
        public string IsBN { get; set; }

        [JsonProperty("part_number")]
        public string PartNumber { get; set; }

        [JsonProperty("is_combo_product")]
        public bool IsComboProduct { get; set; }

        [JsonProperty("sales_channels")]
        public List<object> SalesChannels { get; set; }

        [JsonProperty("preferred_vendors")]
        public List<object> PreferredVendors { get; set; }

        [JsonProperty("package_details")]
        public PackageDetails PackageDetails { get; set; }
    }

    public class ZohoInventoryResponseModel
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("item")]
        public Item Item { get; set; }
    }



}
