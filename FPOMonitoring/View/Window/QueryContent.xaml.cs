using Dapper;
using FPOMonitoring.Data.Class;
using FPOMonitoring.Services;
using FPOMonitoring.Tools.Helpers;
using FPOMonitoring.ViewModel;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for QueryContent.xaml
    /// </summary>
    public partial class QueryContent
    {
        private readonly LogService logService = new LogService();
        private readonly MySQLService mySQLService = new MySQLService();
        private Document doc = new Document();
        public QueryContent(Document document)
        {
            InitializeComponent();
            doc = document;
            this.Loaded += QueryContent_Loaded;
            this.InputBindings.AddRange(new List<KeyBinding>
            {
                new KeyBinding { Key = Key.F1, Command = new RelayCommand(Send), Modifiers = ModifierKeys.None },
                new KeyBinding { Key = Key.F2, Command = new RelayCommand(new Action(() =>  this.Close())), Modifiers = ModifierKeys.None },
            });
        }

        private void QueryContent_Loaded(object sender, RoutedEventArgs e)
        {
            if (doc != null)
            {
                ViewModelLocator.Instance.QueryVM.FetchQueryData();
            }
            else
            {
                ViewModelLocator.Instance.QueryVM.ChatInfos = new ObservableCollection<ChatInfo>();
            } 
        }

        private async void Send()
        {
            try
            {
                List<string> query_message = new List<string>();
                if ((bool)c1.IsChecked) query_message.Add("Mismatch Consignee");
                if ((bool)c2.IsChecked) query_message.Add("Mismatch Shipping");
                if ((bool)c3.IsChecked) query_message.Add("Pro Billed");
                if ((bool)c4.IsChecked) query_message.Add("Pro Used");
                if ((bool)c5.IsChecked) query_message.Add("Disregard");
                if (!TextBoxOthers.Text.Trim().Equals("")) query_message.Add($"Message:s {RemoveExtraSpaces(TextBoxOthers.Text.Replace(",", " ").Trim())}");

                if (query_message.Count > 0)
                {
                    using (IDbConnection connection = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                    {
                        Query query = new Query();
                        query = ViewModelLocator.Instance.QueryVM.PendingQuery;
                        if (query != null)
                        {
                            query.state = 1;
                            query.answer = string.Join(",", query_message);
                            query.time_end = mySQLService.GetTimeFromDatabase(ViewModelLocator.Instance.LoginVM.ConnectionString);
                            query.answered_by = ViewModelLocator.Instance.LoginVM.CurrentUser.Id;

                            var id = connection.Update(query);
                            if (id > 0)
                            {
                                var ret = connection.Execute($"UPDATE document_tbl SET status = 'ANSWERED' WHERE id = {doc.Id};");
                                if (ret > 0)
                                {
                                    SweetAlert.Show("Success", "Data has been updated.", SweetAlertButton.OK, SweetAlertImage.SUCCESS);
                                }
                            }
                        }
                    }
                    this.Close();
                }
                else
                {
                    SweetAlert.Show("Invalid", "Query remarks cannot be blank", SweetAlertButton.OK, SweetAlertImage.ERROR);
                }

            } catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(Send));
                SweetAlert.Show("Error Message", ex.Message, SweetAlertButton.OK, SweetAlertImage.ERROR);
            }
        }

        private string RemoveExtraSpaces(string input)
        {
            // Use a regular expression to match multiple spaces and replace them with a single space.
            string pattern = @"\s+"; // \s represents whitespace characters (space, tab, newline, etc.)
            string replacement = " ";

            Regex regex = new Regex(pattern);
            string result = regex.Replace(input, replacement);
            return result;
        }

        private void SendQuery_Click(object sender, RoutedEventArgs e)
        {
           Send();
        }
    }
}
