using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TinCanDotNet.Models
{
    public class Agent
    {
        [JsonProperty(PropertyName = "objectType", NullValueHandling = NullValueHandling.Ignore)]
        public string  ObjectType { get; set; }

         [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "mbox", NullValueHandling = NullValueHandling.Ignore)]
        public string  Mbox { get; set; }

        [JsonProperty(PropertyName = "mbox_sha1sum", NullValueHandling = NullValueHandling.Ignore)]
        public string Mbox_Sha1Sum { get; set; }

        [JsonProperty(PropertyName = "openid", NullValueHandling = NullValueHandling.Ignore)]
        public string OpenID { get; set; }

        [JsonProperty(PropertyName = "account", NullValueHandling = NullValueHandling.Ignore)]
        public Account Account { get; set; }
    }
}
