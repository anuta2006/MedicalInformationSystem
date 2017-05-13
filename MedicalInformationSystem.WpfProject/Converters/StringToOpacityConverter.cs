using System;
using System.Globalization;
using System.Windows.Data;

namespace MedicalInformationSystem.WpfProject.Converters
{
    public class StringToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = (string)value;

            return String.IsNullOrWhiteSpace(str) ? 0 : 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
