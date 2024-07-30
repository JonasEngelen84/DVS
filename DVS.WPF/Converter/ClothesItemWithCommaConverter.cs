using System.Globalization;

namespace DVS.WPF.Converter
{
    public class ClothesItemWithCommaConverter : ConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString() + ",";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //TODO: throw new NotImplementedException(); aus ConvertBack entfernen
            throw new NotImplementedException();
        }
    }
}
