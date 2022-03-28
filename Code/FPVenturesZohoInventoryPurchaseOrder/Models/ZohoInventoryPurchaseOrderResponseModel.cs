using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryPurchaseOrder.Models
{
	public class PurchaseOrder
	{
		public long purchaseorder_id { get; set; }
		public List<Document> documents { get; set; }
		public string purchaseorder_number { get; set; }
		public string date { get; set; }
		public string expected_delivery_date { get; set; }
		public string date_formatted { get; set; }
		public string expected_delivery_date_formatted { get; set; }
		public string delivery_date_formatted { get; set; }
		public string status_formatted { get; set; }
		public int billed_status { get; set; }
		public bool is_emailed { get; set; }
		public bool is_inclusive_tax { get; set; }
		public bool is_backorder { get; set; }
		public string reference_number { get; set; }
		public string status { get; set; }
		public long vendor_id { get; set; }
		public string vendor_name { get; set; }
		public long contact_persons { get; set; }
		public long currency_id { get; set; }
		public string currency_code { get; set; }
		public string currency_symbol { get; set; }
		public int exchange_rate { get; set; }
		public string delivery_date { get; set; }
		public long salesorder_id { get; set; }
		public bool is_drop_shipment { get; set; }
		public List<LineItem> line_items { get; set; }
		public int sub_total { get; set; }
		public string sub_total_formatted { get; set; }
		public int tax_total { get; set; }
		public int total { get; set; }
		public List<Tax> taxes { get; set; }
		public int price_precision { get; set; }
		public long pricebook_id { get; set; }
		public string notes { get; set; }
		public string terms { get; set; }
		public string ship_via { get; set; }
		public long ship_via_id { get; set; }
		public string attention { get; set; }
		public long delivery_org_address_id { get; set; }
		public long delivery_customer_id { get; set; }
		public string delivery_customer_name { get; set; }
		public string attachment_name { get; set; }
		public bool can_send_in_mail { get; set; }
		public long template_id { get; set; }
		public string template_name { get; set; }
		public string template_type { get; set; }
		public DateTime created_time { get; set; }
		public DateTime last_modified_time { get; set; }
		public string gst_treatment { get; set; }
		public string gst_no { get; set; }
		public string source_of_supply { get; set; }
		public string destination_of_supply { get; set; }
		public bool is_pre_gst { get; set; }
		public bool is_reverse_charge_applied { get; set; }
	}

	public class ZohoInventoryPurchaseOrderResponseModel : InventoryResponse
	{
		public PurchaseOrder purchase_order { get; set; }
	}

}
