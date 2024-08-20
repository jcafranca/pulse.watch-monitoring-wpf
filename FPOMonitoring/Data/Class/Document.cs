using FPOMonitoring.Tools.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FPOMonitoring.Data.Class
{
    [Table("document_tbl")]
    public class Document : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Pronumber { get; set; }
        public string BillType { get; set; }
        public string Pages { get; set; }
        public string Comments { get; set; }
        public string Remarks { get; set; }
        public string Terminal { get; set; }
        private string status;
        public string Status
        {
            get => status;
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public int biller_id { get; set; }
        public int prod_id { get; set; }
        public int query_id { get; set; }
        public DateTime start_time { get; set; }
        private DateTime _end_time;
        public DateTime end_time
        {
            get => _end_time;
            set
            {
                _end_time = value;
                OnPropertyChanged(nameof(end_time));
            }
        }
        public string job { get; set; }
        public string Ex1 { get; set; }
        public string Ex2 { get; set; }
        public string Ex3 { get; set; }
        public string Ex4 { get; set; }
        public string Ex5 { get; set; }
        [IgnoreUpdate]
        public DateTime created_timestamp { get; set; }
        public int archived { get; set; }

        #region "Additional Fields"
        private string username;
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public string BillerUsername
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(BillerUsername));
            }
        }
        private string fullname;
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public string BillerName
        {
            get => fullname;
            set
            {
                fullname = value;
                OnPropertyChanged(nameof(BillerName));
            }
        }
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public TimeSpan ElapsedTimeSTE { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public TimeSpan ActualTimeSTS { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public ICommand DeleteCommand { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public ICommand AnswerCommand { get; set; }
        [IgnoreInsert]
        [IgnoreUpdate]
        public IList<Query> Queries { get; set; } = new List<Query>();

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
