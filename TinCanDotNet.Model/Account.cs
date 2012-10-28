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

        public int Insert(SqlConnection cn)
        {
            string sql = @"insert into Account(homepage, name) values (@homepage, @name); select scope_identity()";
            return cn.Query<int>(sql, this).Single();
        }
    }
}
