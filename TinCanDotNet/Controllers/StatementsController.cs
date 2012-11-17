using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TinCanDotNet.Models;
using Newtonsoft.Json.Serialization;
using System.Web.Helpers;
using TinCanDotNet.Model;
using System.Data.SqlClient;
using Dapper;

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
        public int? limit { get; set; }
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
            var list = new List<Statement>();
            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                
                if (statementid != null)
                {
                    return Statement.FindByID(statementid, cn);
                }
            }
            return list;
            
            
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
        public void Put([FromBody]Statement value, string statementid = null)
        {

            SaveObject(value, statementid);           

        }

        // DELETE api/statements/5
        public void Delete(int id)
        {
        }

        private string SaveObject(Statement value, string statementid)
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
                value.id = new Guid(statementid);
            }
            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                try
                {
                    value.Insert(cn);
                }
                catch (WebException ex)
                {
                    Console.Write(ex.Response);
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }
                return "";

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
