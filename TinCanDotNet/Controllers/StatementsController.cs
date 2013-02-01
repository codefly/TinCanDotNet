using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using System.Web.Helpers;
using TinCanDotNet.Models;

namespace TinCanDotNet.Controllers
{
   

    public class StatementsController : ApiController
    {


        // GET api/statements
        public dynamic Get(string statementid = null, string limit = null)
        {
            return new { foo = "bar" };
            
            
        }

        // GET api/statements/5
        public string Get(int id)
        {
            return "value";
        }



 

        // POST api/statements
        public string Post([FromBody]dynamic value, string statementid = null)
        {
           

            if (statementid != null)
            {
                throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
            }
            
            var list = new List<string>();
            foreach (dynamic val in value)
            {
                var ret = SaveObject(val, null);
                dynamic njo = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(ret);
                list.Add(njo.id); 
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(list);

        }

        // PUT api/statements
        public void Put([FromBody]Statement value, string statementid = null)
        {

            SaveObject(value, statementid);

        }

        // DELETE api/statements/5
        public void Delete(int id)
        {
        }

        private string SaveObject(dynamic value, string statementid)
        {
            string returnval = null;
            if (value == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            return "xx";
            /*
            value.stored = DateTime.Now;
            if (value.timestamp == null) value.timestamp = value.stored;
            if (statementid != null)
            {
                value._id = statementid;
                value.id = statementid;
            }
            value.type = "statement";
            value.authority = "anonymous";
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            try
            {
               var str =  new DB().CreateDocument("http://localhost:5984", "tin_can", json);
               return str;
            }
            catch (WebException ex)
            {
                Console.Write(ex.Response);
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
             * */
        }

        private dynamic CleanStatement(dynamic stm)
        {
            stm.id = stm._id;
            ((IDictionary<String, Object>)stm).Remove("_id");
            ((IDictionary<String, Object>)stm).Remove("_rev");
            ((IDictionary<String, Object>)stm).Remove("type");
            return stm;
        }
    }
}
