using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Dapper;

namespace TinCanDotNet.Model
{
    public class Account
    {
        public string accountid { get; set; }
        public string homepage { get; set; }
        public string name { get; set; }

        public long Insert(SqlConnection cn)
        {
            string sql = @"insert into Account(homepage, name) values (@homepage, @name); select cast(scope_identity() as bigint)";
            return cn.Query<long>(sql, this).Single();
        }
    }
}
