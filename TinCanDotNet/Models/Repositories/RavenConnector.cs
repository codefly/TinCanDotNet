using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Embedded;
using Raven.Database.Server;
using Raven.Client.Indexes;
using System.Reflection;
using Raven.Imports.Newtonsoft.Json.Serialization;

namespace TinCanDotNet.Models
{
    public class LowercaseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }

    public class RavenConnector
    {
        

        private static IDocumentStore _store;
        private static bool _isTest = false;

        public static bool IsTest
        {
            get { return _isTest; }
            set { _isTest = value; }

        }

        public static IDocumentStore GetStore()
        {
            if(_store == null)
            {
                if (IsTest)
                {

                    var EStore = new EmbeddableDocumentStore
                    {

                        DataDirectory = "Data",
                        RunInMemory = true,
                        UseEmbeddedHttpServer = true
                    };

                    EStore.SetStudioConfigToAllowSingleDb();
                    EStore.Configuration.AnonymousUserAccessMode = AnonymousUserAccessMode.All;
                    _store = EStore;

                }
                else
                {

                    var Store = new DocumentStore { ConnectionStringName = "RavenDB" };
                    _store = Store;
                }
                _store.Initialize();
               // _store.Conventions.FindIdentityProperty = (prop => prop.Name == "id");
                IndexCreation.CreateIndexes(Assembly.GetCallingAssembly(), _store);
            }
            
            return _store;
            
        }

    }
}