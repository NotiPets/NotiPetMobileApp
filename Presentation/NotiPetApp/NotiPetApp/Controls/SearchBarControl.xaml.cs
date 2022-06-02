using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBarControl : PancakeView
    {
        public static readonly BindableProperty ReturnCommandProperty = BindableProperty.Create(nameof(ReturnCommand), typeof(ICommand), typeof(SearchBarControl), default(ICommand));

        public static readonly BindableProperty TextProperty
            = BindableProperty.Create(nameof(Text), typeof(string), typeof(SearchBarControl),
                propertyChanged: TextChangePropertyChanged,defaultBindingMode:BindingMode.TwoWay );
        public static readonly BindableProperty PlaceHolderProperty 
            = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(SearchBarControl));
        public static readonly BindableProperty FontFamilyProperty 
            = BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(SearchBarControl));

        public static readonly BindableProperty FontSizeProperty 
            = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(SearchBarControl),defaultValue:14D);
          public static readonly BindableProperty IconColorProperty 
            = BindableProperty.Create(nameof(IconColor), typeof(Color), 
                typeof(SearchBarControl),defaultValue:Color.Gray);
          public static readonly BindableProperty TextColorProperty 
              = BindableProperty.Create(nameof(TextColor), typeof(Color), 
                  typeof(SearchBarControl),defaultValue:Color.Gray);
        public static readonly BindableProperty GlyphCleanProperty 
            = BindableProperty.Create(nameof(GlyphClean), typeof(string), typeof(SearchBarControl),defaultBindingMode:BindingMode.TwoWay);
        public static readonly BindableProperty CleanCommandProperty 
            = BindableProperty.Create(nameof(CleanCommand), typeof(ICommand), typeof(SearchBarControl),defaultBindingMode:BindingMode.OneTime);

        public static readonly BindableProperty CleanPictureUrlSourceProperty 
            = BindableProperty.Create(nameof(CleanPictureUrlSource), typeof(ImageSource), typeof(SearchBarControl),defaultBindingMode:BindingMode.OneTime);
        

        public ImageSource CleanPictureUrlSource
        {
            get => (ImageSource)GetValue(CleanPictureUrlSourceProperty);
            set => SetValue(CleanPictureUrlSourceProperty, value);
        }
        public static readonly BindableProperty SearchPictureUrlSourceProperty 
            = BindableProperty.Create(nameof(SearchPictureUrlSource), typeof(ImageSource), typeof(SearchBarControl),defaultBindingMode:BindingMode.OneTime);
        

        public ImageSource SearchPictureUrlSource
        {
            get => (ImageSource)GetValue(SearchPictureUrlSourceProperty);
            set => SetValue(SearchPictureUrlSourceProperty, value);
        }


        public ICommand ReturnCommand
        {
            get => (ICommand)GetValue(ReturnCommandProperty);
            set => SetValue(ReturnCommandProperty, value);
        }

        
        public string Text
        {
            get=>(string)GetValue(TextProperty); 
            set=>SetValue(TextProperty,value);
        }
        public string FontFamily
        {
            get=>(string)GetValue(FontFamilyProperty); 
            set=>SetValue(FontFamilyProperty,value);
        }
        public string PlaceHolder
        {
            get=>(string)GetValue(PlaceHolderProperty); 
            set=>SetValue(PlaceHolderProperty,value);
        }
        public double FontSize
        {
            get=>(double)GetValue(FontSizeProperty); 
            set=>SetValue(FontSizeProperty,value);
        }   
        public Color IconColor
        {
            get=>(Color)GetValue(IconColorProperty); 
            set=>SetValue(IconColorProperty,value); 
        }
        public Color TextColor
        {
            get=>(Color)GetValue(TextColorProperty); 
            set=>SetValue(TextColorProperty,value); 
        }
        public string GlyphClean
        {
            get=>(string)GetValue(GlyphCleanProperty); 
            set=>SetValue(GlyphCleanProperty,value);
        }
        public ICommand CleanCommand
        {
            get=>(ICommand)GetValue(CleanCommandProperty); 
            set=>SetValue(CleanCommandProperty,value);
        }

        public static readonly BindableProperty HideCleanButtonProperty 
            = BindableProperty.Create(nameof(CleanCommand), typeof(bool), typeof(SearchBarControl),false);
        
        public bool HideCleanButton
        {
            get=>(bool)GetValue(HideCleanButtonProperty); 
            set=>SetValue(HideCleanButtonProperty,value);
        }
        private static void TextChangePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null)
            {
                var text = $"{newValue}";
                var control = bindable as SearchBarControl;
                if (string.IsNullOrEmpty(text))
                {
                    control.HideCleanButton = false;
                }
                else
                {
                    control.HideCleanButton = true;
                }
            }
        }
        public SearchBarControl()
        {
            CleanCommand = new Command(() => Text = string.Empty);
            InitializeComponent();
        }

    }
}