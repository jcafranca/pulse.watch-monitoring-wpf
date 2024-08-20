using FPOMonitoring.Services;
using FPOMonitoring.Tools.Helpers;
using FPOMonitoring.View.Window;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SweetAlertSharp.Enums;
using SweetAlertSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Reflection;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using FPOMonitoring.Data.Class;
using HandyControl.Tools.Extension;

namespace FPOMonitoring.ViewModel
{
    public class NonClientAreaViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly LogService logService = new LogService();
        public ICommand LogoutCommand { get; private set; }
        public string TimezonePath
        {
            get => System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "AppFiles", "Timezone.json");
        }
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                Set(ref _username, value);
                OnPropertyChanged(nameof(Username));
            }
        }
        private string _appname;
        public string AppName
        {
            get => _appname;
            set
            {
                Set(ref _appname, value);
                OnPropertyChanged(nameof(AppName));
            }
        }
        private bool isTimezonePopupOpen;
        public bool IsTimezonePopupOpen
        {
            get => isTimezonePopupOpen;
            set
            {
                Set(ref isTimezonePopupOpen, value);
                OnPropertyChanged(nameof(IsTimezonePopupOpen));
            }
        }
        public ReadOnlyCollection<TimeZoneInfo> Timezone
        {
            get
            {
                ReadOnlyCollection<TimeZoneInfo> tz;
                tz = TimeZoneInfo.GetSystemTimeZones();
                return tz;
            }
        }
        private ObservableCollection<TimeZoneInfo> timeZoneInfos;
        public ObservableCollection<TimeZoneInfo> TimeZones
        {
            get => timeZoneInfos;
            set
            {
                Set(ref timeZoneInfos, value);
                OnPropertyChanged(nameof(TimeZones));
            }
        }
        private TimeZoneInfo _selectedTimezone;
        public TimeZoneInfo SelectedTimezone
        {
            get => _selectedTimezone;
            set
            {
                Set(ref _selectedTimezone, value);
                OnPropertyChanged(nameof(SelectedTimezone));
            }
        }
        public NonClientAreaViewModel() 
        { 
            LogoutCommand = new RelayCommand(SetLogout);
            SetApplicationName();

            TimeZones = new ObservableCollection<TimeZoneInfo>();
            Timezone.ToList().ForEach(item => TimeZones.Add(item));
            TimezoneInfo temp_tz = logService.Deserialize(TimezonePath).CastTo<TimezoneInfo>();
            if (temp_tz == null) {
                SelectedTimezone = Timezone[GetSelectedTimeZoneIndex("Singapore Standard Time")];
                temp_tz = new TimezoneInfo
                {
                    Id = SelectedTimezone.Id,
                    BaseUtcOffset = SelectedTimezone.BaseUtcOffset,
                    DaylightName = SelectedTimezone.DaylightName,
                    DisplayName = SelectedTimezone.DisplayName,
                    StandardName = SelectedTimezone.StandardName,
                    SupportsDaylightSavingTime = SelectedTimezone.SupportsDaylightSavingTime
                };
                logService.Serialize(TimezonePath, temp_tz);
            }  else
            {
                SelectedTimezone = Timezone[GetSelectedTimeZoneIndex(temp_tz.Id)];
            }
        }
        void SetApplicationName() 
        {
            var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            Assembly assembly = Assembly.GetExecutingAssembly();
            AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(assembly, typeof(AssemblyTitleAttribute));

            if (titleAttribute != null)
            {
                string title = titleAttribute.Title;
                AppName = $"{title} v{version.Major}.{version.Minor}";
            }
        }
        private void SetLogout()
        {
            var alert_info = new SweetAlert
            {
                Caption = "Relay to Leave?",
                Message = "You won't be able to revert this!",
                MsgButton = SweetAlertButton.YesNo,
                MsgImage = SweetAlertImage.QUESTION,
                OkText = "Yes, logout it!",
                CancelText = "Cancel",
                ShowInTaskbar = false
            };
            if (alert_info.ShowDialog() == SweetAlertResult.OK)
            {
                try
                {
                    ViewModelLocator.Instance.LoginVM.Username = Username;
                    ViewModelLocator.Instance.LoginVM.Password = new System.Security.SecureString();

                    var login = new LoginContent();
                    login.Show();

                    Messenger.Default.Send(new WindowRequestMessage(window =>
                    {
                        window.Close();
                        Application.Current.MainWindow = login;
                    }));

                }
                catch (Exception ex)
                {
                    logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(SetLogout));
                }
            }
        }
        public int GetSelectedTimeZoneIndex(string timeZoneId)
        {
            TimeZoneInfo selectedTimeZone = Timezone.FirstOrDefault(tz => tz.Id.Equals(timeZoneId));
            if (selectedTimeZone != null) return Timezone.IndexOf(selectedTimeZone);
            return -1;
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
