using FPOMonitoring.Data.Class;
using FPOMonitoring.Services;
using FPOMonitoring.Tools.Helpers;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FPOMonitoring.ViewModel
{
    public class QueryViewModel : ViewModelBase, INotifyPropertyChanged
    {

        private readonly LogService logService = new LogService();
        private readonly MySQLService mySQLService = new MySQLService();

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private Document doc;
        public Document Doc
        {
            get => doc;
            set
            {
                Set(ref doc, value);
                OnPropertyChanged(nameof(Doc));
            }
        }
        public QueryViewModel()
        {
        }

        private ObservableCollection<ChatInfo> _chatInfos;
        public ObservableCollection<ChatInfo> ChatInfos
        {
            get => _chatInfos;
            set
            {
                Set(ref _chatInfos, value);
                OnPropertyChanged(nameof(ChatInfos));
            }
        }

        private Query _pending_query;
        public Query PendingQuery
        {
            get => _pending_query;
        }


        public void FetchQueryData()
        {
            ChatInfos = new ObservableCollection<ChatInfo>();
            Doc.Queries?.ToArray().ToList().ForEach(queryProperty =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {

                    ChatInfos.Add(new ChatInfo
                    {
                        Message = queryProperty.query.Replace(",", "\r"),
                        ChatType = HandyControl.Data.ChatRoleType.Receiver,
                        HeaderAlignment = HorizontalAlignment.Left,
                        DateTime = queryProperty.time_start,
                        Header = $"{queryProperty.ProcessedByName}, {queryProperty.time_start.ToString("hh:mm tt")}"
                    });

                    if (!String.IsNullOrEmpty(queryProperty.answer))
                    {
                        ChatInfos.Add(new ChatInfo
                        {
                            Message = queryProperty.answer,
                            ChatType = HandyControl.Data.ChatRoleType.Sender,
                            HeaderAlignment = HorizontalAlignment.Right,
                            DateTime = queryProperty.time_end,
                            Header = $"{queryProperty.AnsweredByName}, {queryProperty.time_end?.ToString("hh:mm tt")}"
                        });
                    }

                });
            });

            _pending_query = Doc.Queries.ToArray().ToList().OrderByDescending(item => item.id).ToList().FirstOrDefault();

        }
    }
}
