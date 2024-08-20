using HandyControl.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FPOMonitoring.Data.Class
{

    public struct ChatInfo
    {
        public object Header { get; set; }
        public object Message { get; set; }
        public object DateTime { get; set; }
        public ChatRoleType ChatType { get; set; }
        public HorizontalAlignment HeaderAlignment { get; set; }
    }
}
