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
        public void Statement_BasicCreate()
        {
            Statement statement = createTestStatement();
            Guid id = statement.id;

            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                statement.Insert(cn);
                var foundst = cn.Query<Statement>("select * from Statement where ID like @ID", new { ID = id }).Single();
                Assert.AreEqual(foundst.id, id, "not equal: " + foundst.id + " : " + id);


            }

        }

        [TestMethod]
        public void Statement_FindByID()
        {
            Statement statement = createTestStatement();
            Guid id = statement.id;
            string mbox = statement.actor.mbox;

            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                statement.Insert(cn);
                var foundst = Statement.FindByID(id.ToString(), cn);
                Assert.AreEqual(foundst.id, id, "not equal: " + foundst.id + " : " + id);
                Assert.AreEqual(foundst.actor.mbox, mbox);


            }
        }

        private Statement createTestStatement()
        {
            Statement statement = new Statement();
            Guid id = (statement.id = Guid.NewGuid());
            string mbox = (Guid.NewGuid().ToString()) + "@b.c";
            string homepage = (Guid.NewGuid().ToString());
            statement.actor = new Agent { mbox = "codefly@gmail.com", name = "mike" };
            statement.verb = new Verb { id = "http://test.com/testverb", display = "foo" };
            statement.tc_object = new TCObject { agent = new Agent { mbox = mbox, name = "name", account = new Account { homepage = homepage, name = "lkjlkj" } } };
            return statement;

        }
    }
}
