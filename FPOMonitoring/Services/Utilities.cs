using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Services
{
    public class Utilities
    {
        public double ConvertTimeToDecimal(TimeSpan ts)
        {
            try
            {
                double d = ts.Days * 24;
                double h = ts.Hours;
                double m = ts.Minutes * (1.0 / 60);
                double s = ts.Seconds * (1.0 / 3600);
                return  d + h + m + s;
            }
            catch { return 0; }
        }

    }
}
