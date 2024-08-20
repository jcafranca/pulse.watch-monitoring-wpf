using FPOMonitoring.ViewModel;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
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
    /// Interaction logic for LoginContent.xaml
    /// </summary>
    public partial class LoginContent
    {
        public LoginContent()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBoxUsername.SelectAll();
            TextBoxPassword.Password = string.Empty;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TargetTB.Focus();
                TargetTB.SelectAll();
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                    e.Handled = true;
                }
            }
        }

        private void TextBoxPassword_Loaded(object sender, RoutedEventArgs e)
        {
            HandyControl.Controls.PasswordBox pbox = (HandyControl.Controls.PasswordBox)sender;
            pbox.ActualPasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
        }
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Handle the password change event here
            ViewModelLocator.Instance.LoginVM.Password = TextBoxPassword.SecurePassword;
            // Perform operations with the entered password
        }

        private void TargetTB_KeyDown(object sender, KeyEventArgs e)
        {
            var tb = (TextBox)sender;
           if (e.Key == Key.Enter)
            {
                ButtonLogin_Click(this, null);
            }
        }
        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            bool is_okay = false;
            if (TargetTB.Text == null || TargetTB.Text == "") is_okay = true;
            else
            {
                if (TargetTB.Text.Trim().All(char.IsDigit)) is_okay = true;
                else
                {
                    SweetAlert.Show("Invalid Characters", "Target counts must be numeric only, please check.", SweetAlertButton.OK, SweetAlertImage.ERROR);
                    TargetTB.SelectAll();
                    return;
                }
            }

            if (is_okay)
            {
                if (ViewModelLocator.Instance.LoginVM.LoginCommand != null && ViewModelLocator.Instance.LoginVM.LoginCommand.CanExecute(null))
                {
                    ViewModelLocator.Instance.LoginVM.LoginCommand.Execute(null);
                }
            }
        }
    }
}
