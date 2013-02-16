using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;


namespace TinCanDotNet.Models
{
    public class Account
    {

        [JsonProperty(PropertyName = "homepage")]
        public string Homepage { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}
