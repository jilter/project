using Chloe.MySql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouGou8.Service
{
    class MySqlHelper
    {
        public static string connectionStr = ConfigurationManager.ConnectionStrings["YouGou8ConnStr"].ConnectionString;
        public static MySqlContext context = new MySqlContext(new MySqlConnectionFactory(connectionStr));
    }
}
