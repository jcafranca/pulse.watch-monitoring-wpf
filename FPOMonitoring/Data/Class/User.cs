using FPOMonitoring.Tools.Helpers;
using GalaSoft.MvvmLight;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace FPOMonitoring.Data.Class
{
    [Table("user_tbl")]
    public class User : INotifyPropertyChanged
    { 
        public int Id { get; set; }
        private string fullname;
        public string Fullname
        {
            get => fullname;
            set
            {
                fullname = value;
                OnPropertyChanged(nameof(Fullname));
            }
        }
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private int _is_connected;
        public int is_connected 
        {
            get => _is_connected;
            set
            {
                _is_connected = value;
                OnPropertyChanged(nameof(is_connected));
            }
        }
        private DateTime _last_login;
        [IgnoreInsert]
        public DateTime last_login
        {
            get => _last_login;
            set
            {
                _last_login = value;
                OnPropertyChanged(nameof(last_login));
            }
        }
        private string _password;
        public string password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(password));
            }
        }
        private int _password_length;
        public int password_length
        {
            get => _password_length;
            set
            {
                _password_length = value;
                OnPropertyChanged(nameof(password_length));
            }
        }
        private int _account;
        public int account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged(nameof(account));
            }
        }
        private string _job;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string Job
        {
            get => _job;
            set
            {
                _job = value;
                OnPropertyChanged(nameof(Job));
            }
        }
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string BillerCode { get; set; }
        private string _pronumber;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string Pronumber
        {
            get => _pronumber;
            set
            {
                _pronumber = value;
                OnPropertyChanged(nameof(Pronumber));
            }
        }
        private string _billtype;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string BillType
        {
            get => _billtype;
            set
            {
                _billtype = value;
                OnPropertyChanged(nameof(BillType));
            }
        }
        private string _pages;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string Pages
        {
            get => _pages;
            set
            {
                _pages = value;
                OnPropertyChanged(nameof(Pages));
            }
        }
        private string _status;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        private DateTime _startTime;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime  StartTime
        {
            get => _startTime;
            set
            {
                _startTime = value;
                OnPropertyChanged(nameof(StartTime));
            }
        }
        private DateTime _endTime;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChanged(nameof(EndTime));
            }
        }
        private string _elapsedtime;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string ElapsedTime
        {
            get => _elapsedtime;
            set
            {
                _elapsedtime = value;
                OnPropertyChanged(nameof(ElapsedTime));
            }
        }
        private string _remarks;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string Remarks
        {
            get => _remarks;
            set
            {
                _remarks = value;
                OnPropertyChanged(nameof(Remarks));
            }
        }
        private Brush _foreground;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public Brush Foreground
        {
            get => _foreground;
            set
            {
                _foreground = value;
                OnPropertyChanged(nameof(Foreground));
            }
        }

        private double _billed_count;
        private double _reject_count;
        private double _bph;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public double BilledCount
        {
            get => _billed_count;
            set
            {
                _billed_count = value;
                OnPropertyChanged(nameof(BilledCount));
            }
        }
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public double RejectCount
        {
            get => _reject_count;
            set
            {
                _reject_count = value;
                OnPropertyChanged(nameof(RejectCount));
            }
        }
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public double BPH
        {
            get => _bph;
            set
            {
                _bph = value;
                OnPropertyChanged(nameof(BPH));
            }
        }
        private double _prod_time_decimal;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public double ProdTimeDecimal
        {
            get => _prod_time_decimal;
            set
            {
                _prod_time_decimal = value;
                OnPropertyChanged(nameof(ProdTimeDecimal));
            }
        }
        private string _prod_time;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string ProdTime
        {
            get => _prod_time;
            set
            {
                _prod_time = value;
                OnPropertyChanged(nameof(ProdTime));
            }
        }

        private string _meal_break;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string MealBreak
        {
            get => _meal_break;
            set
            {
                _meal_break = value;
                OnPropertyChanged(nameof(MealBreak));
            }
        }
        private string _nature_break;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string NatureBreak
        {
            get => _nature_break;
            set
            {
                _nature_break = value;
                OnPropertyChanged(nameof(NatureBreak));
            }
        }
        private string _idle_break;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public string IdleBreak
        {
            get => _idle_break;
            set
            {
                _idle_break = value;
                OnPropertyChanged(nameof(IdleBreak));
            }
        }
        private int _querId;
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public int QueryId
        {
            get => _querId;
            set
            {
                _querId = value;
                OnPropertyChanged(nameof(QueryId));
            }
        }
        #region "ICommands"
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public ICommand ViewProcessCommand { get; set; }
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public ICommand ForceLogoutCommand { get; set; }
        [IgnoreSelect]
        [IgnoreInsert]
        [IgnoreUpdate]
        public ICommand DeleteCommand { get; set; }

        private  ObservableCollection<HourDetails> _hourly;
        [IgnoreInsert]
        [IgnoreUpdate]
        [IgnoreSelect]
        public ObservableCollection<HourDetails> Hourly
        {
            get => _hourly;
            set
            {
                _hourly = value;
                OnPropertyChanged(nameof(Hourly));
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Reset()
        {
            BillType = string.Empty;
            Pronumber = string.Empty;
            Pages = string.Empty;
            Remarks = string.Empty;
            ElapsedTime = "00:00:00";
            StartTime = new DateTime();
            EndTime = new DateTime();
            ProdTime = "";
            ProdTimeDecimal = 0;
            BPH = 0;
            MealBreak = "";
            NatureBreak = "";
            IdleBreak = "";
            Job = "";

        }

        public User()
        {
            Hourly = new ObservableCollection<HourDetails> {
                new HourDetails { index = 0, name = "00:00-12:59"},
                new HourDetails { index = 1, name = "01:00-01:59"},
                new HourDetails { index = 2, name = "02:00-02:59"},
                new HourDetails { index = 3, name = "03:00-03:59"},
                new HourDetails { index = 4, name = "04:00-04:59"},
                new HourDetails { index = 5, name = "05:00-05:59"},
                new HourDetails { index = 6, name = "06:00-06:59"},
                new HourDetails { index = 7, name = "07:00-07:59"},
                new HourDetails { index = 8, name = "08:00-08:59"},
                new HourDetails { index = 9, name = "09:00-09:59"},
                new HourDetails { index = 10, name = "10:00-10:59"},
                new HourDetails { index = 11, name = "11:00-11:59"},
                new HourDetails { index = 12, name = "12:00-12:59"},
                new HourDetails { index = 13, name = "13:00-13:59"},
                new HourDetails { index = 14, name = "14:00-14:59"},
                new HourDetails { index = 15, name = "15:00-15:59"},
                new HourDetails { index = 16, name = "16:00-16:59"},
                new HourDetails { index = 17, name = "17:00-17:59"},
                new HourDetails { index = 18, name = "18:00-18:59"},
                new HourDetails { index = 19, name = "19:00-19:59"},
                new HourDetails { index = 20, name = "20:00-20:59"},
                new HourDetails { index = 21, name = "21:00-21:59"},
                new HourDetails { index = 22, name = "22:00-22:59"},
                new HourDetails { index = 23, name = "23:00-23:59"},
                };
        }

        public void HourlyReset()
        {
            if (Hourly != null)
            {
                Hourly.ToList().ToArray().ForEach(hr =>
                {
                    hr.billed = 0;
                });
            }
        }
    }
}
