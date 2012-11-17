using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;

namespace TinCanDotNet.Model
{
    public class ActivityDefinition
    {
        public string name { get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string interactionType { get; set; }
        public string correctResponsesPattern { get; set; }
        public string choices { get; set; }
        public string scale { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public string steps { get; set; }
        public string extensions { get; set; }

        public long Insert(SqlConnection cn)
        {

            string sql = @"
                insert into ActivityDefinition(name, description, type, interactionType, correctResponsesPattern, 
                                                choices, scale, source, target, steps, extensions )
                   values(@name, @description, @type, @interactionType, @correctResponsesPattern, 
                                                @choices, @scale, @source, @target, @steps, @extensions );
                select cast(Scope_identity() as bigint);";
            var result = cn.Query<long>(sql, this);
            return result.Single();


        }

    }
}
