using System;
using System.Globalization;
using System.Windows.Data;

namespace MojePierwsze.Converters
{
    public class BoolToColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // true -> kolumna 1, false -> kolumna 0
            if (value is bool b && b) return 1;
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class BoolToOppositeColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // true -> kolumna 0, false -> kolumna 1
            if (value is bool b && b) return 0;
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}