using FPOMonitoring.Data.Class;
using FPOMonitoring.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Tools.Extension;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FPOMonitoring.ViewModel
{
    public class SettingViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly LogService logService = new LogService();
        private readonly MySQLService mySQLService = new MySQLService();
        #region "Commands"
        public ICommand TestCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand AddItemCommand { get; private set; }
        #endregion
        #region "Database Settings"
        private ObservableCollection<string> _hostname = new ObservableCollection<string>();
        public ObservableCollection<string> Hostnames
        {
            get => _hostname;
            set
            {
                Set(ref _hostname, value);
                RaisePropertyChanged(nameof(Hostnames));
            }
        }
        private int _port;
        public int Port
        {
            get => _port;
            set
            {
                Set(ref _port, value);
                RaisePropertyChanged(nameof(Port));
            }
        }
        private string _database_name;
        public string DatabaseName
        {
            get => _database_name;
            set
            {
                Set(ref _database_name, value);
                RaisePropertyChanged(nameof(DatabaseName));
            }
        }
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                Set(ref _username, value);
                RaisePropertyChanged(nameof(Username));
            }
        }
        private SecureString _password;
        public SecureString Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                RaisePropertyChanged(nameof(Password));
            }
        }
        private string _start;
        public string Start
        {
            get => _start;
            set
            {
                Set(ref _start, value);
                RaisePropertyChanged(nameof(Start));
            }
        }
        private string _end;
        public string End
        {
            get => _end;
            set
            {
                Set(ref _end, value);
                RaisePropertyChanged(nameof(End));
            }
        }
        private string _prod_hrs;
        public string ProdHRS
        {
            get => _prod_hrs;
            set
            {
                Set(ref _prod_hrs, value);
                RaisePropertyChanged(nameof(ProdHRS));
            }
        }
        private string _tagName;
        public string TagName
        {
            get => _tagName;
            set => Set(ref _tagName, value);
        }
        private bool loading;
        public bool Loading
        {
            get => loading;
            set
            {
                Set(ref loading, value);
                RaisePropertyChanged(nameof(Loading));
            }
        }
        private bool loading2;
        public bool Loading2
        {
            get => loading2;
            set
            {
                Set(ref loading2, value);
                RaisePropertyChanged(nameof(Loading2));
            }
        }
        #endregion

        public SettingViewModel() 
        {
            Hostnames = new ObservableCollection<string>();
            TestCommand = new RelayCommand(TestConnection);
            SaveCommand = new RelayCommand(Save);
            AddItemCommand = new RelayCommand(AddItem);
            Loading = false;
            Loading2 = false;
        }
        void TestConnection() 
        {
            if (Hostnames.Count > 0)
            {
                Loading = true;
                Task.Run(() =>
                {
                    bool is_success = false;
                    var s = new DatabaseSettings
                    {
                        Server = Hostnames.ToList(),
                        Port = Port,
                        Database = DatabaseName,
                        Username = Username,
                        Password = ConvertSecureStringToString(Password),
                        Start = Start,
                        End = End,
                        ProdHrs = ProdHRS
                    };
                    var connection = mySQLService.CheckConnection(s);
                    using (IDbConnection conn = new MySqlConnection(connection))
                    {
                        conn.Open();
                        is_success = true;
                    }
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (is_success)
                        {
                            SweetAlert.Show("Success", $"Your connection to Database server {((string)connection).Split(';')[0].Split('=')[1]} is success.", SweetAlertButton.OK, SweetAlertImage.SUCCESS);
                        }
                        else
                        {
                            SweetAlert.Show("Error", "Your connection to Database server is denied.", SweetAlertButton.OK, SweetAlertImage.ERROR);
                        }
                        Loading = false;
                    });
                });
            }
        }
        void Save() 
        {
            try
            {
                Loading2 = true;
                var s = new DatabaseSettings
                {
                    Server = Hostnames.ToList(),
                    Port = Port,
                    Database = DatabaseName,
                    Username = Username,
                    Password = ConvertSecureStringToString(Password),
                    Start = Start,
                    End = End,
                    ProdHrs = ProdHRS
                };

                if (s.Start == null || s.Start == "" ||  s.End == null || s.End == "")
                {
                    SweetAlert.Show("Saving Failed", "Start and End cannot be blank, please check.", SweetAlertButton.OK, SweetAlertImage.ERROR);
                    return;
                }
                if (DateTime.Parse(s.Start).TimeOfDay > DateTime.Parse(s.End).TimeOfDay) 
                {
                    SweetAlert.Show("Saving Failed", "Start cannot be higher than End, please check.", SweetAlertButton.OK, SweetAlertImage.ERROR);
                    return;
                }

                logService.Serialize(logService.SettingPath, s);
                ViewModelLocator.Instance.LoginVM.Settings = s;
                ViewModelLocator.Instance.Main.Start = s.Start;
                ViewModelLocator.Instance.Main.End = s.End;
                if (s.Start != null && s.End != null)
                {
                    TimeSpan start = TimeSpan.Parse(s.Start);
                    TimeSpan end = TimeSpan.Parse(s.End);

                    TimeSpan prod = end.Subtract(start);
                    ViewModelLocator.Instance.Main.ProductionHrs = prod.ToString(@"hh\:mm\:ss");
                }

                SweetAlert.Show("Success", "Your settings is up to date.", SweetAlertButton.OK, SweetAlertImage.SUCCESS);
            }
            catch (Exception ex)
            {
                SweetAlert.Show("Error", ex.Message, SweetAlertButton.OK, SweetAlertImage.ERROR);
            } finally
            {
                Loading2 = false;
            }
        }



        void AddItem() 
        {
            if (string.IsNullOrEmpty(TagName))
            {
                SweetAlert.Show("Field cannot be blank!", "Server", SweetAlertSharp.Enums.SweetAlertButton.OK, SweetAlertSharp.Enums.SweetAlertImage.ERROR);
                return;
            }
            Hostnames.Insert(0, TagName);
            TagName = string.Empty;
        }
        public SecureString ConvertToSecureString(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            var securePassword = new SecureString();
            foreach (char c in password) securePassword.AppendChar(c);
            securePassword.MakeReadOnly();
            return securePassword;
        }
        public string ConvertSecureStringToString(SecureString secureString)
        {
            if (secureString == null) return "";
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}
