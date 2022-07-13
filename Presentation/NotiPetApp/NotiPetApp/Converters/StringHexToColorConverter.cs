using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
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
        public string DefaultImage { get; set; } = "patita";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if ( string.IsNullOrEmpty($"{value}"))
                    return DefaultImage;
            
                var base64 = value.ToString();
                if (base64.StartsWith("http"))
                    return base64;
                var arrayByte = System.Convert.FromBase64String(base64);
                Stream stream = new MemoryStream(arrayByte);
                var imageSource = ImageSource.FromStream(()=>stream);
                return imageSource;
            }
            catch (Exception e)
            {
                return DefaultImage;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class Base64StringToStreamConverter:IValueConverter
    {
    
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                // convert string to stream
                var myByteArray = value as byte[];
                MemoryStream stream = new MemoryStream();
                stream.Write(myByteArray, 0, myByteArray.Length);
                return stream;
            }
            catch (Exception e)
            {
                return value;
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    public class SizeListToHeightRequest:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var count = (int) value;
            return count * 40;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}