using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesPestRoutes.Models
{
	public class Customer
    {
        [JsonProperty("customerID")]
        public string CustomerID { get; set; }

        [JsonProperty("billToAccountID")]
        public string BillToAccountID { get; set; }

        [JsonProperty("officeID")]
        public string OfficeID { get; set; }

        [JsonProperty("fname")]
        public string Fname { get; set; }

        [JsonProperty("lname")]
        public string Lname { get; set; }

        [JsonProperty("companyName")]
        public string CompanyName { get; set; }

        [JsonProperty("commercialAccount")]
        public string CommercialAccount { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("statusText")]
        public string StatusText { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone1")]
        public string Phone1 { get; set; }

        [JsonProperty("ext1")]
        public string Ext1 { get; set; }

        [JsonProperty("phone2")]
        public string Phone2 { get; set; }

        [JsonProperty("ext2")]
        public string Ext2 { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("billingCompanyName")]
        public string BillingCompanyName { get; set; }

        [JsonProperty("billingFName")]
        public string BillingFName { get; set; }

        [JsonProperty("billingLName")]
        public string BillingLName { get; set; }

        [JsonProperty("billingCountryID")]
        public string BillingCountryID { get; set; }

        [JsonProperty("billingAddress")]
        public string BillingAddress { get; set; }

        [JsonProperty("billingCity")]
        public string BillingCity { get; set; }

        [JsonProperty("billingState")]
        public string BillingState { get; set; }

        [JsonProperty("billingZip")]
        public string BillingZip { get; set; }

        [JsonProperty("billingPhone")]
        public string BillingPhone { get; set; }

        [JsonProperty("billingEmail")]
        public string BillingEmail { get; set; }

        [JsonProperty("lat")]
        public string Lat { get; set; }

        [JsonProperty("lng")]
        public string Lng { get; set; }

        [JsonProperty("squareFeet")]
        public string SquareFeet { get; set; }

        [JsonProperty("addedByID")]
        public string AddedByID { get; set; }

        [JsonProperty("dateAdded")]
        public string DateAdded { get; set; }

        [JsonProperty("dateCancelled")]
        public string DateCancelled { get; set; }

        [JsonProperty("dateUpdated")]
        public string DateUpdated { get; set; }

        [JsonProperty("sourceID")]
        public string SourceID { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("aPay")]
        public string APay { get; set; }

        [JsonProperty("preferredTechID")]
        public string PreferredTechID { get; set; }

        [JsonProperty("paidInFull")]
        public string PaidInFull { get; set; }

        [JsonProperty("subscriptionIDs")]
        public string SubscriptionIDs { get; set; }

        [JsonProperty("balance")]
        public string Balance { get; set; }

        [JsonProperty("balanceAge")]
        public string BalanceAge { get; set; }

        [JsonProperty("responsibleBalance")]
        public string ResponsibleBalance { get; set; }

        [JsonProperty("responsibleBalanceAge")]
        public string ResponsibleBalanceAge { get; set; }

        [JsonProperty("customerLink")]
        public string CustomerLink { get; set; }

        [JsonProperty("masterAccount")]
        public string MasterAccount { get; set; }

        [JsonProperty("preferredBillingDate")]
        public string PreferredBillingDate { get; set; }

        [JsonProperty("paymentHoldDate")]
        public object PaymentHoldDate { get; set; }

        [JsonProperty("mostRecentCreditCardLastFour")]
        public object MostRecentCreditCardLastFour { get; set; }

        [JsonProperty("mostRecentCreditCardExpirationDate")]
        public object MostRecentCreditCardExpirationDate { get; set; }

        [JsonProperty("regionID")]
        public string RegionID { get; set; }

        [JsonProperty("mapCode")]
        public string MapCode { get; set; }

        [JsonProperty("mapPage")]
        public string MapPage { get; set; }

        [JsonProperty("specialScheduling")]
        public string SpecialScheduling { get; set; }

        [JsonProperty("taxRate")]
        public string TaxRate { get; set; }

        [JsonProperty("smsReminders")]
        public string SmsReminders { get; set; }

        [JsonProperty("phoneReminders")]
        public string PhoneReminders { get; set; }

        [JsonProperty("emailReminders")]
        public string EmailReminders { get; set; }

        [JsonProperty("customerSource")]
        public string CustomerSource { get; set; }

        [JsonProperty("customerSourceID")]
        public string CustomerSourceID { get; set; }

        [JsonProperty("maxMonthlyCharge")]
        public string MaxMonthlyCharge { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("useStructures")]
        public string UseStructures { get; set; }

        [JsonProperty("isMultiUnit")]
        public string IsMultiUnit { get; set; }

        [JsonProperty("appointmentIDs")]
        public string AppointmentIDs { get; set; }

        [JsonProperty("ticketIDs")]
        public string TicketIDs { get; set; }

        [JsonProperty("paymentIDs")]
        public string PaymentIDs { get; set; }

        [JsonProperty("unitIDs")]
        public List<object> UnitIDs { get; set; }
    }

    public class PestRouteCustomerDetailModel : PestRouteCommonFieldsModel
    {
        [JsonProperty("@params")]
        public Params Params { get; set; }

        [JsonProperty("tokenUsage")]
        public TokenUsage TokenUsage { get; set; }

        [JsonProperty("tokenLimits")]
        public TokenLimits TokenLimits { get; set; }

        [JsonProperty("requestAction")]
        public string RequestAction { get; set; }

        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("processingTime")]
        public string ProcessingTime { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("customers")]
        public List<Customer> Customers { get; set; }

        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }
    }

}
