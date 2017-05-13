using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MedicalInformationSystem.WpfProject.Converters
{
    public abstract class BooleanConverter<T> : IValueConverter
    {
        private readonly T _trueValue;
        private readonly T _falseValue;

        protected BooleanConverter(T trueValue, T falseValue)
        {
            _trueValue = trueValue;
            _falseValue = falseValue;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool)value ? _trueValue : _falseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && Object.Equals(value, _trueValue);
        }
    }
}
