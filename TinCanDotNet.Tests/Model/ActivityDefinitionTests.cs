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
    public class ActivityDefinitionTests
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
        public void ActivityDefinition_BasicCreate()
        {

            string name = Guid.NewGuid().ToString();
            ActivityDefinition ad = new ActivityDefinition { name = name, choices = "choices", correctResponsesPattern = "correct",
                                                                description = "desc", extensions = "ext", interactionType = "int",
                                                                 scale = "scale", source = "source", steps = "steps", target= "target",
                                                                    type = "type" };

            using (SqlConnection cn = DbHelper.GetConnection())
            {
                cn.Open();
                ad.Insert(cn);
                var foundac = cn.Query<ActivityDefinition>("select * from ActivityDefinition where name like @name", new { name = name }).Single();


                Assert.AreEqual(foundac.name, name);
                Assert.AreEqual(foundac.choices, "choices");


            }

        }
    }
}
