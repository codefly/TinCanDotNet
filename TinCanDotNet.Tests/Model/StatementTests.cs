using System;
using TinCanDotNet.Model;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using DapperExtensions;

namespace TinCanDotNet.Tests.Model
{
    [TestClass]
    public class StatementTests
    {
        [TestMethod]
        public void BasicCreate()
        {
            Statement statement = new Statement();
            statement.id = new Guid().ToString();
            statement.actor = new Actor { mbox = "codefly@gmail.com", name = "mike" };


            using (SqlConnection cn = new SqlConnection("Data Source=localhost;Initial Catalog=TinCanDotNet;Integrated Security=True"))
            {
                int id = cn.Insert(statement);

            }

        }
    }
}
