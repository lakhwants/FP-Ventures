using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryBill.Models
{
	public class Tax
    {
        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("tax_percentage")]
        public int TaxPercentage { get; set; }

        [JsonProperty("tax_type")]
        public string TaxType { get; set; }

        [JsonProperty("tax_specific_type")]
        public string TaxSpecificType { get; set; }

        [JsonProperty("tax_authority_id")]
        public string TaxAuthorityId { get; set; }

        [JsonProperty("tax_authority_name")]
        public string TaxAuthorityName { get; set; }

        [JsonProperty("is_value_added")]
        public bool IsValueAdded { get; set; }

        [JsonProperty("is_default_tax")]
        public bool IsDefaultTax { get; set; }

        [JsonProperty("is_editable")]
        public bool IsEditable { get; set; }
    }

    public class ZohoInventoryTaxesModel : PageContext
    {
        [JsonProperty("taxes")]
        public List<Tax> Taxes { get; set; }
    }

}
