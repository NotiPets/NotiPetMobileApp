using System;
using System.Globalization;
using System.IO;
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
    public class Base64StringToImageSource:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var base64 = value.ToString();
            if (base64 == null)
                return "patita";
            var arrayByte = System.Convert.FromBase64String(base64);
            Stream stream = new MemoryStream(arrayByte);
            var imageSource = ImageSource.FromStream(()=>stream);
            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}