using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    public class Counts 
    {
        public int ProductionId { get; set; }
        public int Total { get; set; }
        public int Billed { get; set; }
        public int Reject { get; set; }
        public int Query { get; set; }
        public int Answered { get; set; }
    }
}
