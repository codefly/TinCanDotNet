using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using Newtonsoft.Json.Serialization;
using System.Web.Helpers;
using TinCanDotNet.Models;
using System.Web.Http.Filters;
using TinCanDotNet.Models.Repositories;


namespace TinCanDotNet.Controllers
{


    public class StatementsController : ApiController  
    {
        //TODO: move this code into a base class or filter

        private  IDataRepository<Statement> repository;

        public StatementsController(IDataRepository<Statement> repository)
        {
            this.repository = repository;
        }

        // GET api/statements
        public dynamic Get(string statementid = null, string limit = null)
        {
            if (statementid != null)
                return repository.Get(statementid);
            else
                return "TODO";
           
                
        }

        // GET api/statements/5
        public Statement Get(string id)
        {
            return repository.Get(id);
        }



 

        // POST api/statements
        public string Post([FromBody]dynamic value, string statementid = null)
        {
           

            if (statementid != null)
            {
                throw new HttpResponseException(HttpStatusCode.MethodNotAllowed);
            }
            
            var list = new List<string>();
            foreach (Statement val in value)
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
            
            value.Stored = DateTime.Now;
            //if (value.TimeStamp == null) value.TimeStamp = value.Stored;
            if (statementid != null)
            {
                value.Id = statementid;
            }
            else if (value.Id == null)
            {
                value.Id = Guid.NewGuid().ToString();
            }
            //value.Type = "statement";
            value.Authority = new Dictionary<string, string>();
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
            //var obj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
            try
            {

                repository.Save(value.Id, value);
               return "ok";
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
