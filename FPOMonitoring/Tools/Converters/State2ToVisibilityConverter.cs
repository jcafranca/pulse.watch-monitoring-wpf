using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace FPOMonitoring.Tools.Converters
{
    internal class State2ToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str)
            {
           
                switch (str)
                {
                    case "DISCONNECTED":
                        return Visibility.Collapsed;
                    case "CONNECTED":
                        return Visibility.Collapsed;
                }
            }
            return Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
