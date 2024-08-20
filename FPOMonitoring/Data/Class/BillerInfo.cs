using FPOMonitoring.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    [Table("user_tbl")]
    public class BillerInfo
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Account { get; set; }
        public int Is_Connected { get; set; }
        [IgnoreInsert]
        public DateTime Last_Login { get; set; }
    }
}
