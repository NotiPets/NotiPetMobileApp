using System;
using System.Globalization;
using Xamarin.Forms;

namespace NotiPetApp.Converters
{
    public class StringHexToColorConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
             =>Color.FromHex(value.ToString());

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}