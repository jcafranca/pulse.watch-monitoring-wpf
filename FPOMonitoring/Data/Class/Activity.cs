using FPOMonitoring.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    [Table("activity_tbl")]
    public class Activity
    {
        public int id { get; set; }
        public int biller_id { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set;}
        public string job { get; set; }
        public int prod_id { get; set; }
        public string message1 { get; set; }
        public string message2 { get; set; }
        public string message3 { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public string BillerUsername { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public string BillerName { get; set; }
    }
}
