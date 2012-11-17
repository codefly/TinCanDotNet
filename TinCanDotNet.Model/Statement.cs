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
        public Guid id { get; set; }
        public Agent actor { get; set; }
        public Verb verb { get; set; }

        [JsonProperty(PropertyName="object")]
        public TCObject tc_object { get; set; }
        public Result result { get; set; }
        public DateTime? timestamp { get; set; }
        public DateTime? stored { get; set; }
        public Agent authority { get; set; }
        public bool voided { get; set; }

        public long Insert(SqlConnection cn)
        {

            long? actorId = this.actor == null ? null : this.actor.Insert(cn);
            long? verbId = this.verb == null ? null : this.verb.Insert(cn);
            long? tcobjID = this.tc_object == null ? null : this.tc_object.Insert(cn);
            long? authID = this.authority == null ? null : this.authority.Insert(cn);
            string sql = @"
                insert into Statement (ID, AgentID, VerbID, TCObjectID, AuthorityAgentID) values (@id, @actorid, @verbid, @tcobjID, @authorityAgentID); select cast(Scope_identity() as bigint)";
            return cn.Query<long>(sql, new { id = this.id, actorid = actorId, verbid = verbId, tcobjid = tcobjID, authorityAgentID = authID }).Single();

        }

        public static Statement FindByID(string id, SqlConnection cn)
        {
            string sql = @"
            SELECT *
            from Statement s
	            inner join Agent a on s.AgentID = a.AgentID
	            left outer join Account ac on a.AccountID = ac.AccountID
	            inner join Verb v on s.VerbID = v.VerbID
	            inner join TCObject o on s.TCObjectID = o.TCObjectID
            where s.ID = @id";
            var st = cn.Query<Statement, Agent, Account, Verb, TCObject, Statement>(sql,
                                    (statement, agent, account, verb, obj) =>
                                    {
                                        statement.actor = agent;
                                        statement.actor.account = account;
                                        statement.verb = verb;
                                        statement.tc_object = obj;
                                        return statement;
                                    },
                                    new { id = id },splitOn: "AgentID,AccountID,VerbID,TCObjectID").Single();
            return st;
        }



    }
}
