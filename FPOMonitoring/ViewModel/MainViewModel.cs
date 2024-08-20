using Dapper;
using FPOMonitoring.Data.Class;
using FPOMonitoring.Services;
using FPOMonitoring.Tools.Helpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using HandyControl.Tools.Extension;
using MySqlX.XDevAPI.Common;
using SweetAlertSharp;
using SweetAlertSharp.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using CheckBox = FPOMonitoring.Data.Class.CheckBox;
using FPOMonitoring.View.Window;
using System.Collections;
using System.Windows.Media.Animation;


namespace FPOMonitoring.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        readonly MySQLService mySQLService = new MySQLService();
        readonly Utilities utilities = new Utilities();

        // Declarations
        #region "Variables"
        private DispatcherTimer _timer;
        private readonly Pagination<User> UserPagination;
        private readonly LogService logService = new LogService();
        public IEnumerable<User> TotalUsers => UserPagination.TotalList;
        public IEnumerable<User> DataSourceProduction
        {
            get => UserPagination.DataSourceProduction;
        }
        private string _searchtext;
        public string SearchText
        {
            get => _searchtext;
            set
            {
                Set(ref _searchtext, value);
                OnPropertyChanged(nameof(SearchText));
                IsSearched = true;
            }
        }
        private int _pageLimit;
        public int PageLimit
        {
            get => _pageLimit;
            set
            {
                Set(ref _pageLimit, value);
                OnPropertyChanged(nameof(PageLimit));
            }
        }
        private bool IsChanged = false;
        private bool IsSearched = false;
        private bool is_production;
        public bool IsProduction
        {
            get => is_production;
            set
            {
                Set(ref is_production, value);
                OnPropertyChanged(nameof(IsProduction));
            }
        }
        private string _selectedItem;
        public string SelectedItem
        {
            get => _selectedItem;
            set
            {
                Set(ref _selectedItem, value);
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        private ObservableCollection<string> _showlimit;
        public ObservableCollection<string> ShowLimit
        {
            get => _showlimit;
            set
            {
                Set(ref _showlimit, value);
                OnPropertyChanged(nameof(ShowLimit));
            }
        }
        private ObservableCollection<Production> _productionDates;
        public ObservableCollection<Production> ProductionDates
        {
            get => _productionDates;
            set
            {
                Set(ref _productionDates, value);
                OnPropertyChanged(nameof(ProductionDates));
            }
        }
        private Production _selectedProdDate;
        public Production SelectedProdDate
        {
            get => _selectedProdDate;
            set
            {
                Set(ref _selectedProdDate, value);
                OnPropertyChanged(nameof(SelectedProdDate));

                ProdStartIndex = 0;
                documents.Clear();
            }
        }
        private int _ongoing_count;
        public int OngoingCount
        {
            get => _ongoing_count;
            set
            {
                Set(ref _ongoing_count, value);
                OnPropertyChanged(nameof(OngoingCount));
            }
        }
        private int _billed_count;
        public int BilledCount
        {
            get => _billed_count;
            set
            {
                Set(ref _billed_count, value);
                ForBilled = (Target - _billed_count);
                SetCompletionTime();
                OnPropertyChanged(nameof(BilledCount));
            }
        }
        private int _reject_count;
        public int RejectCount
        {
            get => _reject_count;
            set
            {
                Set(ref _reject_count, value);
                OnPropertyChanged(nameof(RejectCount));
            }
        }
        private int _query_count;
        public int QueryCount
        {
            get => _query_count;
            set
            {
                Set(ref _query_count, value);
                OnPropertyChanged(nameof(QueryCount));
            }
        }
        private int _answered_count;
        public int AnsweredCount
        {
            get => _answered_count;
            set
            {
                Set(ref _answered_count, value);
                OnPropertyChanged(nameof(AnsweredCount));
            }
        }
        private bool _is_open;
        public bool IsOpen
        {
            get => _is_open;
            set
            {
                Set(ref _is_open, value);
                OnPropertyChanged(nameof(IsOpen));
            }
        }
        private string _dateAndTime;
        public string DateAndTime
        {
            get => _dateAndTime;
            set
            {
                Set(ref _dateAndTime, value);
                OnPropertyChanged(nameof(DateAndTime));
            }
        }
        private SolidColorBrush _completionForeground;
        public SolidColorBrush CompletionForeground
        {
            get => _completionForeground;
            set
            {
                Set(ref _completionForeground, value);
                OnPropertyChanged(nameof(CompletionForeground));
            }
        }

        #region "Production Table"
        private ObservableCollection<Document> documents;
        public IEnumerable<Document> Documents
        {
            get => documents;
        }
        private int _prodStartIndex;
        public int ProdStartIndex
        {
            get => _prodStartIndex;
            set
            {
                Set(ref _prodStartIndex, value);
                OnPropertyChanged(nameof(ProdStartIndex));
            }
        }
        private int _prodEndIndex;
        public int ProdEndIndex
        {
            get => _prodEndIndex;
            set
            {
                Set(ref _prodEndIndex, value);
                OnPropertyChanged(nameof(ProdEndIndex));
            }
        }
        private int _prodMaxPage;
        public int ProdMaxPage
        {
            get => _prodMaxPage;
            set
            {
                Set(ref _prodMaxPage, value);
                OnPropertyChanged(nameof(ProdMaxPage));
            }
        }
        private int _prodTotalEntries;
        public int ProdTotalEntries
        {
            get => _prodTotalEntries;
            set
            {
                Set(ref _prodTotalEntries, value);
                OnPropertyChanged(nameof(ProdTotalEntries));
            }
        }
        private int _prodPageIndex;
        public int ProdPageIndex
        {
            get => _prodPageIndex;
            set
            {
                Set(ref _prodPageIndex, value);
                OnPropertyChanged(nameof(ProdPageIndex));
            }
        }
        #endregion

        #region "Page Info"
        public int StartIndex => UserPagination.StartIndex;
        public int EndIndex => UserPagination.EndIndex;
        private int _pageindex;
        public int PageIndex
        {
            get => _pageindex;
            set
            {
                Set(ref _pageindex, value);
                UserPagination.SetPageIndex(PageIndex);
                OnPropertyChanged(nameof(PageIndex));
            }
        }
        public int MaxPage => UserPagination.MaxPage;
        public int TotalEntries => UserPagination.TotalEntries;
        public ICommand PageUpdatedCmd => UserPagination.PageUpdatedCmd;
        #endregion

        #region "Modal Information"
        private ObservableCollection<Account> eAccounts;
        public IEnumerable<Account> EAccounts
        {
            get => eAccounts;
        }
        #endregion 
        private string _status_text;
        public string StatusText
        {
            get => _status_text;
            set
            {
                Set(ref _status_text, value);
                OnPropertyChanged(nameof(StatusText));
            }
        }
        private ICollection<CheckBox> _filteredStatus;
        public ICollection<CheckBox> FilteredStatus
        {
            get => _filteredStatus;
            set
            {
                Set(ref _filteredStatus, value);
                OnPropertyChanged(nameof(FilteredStatus));
            }
        }
        private CancellationTokenSource cancellationTokenSource;
        private string _server;
        public string Server
        {
            get => _server;
            set
            {
                Set(ref _server, value);
                OnPropertyChanged(nameof(Server));
            }
        }
        private string _start;
        public string Start
        {
            get => _start;
            set
            {
                Set(ref _start, value);
                OnPropertyChanged(nameof(Start));
            }
        }
        private string _end;
        public string End
        {
            get => _end;
            set
            {
                Set(ref _end, value);
                OnPropertyChanged(nameof(End));
            }
        }
        private double _target;
        public double Target
        {
            get => _target;
            set
            {
                Set(ref _target, value);
                ForBilled = (_target - BilledCount);
                SetCompletionTime();
                OnPropertyChanged(nameof(Target));
            }
        }
        private double _for_billed;
        public double ForBilled
        {
            get => _for_billed;
            set
            {
                Set(ref _for_billed, value);
                OnPropertyChanged(nameof(ForBilled));
            }
        }
        private string _completion_time;
        public string CompletionTime
        {
            get => _completion_time;
            set
            {
                Set(ref _completion_time, value);
                OnPropertyChanged(nameof(CompletionTime));
            }
        }
        private string _production_hrs;
        public string ProductionHrs
        {
            get => _production_hrs;
            set
            {
                Set(ref _production_hrs, value);
                OnPropertyChanged(nameof(ProductionHrs));
            }
        }
        private ObservableCollection<DateTime> _endtime_list;
        public IEnumerable<DateTime> EndTimeList
        {
            get => _endtime_list;
        }
        private bool _isRunning;
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                Set(ref _isRunning, value);
                OnPropertyChanged(nameof(IsRunning));
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            _timer.Tick += _timer_Tick;
            _timer.Start();

            InitializeLimits();
            cancellationTokenSource = new CancellationTokenSource();
            UserPagination = new Pagination<User>();
            PageIndex = 1;
            mySQLService.CheckProductionDate(ViewModelLocator.Instance.LoginVM.ConnectionString);
            documents = new ObservableCollection<Document>();
            ProductionDates = new ObservableCollection<Production>();
            ProductionDates.AddRange(mySQLService.InitProductionDates(ViewModelLocator.Instance.LoginVM.ConnectionString).OrderByDescending(item => item.date));
            SelectedProdDate = ProductionDates?.OrderByDescending(item => item.id).ToList()[0];
            eAccounts = new ObservableCollection<Account> { new Account { Key = 0, Name = "ADMIN" }, new Account { Key = 1, Name = "PC" }, new Account { Key = 2, Name = "BILLER" } };
            FilteredStatus = new ObservableCollection<CheckBox>
            {
                new CheckBox { Name = "All", IsChecked = true, Key = -1 },
                new CheckBox { Name = "ONGOING", IsChecked = true, Key = 0},
                new CheckBox { Name = "BILLED", IsChecked = true, Key = 1},
                new CheckBox { Name = "REJECT", IsChecked = true, Key = 2},
                new CheckBox { Name = "QUERY", IsChecked = true, Key = 3},
                new CheckBox { Name = "ANSWERED", IsChecked = true, Key = 4},
            };
            StatusText = string.Join(", ", FilteredStatus.Where(item => item.IsChecked && item.Name.ToUpper() != "ALL").Select(item => item.Name));
            Server = $"{(ViewModelLocator.Instance.LoginVM.ConnectionString.Split(';')[0]).Split('=')[1]} : {ViewModelLocator.Instance.LoginVM.Settings.Port}";
            Start = ViewModelLocator.Instance.LoginVM.Settings.Start;
            End = ViewModelLocator.Instance.LoginVM.Settings.End;
            SearchText = "";
            CompletionForeground = new SolidColorBrush(Colors.White);
            if (Start != null && End != null)
            {
                TimeSpan start = TimeSpan.Parse(Start);
                TimeSpan end = TimeSpan.Parse(End);
                TimeSpan prod = end.Subtract(start);
                ProductionHrs = prod.ToString(@"hh\:mm\:ss");
            }
            IsRunning = false;
            IsProduction = false;
            StartContinuousTask();
        }

        // INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetPageSize(int value)
        {
            if (SelectedItem != null)
            {
                if (SelectedItem != "Dont Limit")
                {
                    string[] _limit = SelectedItem.Split(' ');
                    if (_limit.Length == 4 && int.TryParse(_limit[2], out int result))
                    {
                        if (UserPagination != null) UserPagination.SetPageSize(result);
                    }
                }
                else
                {
                    if (UserPagination != null) UserPagination.SetPageSize(value);
                }
            }
        }
        public void Refresh()
        {
            UserPagination.Clear();
            documents.Clear();
            ProductionDates.Clear();
            ProductionDates.AddRange(mySQLService.InitProductionDates(ViewModelLocator.Instance.LoginVM.ConnectionString).OrderByDescending(item => item.date));
        }
        string SetStatus(int i)
        {
            if (i == 0)
            {
                return "Disconnected";
            }
            else
            {
                return "Connected";
            }
        }

        /// <summary>
        /// Start Process
        /// </summary>
        async void StartContinuousTask()
        {
            try
            {
                using (IDbConnection connection = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                {
                    List<Document> temp_production = new List<Document>();
                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        // Your continuous task logic here
                        // This can include background processing, polling, etc.
                        if (connection != null)
                        {
                            var users = await connection.GetListAsync<User>(new { account = 2, archived = 0 });
                            if (!string.IsNullOrEmpty(SearchText) && !IsProduction && users != null)
                            {
                                users = users.ToList().ToArray().Where(user => user.Username.ToUpper().Contains(SearchText.ToUpper()) || user.Fullname.ToUpper().Contains(SearchText.ToUpper())).ToList();
                                if (IsSearched)
                                {
                                    UserPagination.Clear();
                                    IsSearched = false;
                                }
                            }

                            SetPageSize(users.Count());
                            if (IsChanged)
                            {
                                UserPagination?.RefreshPageData();
                                IsChanged = false;
                            }
                            users.ToList().ToArray().ForEach(user =>
                            {
                                if (!TotalUsers.ToList().ToArray().Any(item => item.Id == user.Id))
                                {
                                    user.ViewProcessCommand = new RelayCommand<User>(ViewProcess);
                                    user.ForceLogoutCommand = new RelayCommand<User>(ForceLogout);
                                    user.DeleteCommand = new RelayCommand<User>(DeleteAccount);
                                    UserPagination.AddQueue(user, PageIndex);
                                }
                                var u = TotalUsers.ToList().ToArray().Where(item => item.Id == user.Id).FirstOrDefault();
                                u.Fullname = user.Fullname;
                                u.Username = user.Username;
                                u.password = user.password;
                                u.password_length = user.password_length;
                                u.account = user.account;
                                u.is_connected = user.is_connected;
                                u.last_login = user.last_login;
                                u.Pages = user.Pages;

                                if ((u.last_login != null && u.last_login != DateTime.MinValue && u.last_login.ToString("yyyy-MM-dd") == SelectedProdDate.date) && u.is_connected == 1)
                                {
                                    u.Status = "CONNECTED";
                                    u.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(ColorsEnum.Success)));
                                }
                                else
                                {
                                    u.Status = "DISCONNECTED";
                                    u.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(ColorsEnum.Danger)));
                                }

                                if (!IsRunning)
                                {
                                    u.HourlyReset();
                                    documents.Clear();
                                }
                            });

                            if (IsRunning)
                            {
                                //Fetch Production
                                #region "Production Tab"
                                var filter = string.Join(",", FilteredStatus.ToList().ToArray().Where(item => item.Name.ToUpper() != "ALL" && item.IsChecked).Select(item => $"'{item.Name}'"));
                                if (filter != null && filter != "")
                                {
                                    var query = $"SELECT d.id, d.pronumber, d.billtype, d.pages, d.status, d.biller_id, d.start_time, d.end_time, d.job,d.remarks, d.query_id, d.comments,u.fullname, u.username " +
                                                  $"FROM document_tbl d INNER JOIN user_tbl u ON u.id = d.biller_id WHERE d.status IN ({filter}) " +
                                                  $"AND d.prod_id = {SelectedProdDate.id} AND d.archived = 0 ORDER BY d.status = 'ONGOING';";

                                    var production_datas = await connection.QueryAsync<Document>(query);
                                    temp_production = production_datas.ToList();

                                    if (!string.IsNullOrEmpty(SearchText) && IsProduction && production_datas != null)
                                    {
                                        production_datas = production_datas.ToList().ToArray().Where(data => data.Pronumber.ToUpper().Contains(SearchText.ToUpper())).ToList();
                                        if (IsSearched)
                                        {
                                            documents.Clear();
                                            IsSearched = false;
                                        }
                                    }
                                    if (production_datas != null && production_datas.Count() > 0)
                                    {
                                        ProdStartIndex = 1;
                                        production_datas.ToList().ToArray().ForEach(async item =>
                                        {
                                            if (!documents.ToList().ToArray().Any(doc => doc.Id == item.Id))
                                            {
                                                item.DeleteCommand = new RelayCommand<Document>(Delete);
                                                item.AnswerCommand = new RelayCommand<Document>(Answer);
                                                documents.Insert(0, item);
                                            }
                                            var _doc = documents.ToList().ToArray().Where(doc => doc.Id == item.Id).FirstOrDefault();
                                            _doc.start_time = item.start_time;
                                            _doc.end_time = item.end_time;
                                            _doc.Status = item.Status;
                                            _doc.Comments = item.Comments;
                                            _doc.Pronumber = item.Pronumber;

                                            // Get Queries
                                            var query_sql = "SELECT q.id, q.doc_id, q.answer, q.time_start, " +
                                                "q.time_end, q.query, q.answered_by, q.processed_by, q.state, q.created_datetime, " +
                                                "q.archived, p.fullname AS ProcessedByName, a.fullname AS AnsweredByName FROM query_tbl q " +
                                                "LEFT JOIN user_tbl p ON q.processed_by = p.id " +
                                                "LEFT JOIN user_tbl a ON q.answered_by = a.id " +
                                                $"WHERE q.doc_id = {_doc.Id};";

                                            var query_list = await connection.QueryAsync<Query>(query_sql);
                                            if (query_list != null && query_list is List<Query> q && q.Count > 0)
                                            {
                                                q.ToList().ToArray().ForEach(qry =>
                                                {
                                                    var row = item.Queries.ToList().ToArray().Where(r => r.id == qry.id).ToList().FirstOrDefault();
                                                    if (row != null)
                                                    {
                                                        row = qry;
                                                    }
                                                    else
                                                    {
                                                        item.Queries.Add(qry);
                                                    }
                                                });
                                                item.Queries.ToList().ToArray().ForEach(i =>
                                                {
                                                    if (!query_list.ToList().ToArray().Any(row => row.id == i.id))
                                                    {
                                                        item.Queries.Remove(i);
                                                    }
                                                });
                                            }
                                            _doc.Queries = item.Queries;
                                        });

                                        //Remove data not exists in prod
                                        documents.ToList().ToArray().ForEach(doc =>
                                        {
                                            if (!production_datas.ToList().ToArray().Any(row => row.Id == doc.Id))
                                            {
                                                documents.Remove(doc);
                                            }
                                        });
                                    }
                                    else documents.Clear();
                                }
                                else documents.Clear();

                                ProdEndIndex = Documents.ToList().ToArray().Count();
                                ProdTotalEntries = Documents.ToList().ToArray().Count();
                                #endregion
                            }

                            if (TotalUsers.Count() > 0)
                            {
                                UserPagination.SetPaginationInfo((UserPagination.PageIndex - 1) * UserPagination.PageSize + 1, (int)UserPagination.TotalList?.Count(), (int)Math.Ceiling((double)UserPagination.TotalEntries / UserPagination.PageSize));
                                TotalUsers.ToList().ToArray().ForEach(user =>
                                {
                                    DateTime stime = new DateTime();
                                    user.Reset();

                                    var prod_data_per_biller = temp_production.ToList().ToArray().Where(item => item.biller_id == user.Id); //await connection.GetListAsync<Document>(new { biller_id = user.Id, prod_id = SelectedProdDate.id, });
                                    user.BilledCount = prod_data_per_biller.ToList().ToArray().Where(item => item.Status == "BILLED").Count();
                                    user.RejectCount = prod_data_per_biller.ToList().ToArray().Where(item => item.Status == "REJECT").Count();

                                    IEnumerable<Activity> activity_data = connection.GetListAsync<Activity>(new { biller_id = user.Id, prod_id = SelectedProdDate.id, archived = 0 }).Result;
                                    if (IsRunning)
                                    {
                                        if (prod_data_per_biller != null && prod_data_per_biller.Count() > 0)
                                        {
                                            var data = prod_data_per_biller.OrderByDescending(item => item.Status == "ONGOING").FirstOrDefault();
                                            user.Remarks = $"Last Processed pronumber {data.Pronumber} on {data.end_time}";
                                            user.QueryId = data.query_id;

                                            if (data.Status == "ONGOING" && user.is_connected > 0)
                                            {
                                                user.BillType = data.BillType;
                                                user.Pronumber = data.Pronumber;
                                                user.Pages = data.Pages;
                                                user.Remarks = data.Comments;
                                                user.Status = data.Status.ToUpper();
                                                user.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(Data.Class.ColorsEnum.Warning)));
                                                user.Job = data.job;
                                                stime = data.start_time;
                                            }

                                            #region "Summary Tab and Billed Per HR"
                                            user.StartTime = prod_data_per_biller.ToList().ToArray()[0].start_time;
                                            if (data != null && data.end_time != DateTime.MinValue) user.EndTime = data.end_time;
                                            if (temp_production.ToList().ToArray().Where(item => item.Status == "BILLED").Count() != BilledCount)
                                            {
                                                user.HourlyReset();
                                                prod_data_per_biller.ToList().ToArray().ForEach(dta =>
                                                {
                                                    var time = user.Hourly.Where(item => item.index == dta.end_time.TimeOfDay.Hours).FirstOrDefault();
                                                    switch (dta.Status)
                                                    {
                                                        case "BILLED":
                                                            time.billed = (time.billed + 1);
                                                            break;
                                                    }
                                                });
                                            }

                                            //Combine the document and activity time per biller
                                            IEnumerable<TimeInfo> combined_times = prod_data_per_biller.ToArray().ToList().
                                                Select(item => new TimeInfo { start_time = item.start_time, end_time = item.end_time, Status = item.Status }).ToList().
                                                Concat(activity_data.ToArray().ToList().Where(ac => !ac.message1.ToUpper().Equals("NO ACTIVITY")).
                                                    Select(item => new TimeInfo { start_time = item.start_time, end_time = item.end_time, Status = item.message1.ToUpper() }).ToList()).
                                                OrderBy(item => item.start_time);

                                            TimeSpan total_actual_time = ComputeTotalActualTime(combined_times);
                                            user.ProdTime = total_actual_time.ToString();
                                            user.ProdTimeDecimal = Math.Round(utilities.ConvertTimeToDecimal(total_actual_time), 4);
                                            user.BPH = Math.Round((user.BilledCount / user.ProdTimeDecimal), 4);
                                            #endregion
                                        }
                                        else
                                        {
                                            user.HourlyReset();
                                            if (user.Status.ToUpper() == "CONNECTED") user.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(ColorsEnum.Success)));
                                            else user.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(ColorsEnum.Danger)));
                                        }

                                        if (user.is_connected > 0 && user.Status == "CONNECTED")
                                        {
                                            if (activity_data != null && activity_data.Count() > 0)
                                            {
                                                var data2 = activity_data.OrderByDescending(item => item.id).FirstOrDefault();
                                                if (data2.end_time == DateTime.MinValue)
                                                {
                                                    stime = data2.start_time;
                                                    user.Remarks = data2.message2;
                                                    user.Status = data2.message1.ToUpper();
                                                    user.StartTime = stime;

                                                    if (user.Status.ToUpper() == "NO ACTIVITY")
                                                    {
                                                        user.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(ColorsEnum.Danger)));
                                                    }
                                                    else
                                                    {
                                                        user.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(ColorsEnum.Warning)));
                                                    }
                                                }
                                            }
                                        }
                                        if ((user.Status.ToUpper() != "DISCONNECTED" && user.Status.ToUpper() != "CONNECTED") && stime != null && stime is DateTime d)
                                        {
                                            TimeSpan time = TimeSpan.Parse(stime.TimeOfDay.ToString());
                                            user.ElapsedTime = (TimeSpan.Parse(DateTime.Now.ToString("HH:mm:ss")).Subtract(time)).ToString();
                                        }

                                        var nature_break = activity_data.ToList().ToArray().Where(item => item.message1.ToUpper() == "BREAK" && item.message2.ToUpper() == "Nature Break".ToUpper());
                                        var meal_break = activity_data.ToList().ToArray().Where(item => item.message1.ToUpper() == "BREAK" && item.message2.ToUpper() == "Meal Break".ToUpper());
                                        var idle_break = activity_data.ToList().ToArray().Where(item => item.message1.ToUpper() == "IDLE");

                                        user.NatureBreak = ComputeTime(nature_break).ToString();
                                        user.MealBreak = ComputeTime(meal_break).ToString();
                                        user.IdleBreak = ComputeTime(idle_break).ToString();
                                    }
                                });
                            }

                            var bcount = temp_production.ToList().ToArray().Where(item => item.Status == "BILLED").Count();
                            if (bcount != BilledCount) BilledCount = bcount;
                            OngoingCount = temp_production.ToList().ToArray().Where(item => item.Status == "ONGOING").Count();
                            RejectCount = temp_production.ToList().ToArray().Where(item => item.Status == "REJECT").Count();
                            QueryCount = temp_production.ToList().ToArray().Where(item => item.Status == "QUERY").Count();
                            AnsweredCount = temp_production.ToList().ToArray().Where(item => item.Status == "ANSWERED").Count();
                            await Task.Delay(TimeSpan.FromSeconds(1)); // Delay for 1 second before the next iteration

                        }
                    }
                }

            }
            catch (TaskCanceledException e)
            {
                // Task was cancelled
                logService.ErrorLogToFile(e.Message, e.StackTrace, nameof(StartContinuousTask));
            }
        }
        void Delete(Document doc)
        {
            var ask = new SweetAlert
            {
                Title = "Question",
                Message = "Do you want to delete this row ?",
                OkText = "Yes, delete it!",
                CancelText = "Cancel",
                MsgImage = SweetAlertImage.QUESTION,
                MsgButton = SweetAlertButton.YesNo,
            };
            if (ask.ShowDialog() == SweetAlertResult.OK)
            {
                try
                {
                    using (IDbConnection conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                    {
                        var sql = $"UPDATE document_tbl SET archived = 1 WHERE id = {doc.Id}";
                        conn.Query(sql);
                        documents.ToList()?.Remove(doc);
                        SweetAlert.Show("Success", $"Pronumber {doc.Pronumber} was successfully deleted.", SweetAlertButton.OK, SweetAlertImage.SUCCESS);
                    }
                }
                catch (Exception ex)
                {
                    logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(Delete));
                }
            }
        }
        void Answer(Document doc)
        {
            if (IsRunning)
            {
                try
                {
                    if (doc.Status == "QUERY")
                    {
                        ViewModelLocator.Instance.QueryVM.Doc = doc;
                        QueryContent window = new QueryContent(doc);
                        window.Owner = System.Windows.Application.Current.MainWindow;
                        window.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(Answer));
                }
            }
        }
        void InitializeLimits()
        {
            ShowLimit = new ObservableCollection<string>();
            ShowLimit = new ObservableCollection<string>
            {
                "Dont Limit",
                "Limit to 10 rows",
                "Limit to 50 rows",
                "Limit to 100 rows",
                "Limit to 200 rows",
                "Limit to 300 rows",
                "Limit to 400 rows",
                "Limit to 500 rows",
                "Limit to 1000 rows",
                "Limit to 2000 rows",
                "Limit to 5000 rows",
                "Limit to 10000 rows",
                "Limit to 50000 rows"
            };
            SelectedItem = ShowLimit[0]; // Set initial selected item
        }
        void ViewProcess(User user)
        {
            IsOpen = true;
        }
        void ForceLogout(User user)
        {
            try
            {
                if (user != null && user.Id > 0 && user.Status.ToUpper() != "DISCONNECTED")
                {
                    var ask = new SweetAlert
                    {
                        Title = "Question",
                        Message = "Do you wish to logout this user ?",
                        OkText = "Yes, Logout it!",
                        CancelText = "Cancel",
                        MsgImage = SweetAlertImage.QUESTION,
                        MsgButton = SweetAlertButton.YesNo,
                    };
                    if (ask.ShowDialog() == SweetAlertResult.OK)
                    {
                        using (IDbConnection conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                        {
                            var sql = $"UPDATE user_tbl SET is_connected = 0 WHERE id = {user.Id}";
                            conn.Query(sql);
                            SweetAlert.Show("Success", $"User {user.Fullname} was force to logout.", SweetAlertButton.OK, SweetAlertImage.SUCCESS);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(ForceLogout));
            }
        }
        void DeleteAccount(User user)
        {
            try
            {
                var ask = new SweetAlert
                {
                    Title = "Question",
                    Message = "Do you want to DELETE this user account ?\n\rNote: All of the users's data will be lost.",
                    OkText = "Yes, Delete it!",
                    CancelText = "Cancel",
                    MsgImage = SweetAlertImage.QUESTION,
                    MsgButton = SweetAlertButton.YesNo,
                };
                if (ask.ShowDialog() == SweetAlertResult.OK)
                {
                    using (IDbConnection conn = mySQLService.GetDbContext(ViewModelLocator.Instance.LoginVM.ConnectionString))
                    {
                        var sql = $"UPDATE user_tbl SET archived = 1 WHERE id = {user.Id}";
                        conn.Query(sql);
                        UserPagination.RemoveQueue(user, PageIndex);

                        List<Document> user_datas = new List<Document>();
                        if (IsRunning) user_datas = documents.ToList().ToArray().Where(data => data.biller_id == user.Id).ToList();
                        else user_datas = conn.GetList<Document>(new { prod_id = SelectedProdDate.id, biller_id = user.Id, archived = 0 }).ToList();

                        if (user_datas != null && user_datas.Count > 0)
                        {
                            user_datas.ToList().ToArray().ForEach(data =>
                            {
                                data.archived = 1;
                                conn.Update<Document>(data);
                            });
                        }

                        SweetAlert.Show("Success", $"User {user.Fullname} was successfully deleted.", SweetAlertButton.OK, SweetAlertImage.SUCCESS);
                    }
                }
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(DeleteAccount));
            }
        }
        void _timer_Tick(object sender, EventArgs e)
        {
            TimeZoneInfo timeZone = ViewModelLocator.Instance.NonClientAreaVM.SelectedTimezone;
            DateAndTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone).ToString("F");

            OnPropertyChanged(nameof(StartIndex));
            OnPropertyChanged(nameof(EndIndex));
            OnPropertyChanged(nameof(TotalEntries));
            OnPropertyChanged(nameof(MaxPage));
            OnPropertyChanged(nameof(DataSourceProduction));
        }
        public void SetCompletionTime()
        {
            if (Target > 0 && !string.IsNullOrEmpty(DateAndTime))
            {
                DateTime today = DateTime.Parse(DateAndTime);
                if (Information.IsDate(End) && today.TimeOfDay.Hours < DateTime.Parse(End).AddHours(3).TimeOfDay.Hours)
                {
                    double billedTimeInDouble = (((9 * 60 * 60) / Target) * BilledCount) / (24 * 60 * 60);
                    if (billedTimeInDouble > 0)
                    {
                        DateTime billedTime = DateTime.FromOADate(billedTimeInDouble);
                        TimeSpan remainingTime = DateTime.Parse(ProductionHrs).Subtract(billedTime);
                        string convertTo12 = Convert212Hours(remainingTime);
                        DateTime resultTimeSpan = today.Add(TimeSpan.Parse(convertTo12));
                        CompletionTime = resultTimeSpan.ToString("h:mm:ss tt").ToUpper();
                        SetCompletionForeground(resultTimeSpan);
                        return;
                    }
                    CompletionTime = "00:00:00";
                }
            }
        }
        string Convert212Hours(TimeSpan remainingTime)
        {
            if (remainingTime.Hours > 0 && ForBilled > 0)
            {
                return DateTime.Parse(remainingTime.ToString("h\\:mm\\:ss")).ToString("h:mm:ss");
            }
            else
            {
                return remainingTime.ToString("h\\:mm\\:ss");
            }
        }
        void SetCompletionForeground(DateTime resultTimeSpan)
        {

            if (resultTimeSpan.TimeOfDay > DateTime.Parse(End).TimeOfDay)
            {
                CompletionForeground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                CompletionForeground = new SolidColorBrush(Colors.White);
            }
        }
        Brush SetForeground(int i)
        {
            SolidColorBrush colorBrush = new SolidColorBrush();
            try
            {
                if (i == 0)
                {
                    colorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(ColorsEnum.Danger)));
                }
                else
                {
                    colorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(ColorHelper.GetColor(ColorsEnum.Success)));
                }
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(SetForeground));
            }

            return colorBrush;
        }
        TimeSpan ComputeTime(IEnumerable<dynamic> activities)
        {
            TimeSpan ts = new TimeSpan();
            try
            {
                activities.ToList().ToArray().ForEach(row =>
                {
                    if (row.end_time != DateTime.MinValue)
                    {
                        ts += row.end_time.Subtract(row.start_time);
                    }
                });
            }
            catch (Exception ex)
            {
                logService.ErrorLogToFile(ex.Message, ex.StackTrace, nameof(ComputeTime));
            }

            return ts;

        }
        TimeSpan ComputeTotalActualTime(IEnumerable<TimeInfo> times)
        {
            TimeSpan ts = TimeSpan.Zero;
            var invalid_words = new List<string> { "BREAK", "IDLE", "ONGOING" };

            times.ToArray().ToList().Select((item, index) => new { Item = item, Index = index }).ToList().ForEach(t =>
            {
                if (t.Item.start_time != DateTime.MinValue && t.Item.end_time != DateTime.MinValue)
                {
                    TimeSpan ElapsedTimeSTE = t.Item.end_time.Subtract(t.Item.start_time);
                    TimeSpan ElapsedTimeSTS = TimeSpan.Zero;

                    if (t.Index != times.Count() - 1)
                    {
                        var advance_stat = times.ToList()[t.Index + 1].Status.ToUpper();
                        if (!invalid_words.Any(iv => iv == advance_stat) && !invalid_words.Any(iv => iv == t.Item.Status.ToUpper()))
                        {
                            ElapsedTimeSTS = (times.ToList()[t.Index + 1]).start_time.Subtract(t.Item.start_time);
                        }
                        else ElapsedTimeSTS = ElapsedTimeSTE;
                    }
                    else ElapsedTimeSTS = ElapsedTimeSTE;

                    if (!invalid_words.Any(iv => iv == t.Item.Status.ToUpper()))
                    {
                        ts = ts.Add(ElapsedTimeSTS);
                    }
                }
            });

            return ts;
        }

    }

}