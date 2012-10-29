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
    public class ActorTest
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
        public void Actor_BasicCreate()
        {

            string homepage = Guid.NewGuid().ToString();
            Account acc = new Account {  homepage = homepage, name="mike" };

            Actor actor = new Actor { objectType = "Actor", openid = "abc", mbox = "codefly@gmail.com", name = "mike", account = acc };


            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                var actorid = actor.Insert(cn);
                var foundac = cn.Query<Account>("select * from Account where homepage like @hp", new { hp = homepage }).Single();
                var foundactor = cn.Query<Actor>("select * from Agent where AgentID like @actid", new { actid = actorid }).Single();
                
                
                Assert.AreEqual(foundac.homepage, homepage);
                Assert.AreEqual(foundac.name, "mike");
                Assert.AreEqual(foundactor.mbox, "codefly@gmail.com");


            }

        }
        
    }
}
