using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    public class DatabaseSettings
    {
        public List<string> Server { get; set; } = new List<string>();
        public int Port { get; set; } = 3306;
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string ProdHrs { get; set; }
    }
}
