using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPVenturesZohoInventoryPurchaseOrder.Models
{
    public class LineItem
    {
        public string item_id { get; set; }
        public long account_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int item_order { get; set; }
        public int bcy_rate { get; set; }
        public int purchase_rate { get; set; }
        public int quantity { get; set; }
        public int quantity_received { get; set; }
        public string unit { get; set; }
        public int item_total { get; set; }
        public long tax_id { get; set; }
        public string tax_name { get; set; }
        public string tax_type { get; set; }
        public int tax_percentage { get; set; }
        public long image_id { get; set; }
        public string image_name { get; set; }
        public string image_type { get; set; }
        public long reverse_charge_tax_id { get; set; }
        public string hsn_or_sac { get; set; }
        public string tax_exemption_code { get; set; }
        public long warehouse_id { get; set; }
        public string tax_exemption_id { get; set; }
        public long salesorder_item_id { get; set; }
    }

    public class Document
    {
        public bool can_send_in_mail { get; set; }
        public string file_name { get; set; }
        public string file_type { get; set; }
        public string file_size_formatted { get; set; }
        public int attachment_order { get; set; }
        public long document_id { get; set; }
        public int file_size { get; set; }
    }

    public class ZohoInventoryPurchaseOrderRequestModel
    {
        public string purchaseorder_number { get; set; }
        public string date { get; set; }
        public string delivery_date { get; set; }
        public string reference_number { get; set; }
        public string ship_via { get; set; }
        public long vendor_id { get; set; }
        public long salesorder_id { get; set; }
        public bool is_drop_shipment { get; set; }
        public bool is_inclusive_tax { get; set; }
        public bool is_backorder { get; set; }
        public long template_id { get; set; }
        public long contact_persons { get; set; }
        public string attention { get; set; }
        public long delivery_org_address_id { get; set; }
        public long delivery_customer_id { get; set; }
        public string notes { get; set; }
        public string terms { get; set; }
        public int exchange_rate { get; set; }
        public List<LineItem> line_items { get; set; }
        public List<Document> documents { get; set; }
        public string gst_treatment { get; set; }
        public string gst_no { get; set; }
        public string source_of_supply { get; set; }
        public string destination_of_supply { get; set; }
    }


}
