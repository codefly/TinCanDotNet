using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TinCanDotNet.Models;
using Newtonsoft.Json.Serialization;
using System.Web.Helpers;

namespace TinCanDotNet.Controllers
{
    public class SearchObject
    {
        public string verb { get; set; }
        public string _object { get; set; }
        public string registration { get; set; }
        public bool? context { get; set; }
        public string actor { get; set; }
        public DateTime? since { get; set; }
        public DateTime? until { get; set; }
        public int limit { get; set; }
        public String authoritative { get; set; }
        public bool? sparse { get; set; }
        public string instructor { get; set; }

        public bool HasValues()
        {
            return verb != null |
                _object != null |
                registration != null |
                context != null |
                actor != null |
                since != null |
                until != null |
                limit != null |
                authoritative != null |
                sparse != null |
                instructor != null;
        }
    }

    public class StatementsController : ApiController
    {


        // GET api/statements
        public dynamic Get(string statementid = null, string limit = null)
        {
            var db = new DB();
            try
            {
                // hacky
                if (statementid == null)
                {
                    statementid = "_design/statements/_view/all";
                }
                if(limit != null)
                    statementid += "?limit=" + limit;
                var doc = db.GetDocument("http://localhost:5984", "tin_can", statementid);
                dynamic jo = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(doc);
                if(((IDictionary<String, Object>)jo).ContainsKey("_id")){
                    return CleanStatement(jo);
                } else if ( ((IDictionary<String, Object>)jo).ContainsKey("rows") ){
                    var list = new List<dynamic>();
                
                    foreach (var r in jo.rows)
                    {

                        var stm = r.value;
                   
                        list.Add(CleanStatement(stm));
                    }

                    return new { statements = list };
                } else {
                    return new {result = jo};
                }
              

            }catch(WebException ex){
                switch (ex.Status) {
                    case WebExceptionStatus.ProtocolError:
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    default:
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
            }
            
            
        }

        // GET api/statements/5
        public string Get(int id)
        {
            return "value";
        }



 

        // POST api/statements
        public string Post(SearchObject search, string statementid = null) //[FromBody]dynamic value, string statementid = null)
        {
           

            if (statementid != null)
            {
                throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
            }
            if (search != null)
            {
                return "foo";
            }
            dynamic value;
            try
            {
                value = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(Request.Content.ToString());
            }catch{
                throw new HttpResponseException(HttpStatusCode.BadRequest);
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
        public void Put([FromBody]dynamic value, string statementid = null)
        {

            SaveObject(value, statementid);
            /*

            if (value == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
            value.stored = DateTime.Now;
            if (value.timestamp == null) value.timestamp = value.stored;
            value._id = statementid;
            value.id = statementid;
            value.authority = "anonymous";
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            try
            {
                new DB().CreateDocument("http://localhost:5984", "tin_can", json);
            }
            catch (WebException ex)
            {
                Console.Write(ex.Response);
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }*/
                  

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
