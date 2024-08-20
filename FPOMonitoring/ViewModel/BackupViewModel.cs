using Dapper;
using FPOMonitoring.Data.Class;
using FPOMonitoring.Services;
using FPOMonitoring.Tools.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FPOMonitoring.ViewModel
{
    public class BackupViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<Production> _productions;
        private readonly MySQLService mySQLService = new MySQLService();
        private class AllColumns
        {
            public int id { get; set; }
            public string pronumber { get; set; }
            public string terminal { get; set; }
            public string billtype { get; set; }
            public string pages { get; set; }
            public string comments { get; set; }
            public string remarks { get; set; }
            public string status { get; set; }
            public int biller_id { get; set; }
            public int query_id { get; set; }
            public int prod_id { get; set; }
            public DateTime start_time { get; set; }
            public DateTime end_time { get; set; }
            public string job { get; set; }
            public string ex1 { get; set; }
            public string ex2 { get; set; }
            public string ex3 { get; set; }
            public string ex4 { get; set; }
            public string ex5 { get; set; }
            public DateTime created_timestamp { get; set; }
            public DateTime modefied_timestamp { get; set; }
            public string archived { get; set; }
        }
        public IEnumerable<Production> Productions
        {
            get => _productions;
        }
        private ObservableCollection<Document> _datas;
        public ObservableCollection<Document> Datas
        {
            get => _datas;
            set
            {
                Set(ref _datas, value);
                OnPropertyChanged(nameof(Datas));
            }
        }

        private double _progress;
        public double Progress
        {
            get => _progress;
            set
            {
                Set(ref _progress, value);
                OnPropertyChanged(nameof(Progress));
            }
        }
        private string _status_message;
        public string StatusMessage
        {
            get => _status_message;
            set
            {
                Set(ref _status_message, value);
                OnPropertyChanged(nameof(StatusMessage));
            }
        }
        private bool _isCleanCheck;
        public bool IsCleanCheck
        {
            get => _isCleanCheck;
            set
            {
                Set(ref _isCleanCheck, value);
                OnPropertyChanged(nameof(IsCleanCheck));
            }
        }
        private string _iconText;
        public string IconText
        {
            get => _iconText;
            set
            {
                Set(ref _iconText, value);
                OnPropertyChanged(nameof(IconText));
            }
        }
        public ICommand StartBackupCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        private ObservableCollection<Production> _selectedItems = new ObservableCollection<Production>();
        public ObservableCollection<Production> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                Set(ref _selectedItems, value);
                OnPropertyChanged(nameof(SelectedItems));
            }
        }
        public BackupViewModel()
        {
            Datas = new ObservableCollection<Document>();
            IsCleanCheck = false;
            StartBackupCommand = new RelayCommand(Backup);
            RefreshCommand = new RelayCommand(Refresh);
            IconText = "\uf04b;";
            _productions = new ObservableCollection<Production>();
            using (IDbConnection connection = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
            {
                var data = connection.GetList<Production>(new { archived = 0 }).ToList().ToArray();
                data.ForEach(item => _productions.Add(item));
            }
        }

        void ResetProductioDates()
        {
            _productions.Clear();
            using (IDbConnection connection = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
            {
                var data = connection.GetList<Production>(new { archived = 0 }).ToList().ToArray();
                data.ForEach(item => _productions.Add(item));
            }
        }

        public object FetchDocuments(int prod_id)
        {
            using (IDbConnection connection = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
            {
                return connection.GetListAsync<Document>(new { prod_id = prod_id }).Result;
            }
        }

        async void Backup()
        {
            if (SelectedItems != null && SelectedItems.Count > 0)
            {
                IconText = "\uf04c";

                await Task.Run(async () =>
                {
                    List<AllColumns> data = new List<AllColumns>();
                    using (IDbConnection conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                    {
                        var prod_ids = SelectedItems.Select(i => i.id).ToList();
                        var cmd = $"SELECT * FROM document_tbl WHERE prod_id IN ({string.Join(",", prod_ids)})";
                        var temp_data = conn.QueryAsync<AllColumns>(cmd).Result;
                        data.AddRange(temp_data);
                    }

                    var tmpPath = Path.GetTempFileName();
                    using (StreamWriter objwriter = new StreamWriter(tmpPath, false))
                    {
                        objwriter.WriteLine($"{nameof(AllColumns.id)}, {nameof(AllColumns.pronumber)}, {nameof(AllColumns.terminal)}, {nameof(AllColumns.billtype)}, {nameof(AllColumns.pages)}, {nameof(AllColumns.comments)}, {nameof(AllColumns.remarks)}, {nameof(AllColumns.status)}, {nameof(AllColumns.biller_id)}, {nameof(AllColumns.query_id)}, {nameof(AllColumns.prod_id)}, {nameof(AllColumns.start_time)}, {nameof(AllColumns.end_time)}, {nameof(AllColumns.job)}, {nameof(AllColumns.ex1)}, {nameof(AllColumns.ex2)}, {nameof(AllColumns.ex3)}, {nameof(AllColumns.ex4)}, {nameof(AllColumns.ex5)}, {nameof(AllColumns.created_timestamp)}, {nameof(AllColumns.modefied_timestamp)}, {nameof(AllColumns.archived)}");
                        data.ToList().ToArray().Select((item, index) => new { Item = item, Index = index }).ForEach(dta =>
                        {
                            objwriter.Write($"{dta.Item.id},");
                            objwriter.Write($"{dta.Item.pronumber},");
                            objwriter.Write($"{dta.Item.terminal},");
                            objwriter.Write($"{dta.Item.billtype},");
                            objwriter.Write($"{dta.Item.pages},");
                            objwriter.Write($"{dta.Item.comments},");
                            objwriter.Write($"{dta.Item.remarks},");
                            objwriter.Write($"{dta.Item.status},");
                            objwriter.Write($"{dta.Item.biller_id},");
                            objwriter.Write($"{dta.Item.query_id},");
                            objwriter.Write($"{dta.Item.prod_id},");
                            objwriter.Write($"{dta.Item.start_time},");
                            objwriter.Write($"{dta.Item.end_time},");
                            objwriter.Write($"{dta.Item.job},");
                            objwriter.Write($"{dta.Item.ex1},");
                            objwriter.Write($"{dta.Item.ex2},");
                            objwriter.Write($"{dta.Item.ex3},");
                            objwriter.Write($"{dta.Item.ex4},");
                            objwriter.Write($"{dta.Item.ex5},");
                            objwriter.Write($"{dta.Item.created_timestamp},");
                            objwriter.Write($"{dta.Item.modefied_timestamp},");
                            objwriter.Write($"{dta.Item.archived}\n");

                            Thread.Sleep(100);
                            var percent = (((double)dta.Index + 1) / data.Count() * 100);
                            StatusMessage = $"Starting data backup {(dta.Index + 1)} of {data.Count()} ({percent.ToString("N2")}%) exported";
                            Progress = percent;

                        });
                    }

                    var backup = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dont_delete_backup");
                    var filename = $"backup{DateTime.Now.ToString("yyyyMMddHHmmss")}.csv";
                    if (!Directory.Exists(backup)) Directory.CreateDirectory(backup);
                    File.Copy(tmpPath, Path.Combine(backup, filename));
                    File.Delete(tmpPath);


                    if (File.Exists(Path.Combine(backup, filename)) && IsCleanCheck)
                    {
                        using (IDbConnection conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                        {
                            var prod_ids = SelectedItems.Select(i => i.id).ToList();
                            var cmd = $"DELETE FROM document_tbl WHERE prod_id IN ({string.Join(",", prod_ids)})";
                            await conn.QueryAsync(cmd);

                            cmd = $"UPDATE production_tbl SET archived = 1 WHERE id IN ({string.Join(",", prod_ids)})";
                            await conn.QueryAsync(cmd);
                        }
                    }
                });

                ResetProductioDates();
                IconText = "\uf04b;";

            }
        }
        void Refresh()
        {
            ResetProductioDates();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
