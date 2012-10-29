using System;
using System.Collections.Generic;
using System.Linq;
using TinCanDotNet.Model;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dapper;
using DapperExtensions;
using System.Transactions;

namespace TinCanDotNet.Tests.Model
{
    [TestClass]
    public class StatementTests
    {

        TransactionScope scope;

        [TestInitialize]
        public void Initialize()
        {
            scope = new TransactionScope();
        }

        [TestCleanup]
        public void Cleanup()
        {
            scope.Dispose();
        }

        [TestMethod]
        public void BasicCreate()
        {
            Statement statement = new Statement();
            string id = (statement.id = new Guid().ToString());
            statement.actor = new Actor { mbox = "codefly@gmail.com", name = "mike" };
            statement.verb = new Verb {id = "http://test.com/testverb", display = "foo"};
            Console.WriteLine("here 1");


            using (SqlConnection cn = new SqlConnection("Data Source=localhost;Initial Catalog=TinCanDotNet;Integrated Security=True"))
            {
                cn.Open();
                statement.Insert(cn);
                var foundst = cn.Query<Statement>("select * from Statement where ID like @ID", new { ID = id }).Single();
                Console.WriteLine("hello");
                Assert.AreEqual(foundst.id, id, "not equal: " + foundst.id + " : " + id);


            }

        }
    }
}
