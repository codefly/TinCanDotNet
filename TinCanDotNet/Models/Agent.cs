using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace TinCanDotNet.Models
{
    public class Agent
    {
        [JsonProperty(PropertyName = "objectType")]
        public string  ObjectType { get; set; }

         [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "mbox")]
        public string  Mbox { get; set; }

        [JsonProperty(PropertyName = "mbox_sha1sum")]
        public string Mbox_Sha1Sum { get; set; }

        [JsonProperty(PropertyName = "openid")]
        public string OpenID { get; set; }

        [JsonProperty(PropertyName = "account")]
        public Account Account { get; set; }
    }
}
