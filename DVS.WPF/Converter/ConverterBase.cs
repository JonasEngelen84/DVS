using System.Globalization;
using System.Windows.Data;

namespace DVS.WPF.Converter
{
    //TODO: Summary von ConverterBase
    public abstract class ConverterBase : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
