using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TinCanDotNet;
using TinCanDotNet.Controllers;
using TinCanDotNet.Models;
using Newtonsoft.Json;
using Raven.Client.Document;
using Raven.Client;


namespace TinCanDotNet.Tests.Controllers
{

   
    [TestClass]
    public class StatementsControllerTest
    {
        private IDocumentStore Store;

        [TestInitialize]
        public void Setup(){
           // RavenConnector.IsTest = true;
            Store = RavenConnector.GetStore();
        }

        private string statementString1 = @"
                   {
                        ""id"":""4afbd348-fb17-41a8-9c61-a046d7b00bdb"",
                        ""actor"": {
                            ""mbox"": ""mailto:codefly@gmail.com"",
                            ""name"": ""mike"",
                            ""objectType"": ""Agent""
                        },
                        ""verb"": {
                            ""id"": ""http://adlnet.gov/expapi/verbs/answered"",
                            ""display"": {
                                ""en-US"": ""answered""
                            }
                        },
                        ""object"": {
                            ""id"": ""http://www.example.com/tincan/activities/OTwSjPRg"",
                            ""objectType"": ""Activity"",
                            ""definition"": {
                                ""name"": {
                                    ""en-US"": ""Example Activity""
                                },
                                ""description"": {
                                    ""en-US"": ""Example activity definition""
                                }
                            }
                        }
                    }
                    ";
        [TestMethod]
        public void StatementsPut()
        {
            StatementsController controller = new StatementsController(new TinCanDotNet.Models.Repositories.MemoryRepository<Statement>());
            Statement statement =  JsonConvert.DeserializeObject<Statement>(statementString1);

            controller.Put(statement);

            IDocumentSession Session = Store.OpenSession();
            var result = Session.Query<Statement>()
                // .Where(x => x.Id == "4afbd348-fb17-41a8-9c61-a046d7b00bdb").ToList();
               .Where(x => true).ToList();

            var result2 = Session.Load<Statement>("4afbd348-fb17-41a8-9c61-a046d7b00bdb");
           //Assert.AreEqual("mailto:codefly@gmail.com", result.Actor.Mbox);
            
            


            


        }
    }
}
