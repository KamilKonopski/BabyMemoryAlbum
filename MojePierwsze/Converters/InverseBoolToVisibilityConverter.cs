using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MojePierwsze.Converters
{
    public class InverseBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = false;
            if (value is bool) b = (bool)value;
            return b ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility v) return v != Visibility.Visible;
            return true;
        }
    }
}