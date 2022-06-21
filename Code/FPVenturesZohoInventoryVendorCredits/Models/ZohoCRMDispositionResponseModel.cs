using System;
using System.Collections.Generic;

namespace FPVenturesZohoInventoryVendorCredits.Models
{

    public class DispositionModel
        {
            public string Disposition { get; set; }
            public string id { get; set; }
            public string ANI { get; set; }
            public DateTime Timestamp { get; set; }
        }

        public class ZohoCRMDispositionResponseModel
        {
            public List<DispositionModel> data { get; set; }
            public Info info { get; set; }
        }

}
