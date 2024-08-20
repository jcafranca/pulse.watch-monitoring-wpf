using FPOMonitoring.Services;
using FPOMonitoring.View.Window;
using FPOMonitoring.ViewModel;
using HandyControl.Tools.Extension;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FPOMonitoring
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly LogService logService = new LogService();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new LoginContent();
            window.Show();
        }
    }
}
