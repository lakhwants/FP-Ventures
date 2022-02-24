using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPVenturesZohoInventorySalesOrder.Models
{
    public class InventoryItem
    {
        public string item_id { get; set; }
        public string name { get; set; }
        public string item_name { get; set; }
        public string unit { get; set; }
        public string status { get; set; }
        public string source { get; set; }
        public bool is_combo_product { get; set; }
        public bool is_linked_with_zohocrm { get; set; }
        public string zcrm_product_id { get; set; }
        public string description { get; set; }
        public string brand { get; set; }
        public string manufacturer { get; set; }
        public double rate { get; set; }
        public string tax_id { get; set; }
        public string tax_name { get; set; }
        public int tax_percentage { get; set; }
        public string purchase_account_id { get; set; }
        public string purchase_account_name { get; set; }
        public string account_id { get; set; }
        public string account_name { get; set; }
        public string purchase_description { get; set; }
        public double purchase_rate { get; set; }
        public string item_type { get; set; }
        public string product_type { get; set; }
        public bool is_taxable { get; set; }
        public string tax_exemption_id { get; set; }
        public string tax_exemption_code { get; set; }
        public int stock_on_hand { get; set; }
        public bool has_attachment { get; set; }
        public bool is_returnable { get; set; }
        public int available_stock { get; set; }
        public double actual_available_stock { get; set; }
        public string sku { get; set; }
        public string upc { get; set; }
        public string ean { get; set; }
        public string isbn { get; set; }
        public string part_number { get; set; }
        public string reorder_level { get; set; }
        public string image_name { get; set; }
        public string image_type { get; set; }
        public string image_document_id { get; set; }
        public DateTime created_time { get; set; }
        public DateTime last_modified_time { get; set; }
        public bool show_in_storefront { get; set; }
        public string cf_vendor { get; set; }
        public string cf_vendor_unformatted { get; set; }
        public string cf_last_name { get; set; }
        public string cf_last_name_unformatted { get; set; }
        public string cf_phone { get; set; }
        public string cf_phone_unformatted { get; set; }
        public string cf_caller_id { get; set; }
        public string cf_caller_id_unformatted { get; set; }
        public string cf_campaign_name { get; set; }
        public string cf_campaign_name_unformatted { get; set; }
        public string cf_publisher_name { get; set; }
        public string cf_publisher_name_unformatted { get; set; }
        public string cf_duration { get; set; }
        public string cf_duration_unformatted { get; set; }
        public string cf_tagged_state { get; set; }
        public string cf_tagged_state_unformatted { get; set; }
        public DateTime cf_call_date_time { get; set; }
        public DateTime cf_call_date_time_unformatted { get; set; }
        public string cf_payout { get; set; }
        public string cf_payout_unformatted { get; set; }
        public string length { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string weight_unit { get; set; }
        public string dimension_unit { get; set; }
    }

    public class SearchCriteria
    {
        public string column_name { get; set; }
        public string search_text { get; set; }
        public string search_text_formatted { get; set; }
        public string comparator { get; set; }
    }

    public class ZohoInventoryItemModel : PageContext
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<InventoryItem> items { get; set; }
        public PageContext page_context { get; set; }
    }


}
