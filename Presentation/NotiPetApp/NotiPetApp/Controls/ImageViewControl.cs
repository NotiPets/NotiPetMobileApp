using Xamarin.Forms;

namespace NotiPetApp.Controls
{
    public class ImageViewControl:ContentView
    {
        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ImageViewControl));

        public ImageSource ImageSource
        {
            get => (ImageSource) GetValue(ImageSourceProperty) ; 
            set=>SetValue(ImageSourceProperty,value); 
        }
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text), typeof(string), typeof(ImageViewControl));

        public string Text
        {
            get => (string) GetValue(TextProperty) ; 
            set=>SetValue(TextProperty,value); 
        }
    }
}