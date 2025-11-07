using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace windows_pos_system.Converters
{
    public class EqualityToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Length < 2) return Visibility.Collapsed;
            var a = values[0]?.ToString() ?? string.Empty;
            var b = values[1]?.ToString() ?? string.Empty;
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
