using System;
using System.Globalization;
using Xamarin.Forms;

namespace NotiPetApp.Converters
{
    public class EqualConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value}" == $"{parameter}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}