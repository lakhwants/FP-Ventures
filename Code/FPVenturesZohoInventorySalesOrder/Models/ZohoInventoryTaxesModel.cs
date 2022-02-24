using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
	public class Tax
    {
        public string tax_id { get; set; }
        public string tax_name { get; set; }
        public int tax_percentage { get; set; }
        public string tax_type { get; set; }
        public string tax_specific_type { get; set; }
        public string tax_authority_id { get; set; }
        public string tax_authority_name { get; set; }
        public bool is_value_added { get; set; }
        public bool is_default_tax { get; set; }
        public bool is_editable { get; set; }
    }



    public class ZohoInventoryTaxesModel : PageContext
    {
        public int code { get; set; }
        public string message { get; set; }
        public List<Tax> taxes { get; set; }
        public PageContext page_context { get; set; }
    }

}
