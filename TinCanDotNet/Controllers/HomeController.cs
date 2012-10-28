using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DapperExtensions;
using Dapper;
using System.Data.SqlClient;
using TinCanDotNet.Model;
using Newtonsoft.Json;

namespace TinCanDotNet.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Statement statement = new Statement();
            statement.id = new Guid().ToString();
            statement.actor = new Actor { mbox = "codefly@gmail.com", name = "mike" };
            string foo = Newtonsoft.Json.JsonConvert.SerializeObject(statement);


            using (SqlConnection cn = new SqlConnection("Data Source=localhost;Initial Catalog=TinCanDotNet;Integrated Security=True"))
            {
                cn.Open();
                statement.Insert(cn);

            }
            return View();
        }
    }
}
