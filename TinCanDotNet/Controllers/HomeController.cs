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
           
            return View();
        }
    }
}
