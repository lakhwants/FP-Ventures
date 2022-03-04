using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventorySalesOrder.Models
{
    public class ItemCustomField
    {
        [JsonProperty("customfield_id")]
        public string CustomFieldId { get; set; }

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
        public string PlaceHolder { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("is_dependent_field")]
        public bool IsDependentField { get; set; }
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

        [JsonProperty("recurring_invoice_id")]
        public string RecurringInvoiceId { get; set; }

        [JsonProperty("payment_made")]
        public double PaymentMade { get; set; }

        [JsonProperty("reference_number")]
        public string ReferenceNumber { get; set; }

        [JsonProperty("line_items")]
        public List<LineItem> LineItems { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }

        [JsonProperty("balance")]
        public double Balance { get; set; }

        [JsonProperty("write_off_amount")]
        public double WriteOffAmount { get; set; }

        [JsonProperty("roundoff_value")]
        public double RoundOffValue { get; set; }

        [JsonProperty("tax_id")]
        public string TaxId { get; set; }

        [JsonProperty("tax_name")]
        public string TaxName { get; set; }

        [JsonProperty("tax_percentage")]
        public double TaxPercentage { get; set; }

        [JsonProperty("taxes")]
        public List<Tax> Taxes { get; set; }

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

        [JsonProperty("submitter_id")]
        public string SubmitterId { get; set; }

        [JsonProperty("approver_id")]
        public string ApproverId { get; set; }

        [JsonProperty("submitted_date")]
        public string SubmittedDate { get; set; }

        [JsonProperty("submitted_by")]
        public string SubmittedBy { get; set; }

        [JsonProperty("created_time")]
        public DateTime CreatedTime { get; set; }

        [JsonProperty("last_modified_time")]
        public DateTime LastModifiedTime { get; set; }

        [JsonProperty("created_date")]
        public string CreatedDate { get; set; }

        [JsonProperty("created_by_id")]
        public string CreatedById { get; set; }

        [JsonProperty("last_modified_by_id")]
        public string LastModifiedById { get; set; }
    }

    public class ZohoInventoryInvoiceResponseModel : InventoryResponse
    {
        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }
    }


}
