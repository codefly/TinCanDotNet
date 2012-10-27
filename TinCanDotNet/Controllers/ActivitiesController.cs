using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TinCanDotNet.Controllers
{
    public class ActivitiesController : ApiController
    {
        // GET api/activities
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/activities/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/activities
        public void Post([FromBody]string value)
        {
        }

        // PUT api/activities/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/activities/5
        public void Delete(int id)
        {
        }
    }
}
