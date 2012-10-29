using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;

namespace TinCanDotNet.Model
{
    public class Verb
    {
        public string id;
        public string display;
    

        public long Insert(SqlConnection cn)
        {

            string sql = @"
                insert into Verb(ID, display)
                   values(@id, @display);
                select cast(Scope_identity() as bigint);";
            var result = cn.Query<long>(sql, new { id=this.id, display=this.display});
            return result.Single();
            

        }
    }
}
