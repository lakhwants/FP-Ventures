using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventory.Models
{
	public class CustomField
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class ZohoInventoryModel
    {
        public string name { get; set; }
        public string status { get; set; }
        public string source { get; set; }
        public string description { get; set; }
        public string product_type { get; set; }
        public DateTime created_time { get; set; }
        public DateTime last_modified_time { get; set; }
        public List<CustomField> custom_fields { get; set; }
    }




}
