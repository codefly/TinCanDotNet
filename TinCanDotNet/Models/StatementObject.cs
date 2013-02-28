using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace TinCanDotNet.Models
{
    public class StatementObject
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "objectType", NullValueHandling = NullValueHandling.Ignore)]
        public string ObjectType { get; set; }

        [JsonProperty(PropertyName = "definition", NullValueHandling = NullValueHandling.Ignore)]
        public StatementObjectDefinition Definition { get; set; }

    }
    
    public class StatementObjectDefinition
    {
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Name { get; set; }

        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string> Description { get; set; }
    }



}