using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FPOMonitoring.Data.Class
{
    public class HourDetails : INotifyPropertyChanged
    {
        private int _index;
        public int index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged(nameof(index));
            }
        }
        private string _name;
        public string name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private double _billed;
        public double billed 
        {
            get => _billed;
            set
            {
                _billed = value;
                OnPropertyChanged(nameof(billed));
            }
        }

        private double _reject;
        public double reject
        {
            get => _reject;
            set
            {
                _reject = value;
                OnPropertyChanged(nameof(reject));
            }
        }

        private double _query;
        public double query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged(nameof(query));
            }
        }

        private double _total_proccessed;
        public double total_processed
        {
            get => _total_proccessed;
            set
            {
                _total_proccessed = value;
                OnPropertyChanged(nameof(total_processed));
            }
        }

        private Brush _foreground_billed;
        public Brush foreground_billed
        {
            get => _foreground_billed;
            set
            {
                _foreground_billed = value;
                OnPropertyChanged(nameof(foreground_billed));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
