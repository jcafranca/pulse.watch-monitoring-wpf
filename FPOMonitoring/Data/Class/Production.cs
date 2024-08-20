using FPOMonitoring.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    [Table("production_tbl")]
    public class Production : INotifyPropertyChanged
    {
        public int id { get; set; }
        public string date { get; set; }

        private bool _isCheck;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public bool IsCheck
        {
            get => _isCheck;
            set
            {
                _isCheck = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
