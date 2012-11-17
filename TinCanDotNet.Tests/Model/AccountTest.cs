﻿using System;
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

            string id = Guid.NewGuid().ToString();
            Verb v = new Verb {  id = id, display = "thedisplay" };

            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                v.Insert(cn);
                var foundverb = cn.Query<Verb>("select * from Verb where id like @id", new { id = id }).Single();
                
                
                Assert.AreEqual(foundverb.id, id);
                Assert.AreEqual(foundverb.display, "thedisplay");


            }

        }
        
    }
}
