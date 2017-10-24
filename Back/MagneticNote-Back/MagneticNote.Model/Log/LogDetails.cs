using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagneticNote.Model.Log
{
    public class LogDetails
    {
        public DateTime LogDate { get; set; }
        public String LogThread { get; set; }
        public String LogLevel { get; set; }
        public String LogLogger { get; set; }
        public String LogMessage { get; set; }
        public String LogActionClick { get; set; }
        public String UserName { get; set; }
        public String UserIP { get; set; }
    }
}
