using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PestRoutesTest.Models
{
    

    public class Customer
    {
        public string customerID { get; set; }
        public string billToAccountID { get; set; }
        public string officeID { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string companyName { get; set; }
        public string commercialAccount { get; set; }
        public string status { get; set; }
        public string statusText { get; set; }
        public string email { get; set; }
        public string phone1 { get; set; }
        public string ext1 { get; set; }
        public string phone2 { get; set; }
        public string ext2 { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string billingCompanyName { get; set; }
        public string billingFName { get; set; }
        public string billingLName { get; set; }
        public string billingCountryID { get; set; }
        public string billingAddress { get; set; }
        public string billingCity { get; set; }
        public string billingState { get; set; }
        public string billingZip { get; set; }
        public string billingPhone { get; set; }
        public string billingEmail { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string squareFeet { get; set; }
        public string addedByID { get; set; }
        public string dateAdded { get; set; }
        public string dateCancelled { get; set; }
        public string dateUpdated { get; set; }
        public string sourceID { get; set; }
        public string source { get; set; }
        public string aPay { get; set; }
        public string preferredTechID { get; set; }
        public string paidInFull { get; set; }
        public string subscriptionIDs { get; set; }
        public string balance { get; set; }
        public string balanceAge { get; set; }
        public string responsibleBalance { get; set; }
        public string responsibleBalanceAge { get; set; }
        public string customerLink { get; set; }
        public string masterAccount { get; set; }
        public string preferredBillingDate { get; set; }
        public object paymentHoldDate { get; set; }
        public object mostRecentCreditCardLastFour { get; set; }
        public object mostRecentCreditCardExpirationDate { get; set; }
        public string regionID { get; set; }
        public string mapCode { get; set; }
        public string mapPage { get; set; }
        public string specialScheduling { get; set; }
        public string taxRate { get; set; }
        public string smsReminders { get; set; }
        public string phoneReminders { get; set; }
        public string emailReminders { get; set; }
        public string customerSource { get; set; }
        public string customerSourceID { get; set; }
        public string maxMonthlyCharge { get; set; }
        public string county { get; set; }
        public string useStructures { get; set; }
        public string isMultiUnit { get; set; }
        public string appointmentIDs { get; set; }
        public string ticketIDs { get; set; }
        public string paymentIDs { get; set; }
        public List<object> unitIDs { get; set; }
    }

    public class PestRouteCustomerDetailModel : PestRouteCommonFieldsModel
    {
        public Params @params { get; set; }
        public TokenUsage tokenUsage { get; set; }
        public TokenLimits tokenLimits { get; set; }
        public string requestAction { get; set; }
        public string endpoint { get; set; }
        public bool success { get; set; }
        public string processingTime { get; set; }
        public int count { get; set; }
        public List<Customer> customers { get; set; }
        public string propertyName { get; set; }
    }




}
