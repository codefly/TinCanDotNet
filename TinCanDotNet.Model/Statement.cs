using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using Newtonsoft.Json;

namespace TinCanDotNet.Model
{
    public class Statement
    {
        public string id { get; set; }
        public Actor actor { get; set; }
        public Verb verb { get; set; }

        [JsonProperty(PropertyName="object")]
        public TCObject tc_object { get; set; }
        public Result result { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime stored { get; set; }
        public TCObject authority { get; set; }
        public bool voided { get; set; }

        public int Insert(SqlConnection cn)
        {   
  
            int actorId = this.actor.Insert(cn);
            string sql = @"
                insert into Statement (ID, AgentID) values (@id, @actorid); select Scope_identity()";
            return cn.Query<int>(sql, new { id = this.id, actorid = actorId }).Single();

          
        }

    }
}
