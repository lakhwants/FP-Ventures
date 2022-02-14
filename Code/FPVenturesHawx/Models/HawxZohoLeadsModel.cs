using Newtonsoft.Json;
using System.Collections.Generic;

namespace FPVenturesHawx.Models
{

	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
	//public class Owner
	//{
	//    public string name { get; set; }
	//    public string id { get; set; }
	//    public string email { get; set; }
	//}

	//public class Approval
	//{
	//    public bool @delegate { get; set; }
	//    public bool approve { get; set; }
	//    public bool reject { get; set; }
	//    public bool resubmit { get; set; }
	//}

	//public class CreatedBy
	//{
	//    public string name { get; set; }
	//    public string id { get; set; }
	//    public string email { get; set; }
	//}

	//public class ReviewProcess
	//{
	//    public bool approve { get; set; }
	//    public bool reject { get; set; }
	//    public bool resubmit { get; set; }
	//}

	//public class ModifiedBy
	//{
	//    public string name { get; set; }
	//    public string id { get; set; }
	//    public string email { get; set; }
	//}

	//public class ConvertedDetail
	//{
	//}

	public class Record
    {
       // public Owner Owner { get; set; }
        public string Company { get; set; }
       // public string ReferralCode { get; set; }
        public string Email { get; set; }

        //[JsonProperty("$currency_symbol")]
        //public string CurrencySymbol { get; set; }
        //public string Referred_by { get; set; }
        //public DateTime Last_Activity_Time { get; set; }
        //public object Industry { get; set; }

        [JsonProperty("$state")]
        public string State { get; set; }
        //public object Unsubscribed_Mode { get; set; }

        //[JsonProperty("$converted")]
        //public bool Converted { get; set; }

        //[JsonProperty("$process_flow")]
        //public bool ProcessFlow { get; set; }
        //public object Test { get; set; }
        //public object Call_Response_Timestamp { get; set; }
        //public object Street { get; set; }
        //public object Zip_Code { get; set; }
        //public string id { get; set; }

        //[JsonProperty("$approved")]
        //public bool Approved { get; set; }

        //[JsonProperty("$approval")]
        //public Approval Approval { get; set; }
        //public DateTime Created_Time { get; set; }

        //[JsonProperty("$editable")]
        //public bool Editable { get; set; }
        //public object City { get; set; }
        //public object No_of_Employees { get; set; }
        //public object Country { get; set; }
        //public CreatedBy Created_By { get; set; }
        //public object Annual_Revenue { get; set; }
        //public object Secondary_Email { get; set; }
        //public object Description { get; set; }
        //public object Rating { get; set; }

        //[JsonProperty("$review_process")]
        //public ReviewProcess ReviewProcess { get; set; }
        //public object Website { get; set; }
        //public object Twitter { get; set; }
        //public object Salutation { get; set; }
        public string First_Name { get; set; }
        //public string Full_Name { get; set; }
        //public object Lead_Status { get; set; }
        //public object Record_Image { get; set; }
        //public ModifiedBy Modified_By { get; set; }

        //[JsonProperty("$review")]
        //public object Review { get; set; }
        //public object Skype_ID { get; set; }
        public string Phone { get; set; }
        //public bool Email_Opt_Out { get; set; }
        //public bool Update_CRT { get; set; }
        //public object Designation { get; set; }
        //public DateTime Modified_Time { get; set; }
        //public object Name_match_with_Melissa { get; set; }

        //[JsonProperty("$converted_detail")]
        //public ConvertedDetail ConvertedDetail { get; set; }
        //public object Unsubscribed_Time { get; set; }
        //public string Mobile { get; set; }
        //public bool No_Call_logs { get; set; }

        //[JsonProperty("$orchestration")]
        //public bool Orchestration { get; set; }
        //public string Advocate_name { get; set; }
        public string Last_Name { get; set; }

        //[JsonProperty("$in_merge")]
        //public bool InMerge { get; set; }
        //public string Lead_Source { get; set; }
        //public List<object> Tag { get; set; }
        //public bool Call_logs_found { get; set; }
        //public object Fax { get; set; }

        //[JsonProperty("$approval_state")]
        //public string ApprovalState { get; set; }
        //public object Call_Response_Time { get; set; }
    }

    //public class Info
    //{
    //    public int per_page { get; set; }
    //    public int count { get; set; }
    //    public int page { get; set; }
    //    public bool more_records { get; set; }
    //}

    public class HawxZohoLeadsModel
    {
        public List<Record> data { get; set; }
       // public Info info { get; set; }
    }

}
