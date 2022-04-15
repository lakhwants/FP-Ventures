using System;
using System.Collections.Generic;

namespace DeleteUnAnsweredDispositionsFromZohoCRM.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Details
    {
        public string id { get; set; }
    }

    public class Datum
    {
        public string code { get; set; }
        public Details details { get; set; }
        public string message { get; set; }
        public string status { get; set; }
    }

    public class ZohoResponseModel
    {
        public List<Datum> data { get; set; }
    }
}
