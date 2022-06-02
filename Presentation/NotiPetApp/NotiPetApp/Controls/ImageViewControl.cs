using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace NotiPetApp.Controls
{
    public class ImageViewControl:PancakeView
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
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ImageViewControl));

        public ICommand Command
        {
            get => (ICommand) GetValue(CommandProperty) ; 
            set=>SetValue(CommandProperty,value); 
        }
    }
}