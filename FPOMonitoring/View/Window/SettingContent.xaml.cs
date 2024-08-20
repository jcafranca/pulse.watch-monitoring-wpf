using FPOMonitoring.ViewModel;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FPOMonitoring.View.Window
{
    /// <summary>
    /// Interaction logic for SettingContent.xaml
    /// </summary>
    public partial class SettingContent 
    {
        public SettingContent()
        {
            InitializeComponent();

            if (ViewModelLocator.Instance.LoginVM.Settings != null)
            {
                ViewModelLocator.Instance.SettingVM.Hostnames = new System.Collections.ObjectModel.ObservableCollection<string>();
                ViewModelLocator.Instance.SettingVM.Hostnames.AddRange(ViewModelLocator.Instance.LoginVM.Settings.Server);
                ViewModelLocator.Instance.SettingVM.Port = ViewModelLocator.Instance.LoginVM.Settings.Port;
                ViewModelLocator.Instance.SettingVM.DatabaseName = ViewModelLocator.Instance.LoginVM.Settings.Database;
                ViewModelLocator.Instance.SettingVM.Username = ViewModelLocator.Instance.LoginVM.Settings.Username;
                ViewModelLocator.Instance.SettingVM.Password = ViewModelLocator.Instance.SettingVM.ConvertToSecureString(ViewModelLocator.Instance.LoginVM.Settings.Password);
                ViewModelLocator.Instance.SettingVM.Start = ViewModelLocator.Instance.LoginVM.Settings.Start;
                ViewModelLocator.Instance.SettingVM.End = ViewModelLocator.Instance.LoginVM.Settings.End;
                ViewModelLocator.Instance.SettingVM.ProdHRS = ViewModelLocator.Instance.LoginVM.Settings.ProdHrs;
            }
        }

        private void PasswordBox_Loaded(object sender, RoutedEventArgs e)
        {
            HandyControl.Controls.PasswordBox pbox = (HandyControl.Controls.PasswordBox)sender;
            pbox.Password = ViewModelLocator.Instance.SettingVM.ConvertSecureStringToString(ViewModelLocator.Instance.SettingVM.Password) ;
            pbox.ActualPasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Handle the password change event here
            ViewModelLocator.Instance.SettingVM.Password = TextBoxPassword.SecurePassword;
            // Perform operations with the entered password
        }

        private void TextBoxPassword_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        private void ServerText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ViewModelLocator.Instance.SettingVM.TagName = HostnameTB.Text;
                ViewModelLocator.Instance.SettingVM.AddItemCommand.Execute(true);
            }
        }

        private void TextBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
