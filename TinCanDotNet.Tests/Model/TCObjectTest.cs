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
    public class TCObjectTest
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
        public void TCObject_BasicCreate()
        {

            string ot = Guid.NewGuid().ToString();
            string mbox = Guid.NewGuid().ToString();
            Agent agent = new Agent { mbox = mbox };

            TCObject obj = new TCObject { objectType = ot, agent = agent };

            //TODO: more cases
            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                obj.Insert(cn);
                var found = cn.Query<TCObject>("select * from TCObject where objectType like @ot", new { ot = ot }).Single();

                var foundagent = cn.Query<Agent>("select * from Agent where mbox = @mbox", new { mbox = mbox }).Single();


                
                Assert.AreEqual(foundagent.mbox, mbox);


            }

        }
    }
        
}
