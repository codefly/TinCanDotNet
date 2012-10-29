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
    public class AccountTest
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
        public void Account_BasicCreate()
        {

            string homepage = Guid.NewGuid().ToString();
            Account acc = new Account {  homepage = homepage, name="mike" };

            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                acc.Insert(cn);
                var foundac = cn.Query<Account>("select * from Account where homepage like @hp", new { hp = homepage }).Single();
                
                
                Assert.AreEqual(foundac.homepage, homepage);
                Assert.AreEqual(foundac.name, "mike");


            }

        }
        
    }
}
