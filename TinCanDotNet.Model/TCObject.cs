using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;

namespace TinCanDotNet.Model
{
    public class TCObject
    {
        public string objectType { get; set; }
        public Agent agent { get; set; }
        public ActivityDefinition definition { get; set; }
        public Statement statement { get; set; }

        public long? Insert(SqlConnection cn)
        {
           
            long? agentid = null;
            if (agent != null)
                agentid = agent.Insert(cn);

            long? definitionid = null;
            if (definition != null)
                definitionid = definition.Insert(cn);

            long? statementid = null;
            if (statement != null)
                statementid = statement.Insert(cn);

            string sql = @"
                insert into TCObject(objectType, AgentID, ActivityDefinitionID, StatementID)
                   values(@objecttype, @agentid, @activitydefinitionid, @statementid);
                select cast(Scope_identity() as bigint);";
            var result = cn.Query<long?>(sql, new
            {
                objectType = this.objectType ?? "Activity" ,
                agentid = agentid,
                activitydefinitionid = definitionid,
                statementid = statementid
                
            });
            return result.Single();


        }


    }
}
