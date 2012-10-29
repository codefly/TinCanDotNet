using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Configuration;

namespace TinCanDotNet.Model
{
    public class DbHelper
    {
        public static SqlConnection GetConnection(){
            string connstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            return new SqlConnection(connstring);
        }
    }
}
