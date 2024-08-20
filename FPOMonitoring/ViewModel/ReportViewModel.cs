using Dapper;
using FPOMonitoring.Data.Class;
using FPOMonitoring.Services;
using FPOMonitoring.Tools.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Tools.Extension;
using SweetAlertSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;

namespace FPOMonitoring.ViewModel
{
    public class ReportViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private readonly LogService logService = new LogService();
        private readonly MySQLService mySQLService = new MySQLService();
        private readonly Utilities Util = new Utilities();
        private ObservableCollection<HourDetails> _hours;
        public IEnumerable<HourDetails> Hours
        {
            get => _hours;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public readonly string SavePath = $"{AppDomain.CurrentDomain.BaseDirectory}report_path.json";
        private readonly LogService logInfo = new LogService();
        private ObservableCollection<Production> _production_dates;
        public IEnumerable<Production> ProductionDates
        {
            get => _production_dates;
        }
        private Production _selected_date;
        public Production SelectedDate
        {
            get => _selected_date;
            set
            {
                Set(ref _selected_date, value);
                OnPropertyChanged(nameof(SelectedDate));
            }
        }
        public ICommand Generate { get; }
        public ICommand BrowseReport { get; }
        public ICommand BrowseDir { get; }
        private string _reportPath;
        public string ReportPath
        {
            get => _reportPath;
            set
            {
                Set(ref _reportPath, value);
                OnPropertyChanged(nameof(ReportPath));
            }
        }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                Set(ref _isEnabled, value);
                OnPropertyChanged(nameof(IsEnabled));
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
        public ReportViewModel()
        {
            Generate = new RelayCommand(GenerateReport);
            BrowseReport = new RelayCommand(BrowseReportFolder);
            BrowseDir = new RelayCommand(ShowFolderDir);
            Initialize();
        }

        public void Initialize()
        {
            if (ReportPath == null || ReportPath == "") ReportPath = (string)logInfo.Deserialize(SavePath);
            _production_dates = new ObservableCollection<Production>();
            _production_dates.AddRange(mySQLService.InitProductionDates(ViewModelLocator.Instance.LoginVM.ConnectionString).OrderByDescending(item => item.date));
            if (ProductionDates.Count() > 0) SelectedDate = ProductionDates.ToList().FirstOrDefault();
            IsEnabled = true;
            Progress = 0;
            StatusMessage = "Not Running...";
            InitHours();
        }

        void InitHours()
        {
            _hours = new ObservableCollection<HourDetails>
            {
                new HourDetails { index = 0, name = "00:00-12:59"},
                new HourDetails { index = 1, name = "01:00-01:59"},
                new HourDetails { index = 2, name = "02:00-02:59"},
                new HourDetails { index = 3, name = "03:00-03:59"},
                new HourDetails { index = 4, name = "04:00-04:59"},
                new HourDetails { index = 5, name = "05:00-05:59"},
                new HourDetails { index = 6, name = "06:00-06:59"},
                new HourDetails { index = 7, name = "07:00-07:59"},
                new HourDetails { index = 8, name = "08:00-08:59"},
                new HourDetails { index = 9, name = "09:00-09:59"},
                new HourDetails { index = 10, name = "10:00-10:59"},
                new HourDetails { index = 11, name = "11:00-11:59"},
                new HourDetails { index = 12, name = "12:00-12:59"},
                new HourDetails { index = 13, name = "13:00-13:59"},
                new HourDetails { index = 14, name = "14:00-14:59"},
                new HourDetails { index = 15, name = "15:00-15:59"},
                new HourDetails { index = 16, name = "16:00-16:59"},
                new HourDetails { index = 17, name = "17:00-17:59"},
                new HourDetails { index = 18, name = "18:00-18:59"},
                new HourDetails { index = 19, name = "19:00-19:59"},
                new HourDetails { index = 20, name = "20:00-20:59"},
                new HourDetails { index = 21, name = "21:00-21:59"},
                new HourDetails { index = 22, name = "22:00-22:59"},
                new HourDetails { index = 23, name = "23:00-23:59"},
            };
        }
        void GenerateReport()
        {
            try
            {

                var actual_sts_result = new TimeSpan();
                var pro_ste_result = new TimeSpan();

                var report_path = Path.Combine(ReportPath, SelectedDate.date);

                if (!Directory.Exists(report_path)) Directory.CreateDirectory(report_path);
                IsEnabled = false;
                Progress = 0;

                Task.Run(async () =>
                {
                    using (var conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                    {
                        var sql = $@"SELECT 
                                            d.*, q.*, u.*
                                        FROM
                                            document_tbl d
                                                LEFT JOIN
                                            user_tbl u ON u.id = d.biller_id
                                                LEFT JOIN
                                            query_tbl q ON q.id = d.query_id AND q.state = 0
                                        WHERE
                                            d.prod_id = {SelectedDate.id}
                                                AND d.status IN ('BILLED' , 'REJECT', 'QUERY')
                                                AND d.archived = 0
                                        ORDER BY d.end_time;";
                        var prod_data = await conn.QueryAsync<Document, Query, BillerInfo, Document>(sql, (d, q, u) =>
                        {
                            if (q != null) d.Queries.Add(q);
                            if (u != null)
                            {
                                d.BillerUsername = u.Username;
                                d.BillerName = u.Fullname;
                            }
                            return d;
                        });

                        var separated_by_jobs = prod_data.ToList().ToArray().GroupBy(item => item.job).ToList();
                        separated_by_jobs.ToList().ToArray().Select((itm, indx) => new { Item = itm, Index = indx })
                        .ForEach(job =>
                        {
                            if (job != null && job.Item.ToList() is List<Document> j)
                            {
                                List<Document> Documents = new List<Document>();
                                var path = Path.Combine(report_path, job.Item.Key);
                                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                                var separated_per_users = j.ToList().ToArray().GroupBy(user => user.biller_id).ToList();
                                separated_per_users.ToList().ToArray()
                                    .Select((itm, indx) => new { Item = itm, Index = indx })
                                    .ForEach(user =>
                                    {

                                        //Get activity data and merge it in users production data
                                        sql = $"SELECT start_time, end_time, job, message1, message2 FROM activity_tbl WHERE biller_id = {user.Item.Key} AND message1 IN ('Break', 'Idle') AND prod_id = {SelectedDate.id} AND archived = 0";
                                        var temp_activity_data = conn.QueryAsync<Activity>(sql).Result;
                                        if (temp_activity_data != null && temp_activity_data.Count() > 0) temp_activity_data = temp_activity_data.Where(a => a.end_time != DateTime.MinValue).ToList();
                                        List<Document> temp_doc_data = new List<Document>();
                                        temp_activity_data.ToList().ToArray().ForEach(item =>
                                        {

                                            Document document = new Document
                                            {
                                                BillerUsername = user.Item.FirstOrDefault().BillerUsername,
                                                BillerName = user.Item.FirstOrDefault().BillerName,
                                                job = item.job,
                                                start_time = item.start_time,
                                                end_time = item.end_time,
                                                Status = item.message1,
                                                Remarks = item.message2
                                            };
                                            temp_doc_data.Add(document);
                                        });

                                        List<Document> mixed_data = (List<Document>)user.Item.ToList();
                                        mixed_data.AddRange(temp_doc_data);
                                        mixed_data = mixed_data.OrderBy(d => d.start_time).ToList();
                                        mixed_data.ToList().ToArray().OrderBy(item => item.start_time).ToList().ToArray()
                                                .Select((itm, indx) => new { Item = itm, Index = indx })
                                                .ForEach(data =>
                                                {
                                                    pro_ste_result = data.Item.end_time.Subtract(data.Item.start_time);

                                                    if (data.Index != mixed_data.Count() - 1)
                                                    {
                                                        string stats = mixed_data.ToList()[data.Index + 1].Status.ToUpper();
                                                        if ((stats != "BREAK" && stats != "IDLE") && (data.Item.Status.ToUpper() != "BREAK" && data.Item.Status.ToUpper() != "IDLE"))
                                                        {
                                                            actual_sts_result = (mixed_data.ToList().ToArray()[data.Index + 1]).start_time.Subtract(data.Item.start_time);
                                                        }
                                                        else
                                                        {
                                                            actual_sts_result = pro_ste_result;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        actual_sts_result = pro_ste_result;
                                                    }

                                                    Thread.Sleep(100);
                                                    data.Item.ElapsedTimeSTE = pro_ste_result;
                                                    data.Item.ActualTimeSTS = actual_sts_result;

                                                    var percent = (((double)job.Index + 1) / separated_by_jobs.Count() * 100);
                                                    StatusMessage = $"Computing documents... Username [ {data.Item.BillerUsername} ] Pronumber [ {data.Item.Pronumber} ] ( {(data.Index + 1)} of {user.Item.Count()}), {(job.Index + 1)} of {separated_by_jobs.Count()} ({percent.ToString("N2")}%)";
                                                    Progress = percent;
                                                });

                                        Documents.AddRange(mixed_data);
                                    });

                                var clean_data = Documents.Where(d => d.Status.ToUpper() != "BREAK" && d.Status.ToUpper() != "IDLE").ToList();
                                GenerateBillerProdReport(Documents, path);
                                GenerateHourlyStatReports(clean_data, path);
                                GenerateSummary(clean_data, path);

                            }
                        });

                        sql = $@"SELECT 
                                  a.*, u.*
                              FROM
                                  activity_tbl a
                                      LEFT JOIN
                                  user_tbl u ON u.id = a.biller_id
                              WHERE
                                  a.prod_id = {SelectedDate.id}
                                      AND a.archived = 0 
                                      AND a.message1 != 'No Activity'
                              ORDER BY a.end_time;";
                        var activities = await conn.QueryAsync<Activity, BillerInfo, Activity>(sql, (a, u) =>
                        {
                            if (u != null)
                            {
                                a.BillerUsername = u.Username;
                                a.BillerName = u.Fullname;
                            }
                            return a;
                        });

                        GenerateIdleReport(activities.ToList(), report_path);
                        GenerateIdleHourlyReport(activities.ToList(), report_path);
                        GenerateIdleSummary(activities.ToList(), report_path);

                    }

                    StatusMessage = "Done";
                    Reset();

                });
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(GenerateReport));
                SweetAlert.Show("Error Message", ex.Message, SweetAlertSharp.Enums.SweetAlertButton.OK, SweetAlertSharp.Enums.SweetAlertImage.ERROR);
            }

        }
        void GenerateBillerProdReport(List<Document> docs, string report_path)
        {
            try
            {
                if (docs != null)
                {
                    List<string> body = new List<string>();
                    Progress = 0;

                    docs.ToList().ToArray().Select((itm, indx) => new { Item = itm, Index = indx }).ForEach(doc =>
                    {
                        Thread.Sleep(100);

                        string act_time = doc.Item.ActualTimeSTS.ToString("hh\\:mm\\:ss");
                        string act_dec = Math.Round(Util.ConvertTimeToDecimal(doc.Item.ActualTimeSTS), 4).ToString();
                        var data = $@"{doc.Item.BillerUsername}, {doc.Item.BillerName.Replace(",", " ").Replace("  ", " ")}, {SelectedDate.date}, {doc.Item.Pronumber}, {doc.Item.BillType}, {doc.Item.start_time.ToString("hh:mm:ss tt")}, {doc.Item.end_time.ToString("hh:mm:ss tt")}, {act_time}, {act_dec}, {doc.Item.Status}, {doc.Item.Remarks}";

                        body.Add(data);
                        var percent = (((double)doc.Index + 1) / docs.Count() * 100);
                        StatusMessage = $"Generating Biller Production Report... Username [ {doc.Item.BillerUsername} ] Pronumber [ {doc.Item.Pronumber} ], {(doc.Index + 1)} of {docs.Count()} ({percent.ToString("N2")}%)";
                        Progress = percent;

                    });

                    var tmpPath = Path.GetTempFileName();
                    using (StreamWriter objwriter = new StreamWriter(tmpPath, false))
                    {
                        objwriter.WriteLine($"As of '{DateTime.Now.ToString("F").Replace(",", " ").Replace("  ", " ")}'");
                        objwriter.WriteLine(@"Biller ID, Biller Name, Production Date, Pronumber, BOL Type, Start Time, End Time, Actual Time, Actual Time Decimal, Status, Remarks");
                        body.ToList().ToArray().ForEach(item => objwriter.WriteLine(item));
                    }

                    if (File.Exists(Path.Combine(report_path, "BillerProdReport.csv"))) File.Delete(Path.Combine(report_path, "BillerProdReport.csv"));
                    File.Copy(tmpPath, Path.Combine(report_path, "BillerProdReport.csv"));
                    File.Delete(tmpPath);

                }
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(GenerateBillerProdReport));
            }
        }
        void GenerateHourlyStatReports(List<Document> docs, string report_path)
        {
            try
            {
                if (docs != null)
                {
                    List<string> body = new List<string>();
                    Progress = 0;

                    docs.ToList().ToArray().Select((itm, indx) => new { Item = itm, Index = indx }).ForEach(doc =>
                    {
                        Thread.Sleep(100);
                        var d = doc.Item.CastTo<Document>();
                        if (d.end_time != DateTime.MinValue)
                        {
                            var time = _hours.Where(item => item.index == d.end_time.TimeOfDay.Hours).FirstOrDefault();
                            switch (d.Status)
                            {
                                case "BILLED":
                                    time.billed = (time.billed + 1);
                                    break;
                                case "REJECT":
                                    time.reject = (time.reject + 1);
                                    break;
                                case "QUERY":
                                    time.query = (time.query + 1);
                                    break;
                            }
                            time.total_processed = (time.total_processed + 1);

                            var percent = (((double)doc.Index + 1) / docs.Count() * 100);
                            StatusMessage = $"Generating Hourly Stat Report... Username [ {d.BillerUsername} ] Pronumber [ {d.Pronumber} ], {(doc.Index + 1)} of {docs.Count()} ({percent.ToString("N2")}%)";
                            Progress = percent;
                        }
                    });

                    var tmpPath = Path.GetTempFileName();
                    using (StreamWriter objwriter = new StreamWriter(tmpPath, false))
                    {
                        objwriter.WriteLine($"As of '{DateTime.Now.ToString("F").Replace(",", " ").Replace("  ", " ")}'");
                        objwriter.WriteLine(@"Time, Billed, Reject, Query, Total Processed");

                        Hours.ToList().ToArray().ForEach(hrs => objwriter.WriteLine($"{hrs.name}, {hrs.billed}, {hrs.reject}, {hrs.query}, {hrs.total_processed}"));
                        objwriter.WriteLine($"Total, {Hours.ToList().ToArray().Select(item => item.billed).Sum()}, " +
                            $"{Hours.ToList().ToArray().Select(item => item.reject).Sum()}, " +
                            $"{Hours.ToList().ToArray().Select(item => item.query).Sum()}, " +
                            $"{Hours.ToList().ToArray().Select(item => item.total_processed).Sum()}");
                    }

                    InitHours();
                    var fname = Path.Combine(report_path, "HourlyStatReports.csv");
                    if (File.Exists(fname)) File.Delete(fname);
                    File.Copy(tmpPath, fname);
                    File.Delete(tmpPath);

                }
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(GenerateHourlyStatReports));
            }

        }
        void GenerateSummary(List<Document> docs, string report_path)
        {
            try
            {
                if (docs != null)
                {
                    List<string> body = new List<string>();
                    Progress = 0;

                    var separated_per_billers = docs.ToList().ToArray().GroupBy(user => user.CastTo<Document>().BillerUsername).OrderBy(user => user.Key).ToList();
                    separated_per_billers.ToList().ToArray().Select((itm, indx) => new { Item = itm, Index = indx }).ForEach(async user =>
                    {

                        Thread.Sleep(100);

                        var user_data = user.Item.ToList().ToArray().OrderBy(item => item.CastTo<Document>().start_time);
                        var user_info = user_data.FirstOrDefault().CastTo<Document>();

                        var total_billed = user_data.ToList().ToList().Where(item => item.CastTo<Document>().Status == "BILLED").ToList().Count;
                        var total_reject = user_data.ToList().ToList().Where(item => item.CastTo<Document>().Status == "REJECT").ToList().Count;
                        var total_query = user_data.ToList().ToList().Where(item => item.CastTo<Document>().Status == "QUERY").ToList().Count;

                        var total_break_meal_time = await GetActivityTime("BREAK", "MEAL BREAK", SelectedDate.id, user_info.biller_id);
                        var total_break_nature_time = await GetActivityTime("BREAK", "NATURE BREAK", SelectedDate.id, user_info.biller_id);
                        var total_idle_time = await GetActivityTime("IDLE", null, SelectedDate.id, user_info.biller_id);

                        TimeSpan total_actual_time = TimeSpan.Zero;
                        user_data.ToList().ToArray().ForEach(item => total_actual_time = total_actual_time.Add(item.ActualTimeSTS));

                        TimeSpan total_manhrs = total_actual_time.Add(total_break_meal_time).Add(total_break_nature_time).Add(total_idle_time);
                        var temp_speed = (total_billed / Math.Round(Util.ConvertTimeToDecimal(total_actual_time), 4));
                        var speed = Math.Round(temp_speed, 4);

                        body.Add($"{user_info.BillerUsername}, {user_info.BillerName.Replace(",", " ").Replace("  ", " ")}, " +
                            $"{total_billed}, {total_reject}, {total_query}, " +
                            $"{total_manhrs}, {Math.Round(Util.ConvertTimeToDecimal(total_manhrs), 4)}, " +
                            $"{total_actual_time}, {Math.Round(Util.ConvertTimeToDecimal(total_actual_time), 4)} ," +
                            $"{total_break_meal_time}, {Math.Round(Util.ConvertTimeToDecimal(total_break_meal_time), 4)}, " +
                            $"{total_break_nature_time}, {Math.Round(Util.ConvertTimeToDecimal(total_break_nature_time), 4)}, " +
                            $" {total_idle_time}, {Math.Round(Util.ConvertTimeToDecimal(total_idle_time), 4)}, {speed}");

                        var percent = (((double)user.Index + 1) / separated_per_billers.Count() * 100);
                        StatusMessage = $"Generating Summary Report... Username [ {user_info.BillerUsername} ], {(user.Index + 1)} of {separated_per_billers.Count()} ({percent.ToString("N2")}%)";
                        Progress = percent;

                    });

                    var tmpPath = Path.GetTempFileName();
                    using (StreamWriter objwriter = new StreamWriter(tmpPath, false))
                    {
                        objwriter.WriteLine($"As of '{DateTime.Now.ToString("F").Replace(",", " ").Replace("  ", " ")}'");
                        objwriter.WriteLine(@"Biller ID, Biller Name, Billed, Reject, Query, Total Manhours, Manhours Decimal, Actual Billing Time, Actual Billing Time Decimal, Meal Break, Meal Break Decimal, Nature Break, Nature Break Decimal, Idle Time, Idle Time Decimal, Speed");
                        body.ToList().ToArray().ForEach(item => objwriter.WriteLine(item));
                    }

                    var fname = Path.Combine(report_path, "SummaryReport.csv");
                    if (File.Exists(fname)) File.Delete(fname);
                    File.Copy(tmpPath, fname);
                    File.Delete(tmpPath);
                }

            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(GenerateSummary));
            }

        }
        void GenerateIdleReport(List<Activity> activities, string report_path)
        {
            try
            {
                if (activities != null)
                {
                    List<string> body = new List<string>();
                    Progress = 0;

                    activities.ToList().ToArray().Select((itm, indx) => new { Item = itm, Index = indx }).ForEach(doc =>
                    {
                        var d = doc.Item.CastTo<Activity>();
                        TimeSpan elapsed_time = new TimeSpan();
                        if (d.end_time != DateTime.MinValue)
                        {
                            elapsed_time = d.end_time.Subtract(d.start_time.TimeOfDay).TimeOfDay;
                        }
                        var data = $"{d.BillerUsername}, {SelectedDate.date}, {d.job}, {d.start_time.ToString("hh:mm:ss tt")}, {d.end_time.ToString("hh:mm:ss tt")}, {elapsed_time}, {Math.Round(Util.ConvertTimeToDecimal(elapsed_time), 4)}, {d.message1}, {d.message2}";
                        body.Add(data);

                        var percent = (((double)doc.Index + 1) / activities.Count() * 100);
                        StatusMessage = $"Generating Idle Report... Username [ {d.BillerUsername} ] Job [ {d.job} ], {(doc.Index + 1)} of {activities.Count()} ({percent.ToString("N2")}%)";
                        Progress = percent;
                    });

                    var tmpPath = Path.GetTempFileName();
                    using (StreamWriter objwriter = new StreamWriter(tmpPath, false))
                    {
                        objwriter.WriteLine($"As of '{DateTime.Now.ToString("F").Replace(",", " ").Replace("  ", " ")}'");
                        objwriter.WriteLine(@"Biller ID, Production Date, Job, Start Time, End Time, Elapsed Time, Elapsed Time Decimal, Status, Reason");
                        body.ToList().ToArray().ForEach(item => objwriter.WriteLine(item));
                    }

                    var fname = Path.Combine(report_path, "IdleReport.csv");
                    if (File.Exists(fname)) File.Delete(fname);
                    File.Copy(tmpPath, fname);
                    File.Delete(tmpPath);

                }
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(GenerateIdleReport));
            }
        }
        void GenerateIdleHourlyReport(List<Activity> activities, string report_path)
        {
            try
            {
                if (activities != null)
                {
                    List<string> body = new List<string>();
                    Progress = 0;

                    activities.ToList().ToArray().Select((itm, indx) => new { Item = itm, Index = indx }).ForEach(activity =>
                    {
                        var a = activity.Item.CastTo<Activity>();
                        if (a.end_time != DateTime.MinValue)
                        {
                            var time = _hours.Where(item => item.index == a.end_time.TimeOfDay.Hours).FirstOrDefault();
                            if (a.message1.ToUpper() == "IDLE")
                            {
                                time.query = (time.query + 1);
                            }
                            else
                            {
                                if (!String.IsNullOrEmpty(a.message2))
                                {
                                    switch (a.message2.ToUpper())
                                    {
                                        case "NATURE BREAK":
                                            time.billed = (time.billed + 1);
                                            break;
                                        case "MEAL BREAK":
                                            time.reject = (time.reject + 1);
                                            break;
                                    }
                                }
                            }
                            time.total_processed = (time.total_processed + 1);

                            var percent = (((double)activity.Index + 1) / activities.Count() * 100);
                            StatusMessage = $"Generating Idle Hourly Report... Username [ {a.BillerUsername} ] Job [ {a.job} ], {(activity.Index + 1)} of {activities.Count()} ({percent.ToString("N2")}%)";
                            Progress = percent;
                        }
                    });

                    var tmpPath = Path.GetTempFileName();
                    using (StreamWriter objwriter = new StreamWriter(tmpPath, false))
                    {
                        objwriter.WriteLine($"As of '{DateTime.Now.ToString("F").Replace(",", " ").Replace("  ", " ")}'");
                        objwriter.WriteLine(@"Time, Nature Break, Meal Break, Idle Break, Total Processed"); //For reference only Nature = billed, meal = reject, idle = query

                        Hours.ToList().ToArray().ForEach(hrs => objwriter.WriteLine($"{hrs.name}, {hrs.billed}, {hrs.reject}, {hrs.query}, {hrs.total_processed}"));
                        objwriter.WriteLine($"Total, {Hours.ToList().ToArray().Select(item => item.billed).Sum()}, " +
                            $"{Hours.ToList().ToArray().Select(item => item.reject).Sum()}, " +
                            $"{Hours.ToList().ToArray().Select(item => item.query).Sum()}, " +
                            $"{Hours.ToList().ToArray().Select(item => item.total_processed).Sum()}");
                    }

                    InitHours();
                    var fname = Path.Combine(report_path, "IdleHourlyReports.csv");
                    if (File.Exists(fname)) File.Delete(fname);
                    File.Copy(tmpPath, fname);
                    File.Delete(tmpPath);

                }
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(GenerateIdleHourlyReport));
            }

        }
        void GenerateIdleSummary(List<Activity> activities, string report_path)
        {

            try
            {
                if (activities != null)
                {
                    var body = new List<string>();
                    var all_data = new List<List<Activity>>(); // Separate per reason while idle reason is the same one after the other
                    var separate_per_billers = activities.ToList().ToArray().GroupBy(user => user.BillerUsername).ToList();
                    separate_per_billers.ToList().ToArray().Select((itm, indx) => new { Item = itm, Index = indx })
                        .ForEach(datas =>
                        {

                            //Apply Grouping algorithm 
                            var list = new List<Activity>();
                            var first_reason = "";
                            datas.Item.ToList().ToArray().OrderBy(item => item.end_time).Select((itm, indx) => new { Item = itm, Index = indx })
                            .ForEach(data =>
                            {
                                var d = data.Item.CastTo<Activity>();
                                if (first_reason != d.message2)
                                {
                                    first_reason = d.message2;
                                    if (list.Count > 0)
                                    {
                                        all_data.Add(list);
                                        list = new List<Activity>();
                                    }
                                }

                                list.Add(d);
                                if (data.Index == datas.Item.Count() - 1) all_data.Add(list);

                                var percent = (((double)data.Index + 1) / datas.Item.Count() * 100);
                                StatusMessage = $"Generating Idle Summary Report... Username [ {d.BillerUsername} ] Job [ {d.job} ], {(data.Index + 1)} of {datas.Item.Count()} ({percent.ToString("N2")}%)";
                                Progress = percent;

                            });
                        });

                    all_data.ToList().ToArray().ForEach(item =>
                    {
                        var total_mhrs = new TimeSpan();
                        item.ToList().ToArray().Select((itm, indx) => new { Item = itm, Index = indx }).ForEach(data =>
                        {
                            if (data.Item.end_time != DateTime.MinValue)
                            {
                                total_mhrs = total_mhrs.Add(data.Item.end_time.Subtract(data.Item.start_time.TimeOfDay).TimeOfDay);
                            }
                        });

                        var dta = $"{item[0].BillerUsername}, {SelectedDate.date}, {item[0].start_time.ToString("hh:mm:ss tt")}, {item[item.Count - 1].end_time.ToString("hh:mm:ss tt")}, {total_mhrs}, {item[0].message2}";
                        body.Add(dta);
                    });

                    var tmpPath = Path.GetTempFileName();
                    using (StreamWriter objwriter = new StreamWriter(tmpPath, false))
                    {
                        objwriter.WriteLine($"As of '{DateTime.Now.ToString("F").Replace(",", " ").Replace("  ", " ")}'");
                        objwriter.WriteLine(@"Biller Username, Production Date, Start Time, End Time, Total Hours, Reason of Idle Time");
                        body.ToList().ToArray().ForEach(item => objwriter.WriteLine(item));
                    }

                    var fname = Path.Combine(report_path, "IdleSummaryReport.csv");
                    if (File.Exists(fname)) File.Delete(fname);
                    File.Copy(tmpPath, fname);
                    File.Delete(tmpPath);

                }
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(GenerateIdleSummary));
            }
        }
        private async Task<TimeSpan> GetActivityTime(string type, string m2, int prodid, int billerid)
        {
            try
            {
                using (IDbConnection conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                {
                    IEnumerable<Activity> data = new List<Activity>();
                    if (!String.IsNullOrEmpty(m2))
                    {
                        data = await conn.GetListAsync<Activity>(new { message2 = m2, prod_id = prodid, biller_id = billerid });
                    }
                    else
                    {
                        data = await conn.GetListAsync<Activity>(new { message1 = type, prod_id = prodid, biller_id = billerid });
                    }

                    if (data != null && data.Count() > 0)
                    {
                        var ts = new TimeSpan();
                        data.ToList().ToArray().Where(item => item.message1.ToUpper() != "NO ACTIVITY").ForEach(item =>
                        {
                            var elapsed_ste = item.end_time.Subtract(item.start_time.TimeOfDay).TimeOfDay;
                            ts = ts.Add(elapsed_ste);
                        });
                        return ts;
                    }
                }
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(GetActivityTime));
            }

            return new TimeSpan();
        }
        private void Reset()
        {
            IsEnabled = true;
        }
        private void BrowseReportFolder()
        {
            if (ReportPath == null || ReportPath == "") return;
            Process.Start(ReportPath);
        }
        private void ShowFolderDir()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK && dialog.SelectedPath != "")
                {
                    ReportPath = dialog.SelectedPath;
                    logInfo.Serialize(SavePath, ReportPath);
                }
            }
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
