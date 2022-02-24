using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventory.Models
{
	public class CustomFieldResponseModel
    {
        public string customfield_id { get; set; }
        public bool show_in_store { get; set; }
        public bool show_in_portal { get; set; }
        public bool is_active { get; set; }
        public int index { get; set; }
        public string label { get; set; }
        public bool show_on_pdf { get; set; }
        public bool edit_on_portal { get; set; }
        public bool edit_on_store { get; set; }
        public bool show_in_all_pdf { get; set; }
        public string value_formatted { get; set; }
        public string search_entity { get; set; }
        public string data_type { get; set; }
        public string placeholder { get; set; }
        public object value { get; set; }
        public bool is_dependent_field { get; set; }
    }

    public class CustomFieldHash
    {
        public string cf_vendor { get; set; }
        public string cf_vendor_unformatted { get; set; }
        public string cf_first_name { get; set; }
        public string cf_first_name_unformatted { get; set; }
        public string cf_last_name { get; set; }
        public string cf_last_name_unformatted { get; set; }
        public string cf_email { get; set; }
        public string cf_email_unformatted { get; set; }
        public string cf_phone { get; set; }
        public string cf_phone_unformatted { get; set; }
        public string cf_caller_id { get; set; }
        public string cf_caller_id_unformatted { get; set; }
        public string cf_campaign_name { get; set; }
        public string cf_campaign_name_unformatted { get; set; }
        public string cf_publisher_name { get; set; }
        public string cf_publisher_name_unformatted { get; set; }
        public string cf_inbound_call_id { get; set; }
        public string cf_inbound_call_id_unformatted { get; set; }
        public string cf_duration { get; set; }
        public string cf_duration_unformatted { get; set; }
        public string cf_payout { get; set; }
        public double cf_payout_unformatted { get; set; }
        public string cf_tagged_state { get; set; }
        public string cf_tagged_state_unformatted { get; set; }
        public string cf_leads_date_time { get; set; }
        public string cf_leads_date_time_unformatted { get; set; }
        public string cf_call_date_time { get; set; }
        public string cf_call_date_time_unformatted { get; set; }
    }

    public class PackageDetails
    {
        public string length { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string weight_unit { get; set; }
        public string dimension_unit { get; set; }
    }

    public class Item
    {
        public string item_id { get; set; }
        public string name { get; set; }
        public string sku { get; set; }
        public string brand { get; set; }
        public string manufacturer { get; set; }
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string image_name { get; set; }
        public string image_type { get; set; }
        public string status { get; set; }
        public string source { get; set; }
        public bool is_linked_with_zohocrm { get; set; }
        public string zcrm_product_id { get; set; }
        public string crm_owner_id { get; set; }
        public string unit { get; set; }
        public string description { get; set; }
        public double rate { get; set; }
        public string account_id { get; set; }
        public string account_name { get; set; }
        public string tax_id { get; set; }
        public string tax_name { get; set; }
        public int tax_percentage { get; set; }
        public string tax_type { get; set; }
        public bool is_default_tax_applied { get; set; }
        public string associated_template_id { get; set; }
        public List<object> documents { get; set; }
        public string purchase_description { get; set; }
        public double pricebook_rate { get; set; }
        public double sales_rate { get; set; }
        public double purchase_rate { get; set; }
        public string purchase_account_id { get; set; }
        public string purchase_account_name { get; set; }
        public string inventory_account_id { get; set; }
        public string inventory_account_name { get; set; }
        public DateTime created_time { get; set; }
        public string offline_created_date_with_time { get; set; }
        public DateTime last_modified_time { get; set; }
        public List<object> tags { get; set; }
        public string item_type { get; set; }
        public string product_type { get; set; }
        public bool is_returnable { get; set; }
        public string minimum_order_quantity { get; set; }
        public string maximum_order_quantity { get; set; }
        public string initial_stock { get; set; }
        public string initial_stock_rate { get; set; }
        public string vendor_id { get; set; }
        public string vendor_name { get; set; }

        public List<CustomFieldResponseModel> custom_fields { get; set; }
        public CustomFieldHash custom_field_hash { get; set; }
        public string upc { get; set; }
        public string ean { get; set; }
        public string isbn { get; set; }
        public string part_number { get; set; }
        public bool is_combo_product { get; set; }
        public List<object> sales_channels { get; set; }
        public List<object> preferred_vendors { get; set; }
        public PackageDetails package_details { get; set; }
    }

    public class ZohoInventoryResponseModel
    {
        public int code { get; set; }
        public string message { get; set; }
        public Item item { get; set; }
    }



}
