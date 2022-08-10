using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
    public class ItemCustomField
    {
        [JsonProperty("customfield_id")]
        public string CustomFieldId { get; set; }

        [JsonProperty("is_active")]
        public bool IsActive { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ContactPersonsDetail
    {
        [JsonProperty("contact_person_id")]
        public string ContactPersonId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("is_primary_contact")]
        public bool IsPrimaryContact { get; set; }
    }

    public class Invoice
    {
        [JsonProperty("invoice_id")]
        public string InvoiceId { get; set; }

        [JsonProperty("invoice_number")]
        public string InvoiceNumber { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        [JsonProperty("customer_name")]
        public string CustomerName { get; set; }

        [JsonProperty("customer_custom_fields")]
        public List<object> CustomerCustomFields { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("custom_fields")]
        public List<object> CustomFields { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("contact_persons")]
        public List<object> ContactPersons { get; set; }

        [JsonProperty("salesorder_id")]
        public string SalesorderId { get; set; }

        [JsonProperty("salesorder_number")]
        public string SalesorderNumber { get; set; }

        [JsonProperty("salesorders")]
        public List<object> SalesOrders { get; set; }

        [JsonProperty("salesperson_id")]
        public string SalespersonId { get; set; }

        [JsonProperty("salesperson_name")]
        public string SalespersonName { get; set; }

        [JsonProperty("is_emailed")]
        public bool IsEmailed { get; set; }

    }

    public class ZohoInventoryInvoiceResponseModel : InventoryResponse
    {
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }
    }


}
