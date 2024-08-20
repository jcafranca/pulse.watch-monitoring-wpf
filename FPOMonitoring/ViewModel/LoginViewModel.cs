using FPOMonitoring.Services;
using GalaSoft.MvvmLight;
using SweetAlertSharp.Enums;
using SweetAlertSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using FPOMonitoring.Data.Class;
using System.Windows;
using FPOMonitoring.Tools.Helpers;
using GalaSoft.MvvmLight.Command;
using Dapper;
using FPOMonitoring.View.Window;
using HandyControl.Tools.Extension;

namespace FPOMonitoring.ViewModel
{
    public class LoginViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly LogService logService = new LogService();
        private readonly MySQLService mySQLService = new MySQLService();
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand LoginCommand { get; private set; }
        public ICommand ResetPasswordCommand { get; private set; }

        private BillerInfo _currentUser;
        public BillerInfo CurrentUser
        {
            get => _currentUser;
        }
        private SecureString _password;
        public SecureString Password
        {
            get => _password;
            set
            {
                Set(ref _password, value);
                OnPropertyChanged(nameof(Password));
            }
        }
        private string _username;
        public string Username
        {
            get => _username;
            set { Set(ref _username, value); OnPropertyChanged(nameof(Username)); }
        }
        private bool loading;
        public bool Loading
        {
            get => loading;
            set
            {
                Set(ref loading, value);
                OnPropertyChanged(nameof(Loading));
            }
        }
        private double _target;
        public double Target
        {
            get => _target;
            set
            {
                Set(ref _target, value);
                OnPropertyChanged(nameof(Target));
            }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get => _connectionString;
        }
        private DatabaseSettings settings;
        public DatabaseSettings Settings
        {   
            get => settings;
            set
            {
                Set(ref settings, value);
                OnPropertyChanged(nameof(Settings));
            }
        }

        public LoginViewModel() 
        {
            LoginCommand = new RelayCommand(Login);
            ResetPasswordCommand = new RelayCommand(Reset);
            Settings = logService.DeserializeSetting(logService.SettingPath).CastTo<DatabaseSettings>();
            if (Settings != null)
                _connectionString = mySQLService.CheckConnection(Settings);
            else _connectionString = "";
        }
        private void Login()
        {
            if (_connectionString != null && _connectionString != "")
            {
                Task.Factory.StartNew(() =>
            {
                try
                {
                    Loading = true;
                    Thread.Sleep(100);
                    if (ValidateCredentials())
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ViewModelLocator.Instance.NonClientAreaVM.Username = Username;
                            ViewModelLocator.Instance.Main.Target = Target;
                            MainWindow window = new MainWindow();
                            window.Show();
                            Application.Current.MainWindow.Close(); // Close the login window
                            Application.Current.MainWindow = window;
                        });
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            SweetAlert.Show("Illegal Credentials", "Invalid username and password, please check!", msgImage: SweetAlertSharp.Enums.SweetAlertImage.ERROR);
                        });
                    }
                }
                catch (Exception ex)
                {
                    logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(Login));
                }
                finally
                {
                    Loading = false;
                }
            }).ConfigureAwait(true);
            } else
            {
                SweetAlert.Show("Files not found", "Some files are missing, please contact the programmer.", SweetAlertButton.OK, SweetAlertImage.ERROR);
                return;
            }
        }
        public bool ValidateCredentials()
        {
            
            if (Username == "") return false;
            if (Password == null || Password.Length == 0) return false;

            var pass = "";
            if (Password is SecureString p) pass = ConvertSecureStringToString(p);

            string sql = $"SELECT * FROM user_tbl WHERE username = '{Username}' AND password = SHA1('{pass}') AND account = 0 AND archived = 0;";
            using (var connection = mySQLService.GetDbContext(ConnectionString))
            {
                var keyer = connection.Query<BillerInfo>(sql).ToList().FirstOrDefault();
                if (keyer != null)
                {
                    keyer.Password = pass;
                    _currentUser = keyer;
                    return true;
                }
            }

            return false;
        }

        private string ConvertSecureStringToString(SecureString secureString)
        {
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

        private void Reset()
        {
            var alert = new SweetAlert
            {
                Caption = "Contact your supervisor to reset your password.",
                Message = "Thank you!",
                MsgImage = SweetAlertImage.INFORMATION,
                MsgButton = SweetAlertButton.OK,
                OkText = "Okay, got it."
            };
            alert.ShowDialog();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
