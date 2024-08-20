using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPOMonitoring.Data.Class
{
    public class CheckBox : ViewModelBase, INotifyPropertyChanged
    {

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value);
                OnPropertyChanged(nameof(Name));
            }
        }
        private int _key;
        public int Key
        {
            get => _key;
            set
            {
                Set(ref _key, value);
                OnPropertyChanged(nameof(Key));
            }
        }
        private bool _isChecked;
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                Set(ref _isChecked, value);
                OnPropertyChanged(nameof(IsChecked));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
