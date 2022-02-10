using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HeyFlowIntegration.Models
{

	// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
	public class Value
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Field
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("variable")]
        public string Variable { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("values")]
        public List<Value> Values { get; set; }
    }

    public class HeyFlowReponseModel
    {
        [JsonProperty("fields")]
        public List<Field> Fields { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("flowId")]
        public string FlowId { get; set; }

        [JsonProperty("simplified")]
        public bool Simplified { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }
    }


}
