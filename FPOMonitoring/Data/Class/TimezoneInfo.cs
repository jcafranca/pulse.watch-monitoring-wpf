using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    public class TimezoneInfo
    {
        public string Id { get; set; }
        public TimeSpan BaseUtcOffset { get; set; }
        public string DaylightName { get; set; }
        public string DisplayName { get; set; }
        public string StandardName { get; set; }
        public bool SupportsDaylightSavingTime { get; set; }
    }
}
