using FPOMonitoring.View.Window;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace FPOMonitoring.ViewModel
{
    public class CommandViewModel : ViewModelBase, INotifyPropertyChanged
    {
        //Declarations
        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }
        public ICommand RefreshCommand { get; }
        public ICommand ChangeCommand { get; }
        public ICommand RestartAppCommand { get; }
        public ICommand ExitAppCommand { get; }
        public ICommand SettingCommand { get; }
        public ICommand ReportCommand { get; }
        public ICommand BackupCommand { get; }
        public ICommand QueryCommand { get; }

        private string status_message;
        public string StatusMessage
        {
            get => status_message;
            set
            {
                Set(ref status_message, value);
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        private string _icon;
        public string Icon
        {
            get => _icon;
            set
            {
                Set(ref _icon, value);
                OnPropertyChanged(nameof(Icon));
            }
        }
        public CommandViewModel()
        {
            StartCommand = new RelayCommand(Start);
            StopCommand = new RelayCommand(Stop);
            RefreshCommand = new RelayCommand(Refresh);
            ChangeCommand = new RelayCommand(Change);
            RestartAppCommand = new RelayCommand(RestartApp);
            ExitAppCommand = new RelayCommand(ExitApp);
            SettingCommand = new RelayCommand(Setting);
            ReportCommand = new RelayCommand(Report);
            BackupCommand = new RelayCommand(Backup);
            QueryCommand = new RelayCommand(Query);
            StatusMessage = "Not Running";
            Icon = "\uf05e";
        }
        private void Account()
        {
        }
        private void Start() 
        {
            ViewModelLocator.Instance.Main.IsRunning = true;
            StatusMessage = "Running";
            Icon = "\uf70c";
        }
        private void Stop() 
        {
            ViewModelLocator.Instance.Main.IsRunning = false;
            StatusMessage = "Not Running";
            Icon = "\uf05e";
        }
        private void Refresh() 
        {
            ViewModelLocator.Instance.Main.Refresh();
        }
        private void Change()
        {
            InputBox input = new InputBox("Change Target");
            input.Owner = Application.Current.MainWindow;
            input.ShowDialog();
        }
        private void RestartApp() 
        {
            // Restart the application
            System.Diagnostics.Process.Start(Process.GetCurrentProcess().MainModule.FileName);
            Application.Current.Shutdown();
        }
        private void ExitApp() 
        {
            var alert_info = new SweetAlert
            {
                Caption = "Ready to Leave?",
                Message = "Do you want to exit this application ?",
                MsgButton = SweetAlertButton.YesNo,
                MsgImage = SweetAlertImage.QUESTION,
                OkText = "Yes, exit it!",
                CancelText = "Cancel",
                ShowInTaskbar = false
            };
            if (alert_info.ShowDialog() == SweetAlertResult.OK)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        private void Setting() 
        {
            SettingContent setting = new SettingContent();
            setting.Owner = Application.Current.MainWindow;
            setting.ShowDialog();
        }
        private void Report() 
        {
            ReportContent report = new ReportContent();
            report.Owner = Application.Current.MainWindow;
            ViewModelLocator.Instance.ReportVM.Initialize();
            report.ShowDialog();
        }
        private void Backup() 
        {
            BackupContent backup = new BackupContent();
            backup.Owner = Application.Current.MainWindow;
            backup.ShowDialog();
        }
        private void Query() { }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
