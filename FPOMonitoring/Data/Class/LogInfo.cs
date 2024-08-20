using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    public class LogInfo
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string ErrWhere { get; set; }
        public DateTime DateTime { get; set; }
    }
}
