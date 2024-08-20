using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Tools.Extension;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    public class Pagination<T> : ViewModelBase, INotifyPropertyChanged
    {
        private ObservableCollection<T> _dataSourceProduction;
        public IEnumerable<T> DataSourceProduction 
        {
            get => _dataSourceProduction;
        }
        private ObservableCollection<T> _totalList;
        public  IEnumerable<T> TotalList => _totalList;
        private int _pageSize;
        public int PageSize
        {
            get => _pageSize;
        }
        private int _startIndex;
        public int StartIndex => _startIndex;
        private int _endIndex;
        public int EndIndex
        {
            get => _endIndex;
        }
        private int _totalentries;
        public int TotalEntries
        {
            get => _totalentries;
        }
        private int _pageIndex;
        public int PageIndex
        {
            get => _pageIndex;
        }
        private int _maxPage;
        public int MaxPage
        {
            get => _maxPage;
        }
        public RelayCommand<FunctionEventArgs<int>> PageUpdatedCmd => new RelayCommand<FunctionEventArgs<int>>(PageUpdated);
       
        public Pagination()
        {
            _dataSourceProduction = new ObservableCollection<T>();
            _totalList = new ObservableCollection<T>();
            _pageIndex = 0;
            _startIndex = 0;
            _endIndex = 0;
            _totalentries = 0;
            _maxPage = 0;
            _pageSize = 0;
        }
        private void PageUpdated(FunctionEventArgs<int> info)
        {
            _dataSourceProduction.Clear();
            _dataSourceProduction?.AddRange(this.TotalList.Skip((info.Info - 1) * this._pageSize).Take(this._pageSize).ToList());
            _startIndex = (this._pageIndex - 1) * this._pageSize + 1;
            SetEndIndex();
        }
        public void  SetEndIndex()
        {
            var end = this._pageIndex * this._pageSize;
            _endIndex = (end > this.TotalList.Count()) ? this.TotalList.Count() : end;
        }
        public void AddQueue(T any, int page_index)
        {
            _totalList.Add(any);
            UpdateList(page_index);
        }
        public void RemoveQueue(T any, int page_index)
        {
            _totalList.Remove(any);
            UpdateList(page_index);
        }
        private void UpdateList(int page_index)
        {
            _dataSourceProduction.Clear();
            _dataSourceProduction?.AddRange(this.TotalList.Skip((page_index - 1) * this._pageSize).Take(this._pageSize).ToList());
            _startIndex = (this._pageIndex - 1) * this._pageSize + 1;
            SetEndIndex();
        }
        public void SetPaginationInfo(int start_index, int total_entries, int max_page) 
        {
            _startIndex = start_index;
            SetEndIndex();
            _totalentries = total_entries;
            _maxPage = max_page;
        }
        public void SetPageIndex(int page_index)
        {
            _pageIndex = page_index;
        }
        public void SetPageSize(int page_size)
        {
            if (_pageSize != page_size)
            {
                _pageSize = page_size;
                _dataSourceProduction.Clear();
                _dataSourceProduction?.AddRange(TotalList.Take(_pageSize).ToList());
                _startIndex = (this._pageIndex - 1) * this._pageSize + 1;
                SetEndIndex();
            }           
        }
        public void RefreshPageData()
        {
            _dataSourceProduction.Clear();
            _dataSourceProduction?.AddRange(TotalList.Take(_pageSize).ToList());
        }
        public void Clear()
        {
            _dataSourceProduction.Clear();
            _totalList.Clear();
            _startIndex = 0;
            _endIndex = 0;
            _totalentries = 0;
            _maxPage = 0;
            _pageSize = 0;
        }
    }
}
