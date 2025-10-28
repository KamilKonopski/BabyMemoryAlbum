using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace MojePierwsze.Converters
{
    public class BoolToCursorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isEditing)
                return isEditing ? Cursors.Hand : Cursors.Arrow;
            return Cursors.Arrow;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}