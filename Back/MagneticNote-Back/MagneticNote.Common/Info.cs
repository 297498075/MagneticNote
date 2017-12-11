using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Info
    {
        public static String Address = ConfigurationManager.AppSettings["Address"];
        public static String ResourceAddress = ConfigurationManager.AppSettings["ResourceAddress"];
        public static String Host = ConfigurationManager.AppSettings["EmailHost"];
        public static String EmailUserName = ConfigurationManager.AppSettings["EmailAccount"];
        public static String EmailPassword = ConfigurationManager.AppSettings["EmailPassword"];
        public static int PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["PageSize"]);
    }
}
