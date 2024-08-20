using FPOMonitoring.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    [Table("query_tbl")]
    public class Query
    {
        public int id { get; set; }
        public int doc_id { get;set; }
        public string answer { get; set; }
        public DateTime time_start { get; set; }    
        public DateTime? time_end { get; set;}
        public string query { get; set; }
        public int processed_by { get; set; }
        public int answered_by { get; set; }
        public int state { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        public  DateTime created_datetime { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        public int archived { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        public string ProcessedByName { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        public string AnsweredByName { get; set; }
    }
}
