using Dapper;
using FPOMonitoring.Data.Class;
using FPOMonitoring.Services;
using FPOMonitoring.Tools.Helpers;
using FPOMonitoring.View.UserControl;
using FPOMonitoring.ViewModel;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FPOMonitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        private readonly MySQLService mySQLService = new MySQLService();
        private readonly LogService logService = new LogService();

        public MainWindow()
        {
            InitializeComponent();
            NonClientAreaContent = new NonClientAreaContent();
            this.InputBindings.AddRange(new List<KeyBinding> { 
                new KeyBinding { Key = Key.Q, Modifiers = ModifierKeys.Control, Command = new RelayCommand(() => SearchBar.Focus()) }, 
                new KeyBinding { Key = Key.F1, Modifiers = ModifierKeys.None, Command = ViewModelLocator.Instance.CommandVM.StartCommand },
                new KeyBinding { Key = Key.F2, Modifiers = ModifierKeys.None, Command = ViewModelLocator.Instance.CommandVM.StopCommand },
                new KeyBinding { Key = Key.F3, Modifiers = ModifierKeys.None, Command = ViewModelLocator.Instance.CommandVM.RefreshCommand },
                new KeyBinding { Key = Key.F4, Modifiers = ModifierKeys.None, Command = ViewModelLocator.Instance.CommandVM.ChangeCommand },
                new KeyBinding { Key = Key.E, Modifiers = ModifierKeys.Alt, Command = ViewModelLocator.Instance.CommandVM.ExitAppCommand }
            });

            TypeCB.ItemsSource = ViewModelLocator.Instance.Main.EAccounts;
            CompositionTarget.Rendering += (sender, args) =>
            {
                ProdDGV.InvalidateVisual();
                BPHDGV.InvalidateVisual();
                SUMMARYDGV.InvalidateVisual();
            };
            TabControl.SelectionChanged += TabControl_SelectionChanged;
        
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TabControl.SelectedIndex == 4)
            {
                ViewModelLocator.Instance.Main.IsProduction = true;
            } else
            {
                ViewModelLocator.Instance.Main.IsProduction = false;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = 1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void DrawerViewProcess_Closed(object sender, RoutedEventArgs e)
        {
            FilteredItems.ItemsSource = null;
            FilteredItemsCounts.Text = "0";
            ViewModelLocator.Instance.Main.IsOpen = false;
            lbl_nodata.Visibility = Visibility.Visible;
        }

        private void DrawerViewProcess_Opened(object sender, RoutedEventArgs e)
        {
            User user = (User)UsersDGV.SelectedItem;
            if (user != null)
            {
                var user_data = ViewModelLocator.Instance.Main.Documents.Where(item => item.biller_id == user.Id);
                FilteredItems.ItemsSource = user_data;
                if (user_data != null && user_data.Count() > 0)
                {
                    lbl_nodata.Visibility = Visibility.Collapsed;
                }
            }
            FilteredItemsCounts.Text = FilteredItems.Items.Count.ToString();
        }

        #region "Drawer Add and Update user"
        private bool is_edit = false;
        private string old_password = "";
        private User user1 = new User();
        private void DrawerAddUser_Opened(object sender, RoutedEventArgs e)
        {
           try
            {
                FullnameTB.Focus();
                FullnameTB.Text = string.Empty;
                UsernameTB.Text = string.Empty;
                PasswordPB.Password = "";
                TypeCB.SelectedIndex = -1;
                if (is_edit)
                {
                    if (UsersDGV.SelectedItem is User row && row != null)
                    {
                        old_password = GenerateViewPass(row.password_length);
                        FullnameTB.Text = row.Fullname;
                        UsernameTB.Text = row.Username;
                        PasswordPB.Password = old_password;
                        TypeCB.SelectedIndex = row.account;
                        user1 = row;
                    }
                    FullnameTB.SelectAll();
                }
            } catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(DrawerAddUser_Opened));
            }
        }
        private string GenerateViewPass(int length)
        {
            Random r = new Random();
            string s = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= length; i++)
            {
                int idx = r.Next(0, s.Length);
                sb.Append(s.Substring(idx, 1));
            }
            return sb.ToString();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DrawerAddUser.IsOpen = false;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DrawerAddUser.IsOpen = true;
        }
        private bool ValidateFields()
        {
            if (FullnameTB.Text == "") return false;
            if (UsernameTB.Text == "") return false;
            if (PasswordPB.Password == "") return false;
            if (TypeCB.SelectedItem is string tcb && tcb == "") return false;
            return true;
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Submit.IsChecked = true;
            if (ValidateFields())
            {
                if (is_edit)
                {
                    //Update user
                    using (IDbConnection conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                    {
                        var query = $"SELECT * FROM user_tbl WHERE username = '{UsernameTB.Text}' AND password = SHA1('{PasswordPB.Password}') AND archived = 0;";
                        var res = conn.Query<User>(query);
                        if (res != null && res.Count() > 0)
                        {
                            SweetAlert.Show("Illegal Username", "Please check the user you added because it has a duplicate username.", SweetAlertButton.OK, SweetAlertImage.ERROR);
                            return;
                        }
                        else
                        {
                            var query1 = "";
                            if (old_password != PasswordPB.Password)
                            {
                                query1 = $"UPDATE user_tbl SET fullname = '{FullnameTB.Text.Trim()}', " +
                                $"username = '{UsernameTB.Text.Trim()}', password = '{PasswordPB.Password.Trim()}', account = 2 WHERE id = {user1.Id};";
                            }
                            else
                            {
                                query1 = $"UPDATE user_tbl SET fullname = '{FullnameTB.Text.Trim()}', " +
                                $"username = '{UsernameTB.Text.Trim()}', account = 2 WHERE id = {user1.Id};";
                            }
                            conn.Query(query1);
                            SweetAlert.Show("Success", "User information has been updated.", SweetAlertButton.OK, SweetAlertImage.SUCCESS);
                            DrawerAddUser.IsOpen = false;
                        }
                    }
                }
                else
                {
                    //Inser user
                    using (IDbConnection conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                    {
                        var query = $"SELECT * FROM user_tbl WHERE username = '{UsernameTB.Text}' AND archived = 0;";
                        var res = conn.Query<User>(query);
                        if (res != null && res.Count() > 0)
                        {
                            SweetAlert.Show("Illegal Username", "Please check the user you added because it has a duplicate username.", SweetAlertButton.OK, SweetAlertImage.ERROR);
                            return;
                        }
                        else
                        {
                            var insert_query = $"INSERT INTO user_tbl (fullname, username, password, account) " +
                                $"VALUES ('{FullnameTB.Text.Trim()}', '{UsernameTB.Text.Trim()}', '{PasswordPB.Password}', 2)";
                            conn.Query(insert_query);
                            SweetAlert.Show("Success", "User has been added to the database.", SweetAlertButton.OK, SweetAlertImage.SUCCESS);
                            DrawerAddUser.IsOpen = false;
                        }
                        { }
                    }
                }
            }
            Submit.IsChecked = false;
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            is_edit = true;
            DrawerAddUser.IsOpen = true;
        }

        private void DrawerAddUser_Closed(object sender, RoutedEventArgs e)
        {
            is_edit = false;
        }
        #endregion

        private void StatusBTN_Click(object sender, RoutedEventArgs e)
        {
            StatusFiltered.IsOpen = true;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox checkBox = (System.Windows.Controls.CheckBox)sender;
            if (checkBox.Content.ToString().ToUpper() == "ALL")
            {
                if ((bool)checkBox.IsChecked)
                {
                    ViewModelLocator.Instance.Main.FilteredStatus.ToList().ForEach(item => item.IsChecked = true);
                } else
                {
                    ViewModelLocator.Instance.Main.FilteredStatus.ToList().ForEach(item => item.IsChecked = false);
                }
            }
            else
            {
                var item = ViewModelLocator.Instance.Main.FilteredStatus.FirstOrDefault(itm => itm.Name.ToUpper() == "ALL");
                if (item != null) item.IsChecked = false;

                //Check if all items is check
                bool allItemsCheckedExceptAll = ViewModelLocator.Instance.Main.FilteredStatus.Where(itm => itm.Name.ToUpper() != "ALL").All(itm => itm.IsChecked); 
                if (allItemsCheckedExceptAll)
                {
                    var temp = ViewModelLocator.Instance.Main.FilteredStatus.Where(itm => item.Name.ToUpper() == "ALL").FirstOrDefault();
                    temp.IsChecked = true;
                }
            }
          
           ViewModelLocator.Instance.Main.StatusText = string.Join(", ", ViewModelLocator.Instance.Main.FilteredStatus.Where(item => item.IsChecked && item.Name.ToUpper() != "ALL").Select(item => item.Name));

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HandyControl.Controls.ComboBox comboBox = (HandyControl.Controls.ComboBox)sender;
            if (comboBox.SelectedIndex > -1)
            {
            }
        }
        
    }
}
