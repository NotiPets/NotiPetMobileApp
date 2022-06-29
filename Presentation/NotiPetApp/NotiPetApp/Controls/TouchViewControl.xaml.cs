using System;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TouchViewControl : PancakeView
    {
        public static readonly BindableProperty CommandTextProperty = BindableProperty.Create(nameof(CommandText), typeof(string), typeof(TouchViewControl), defaultValue: string.Empty);
        public string CommandText
        {
            get => (string)GetValue(CommandTextProperty);
            set => SetValue(CommandTextProperty, value);
        }
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(TouchViewControl), Color.White);

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TouchViewControl), defaultBindingMode: BindingMode.OneTime);   
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(TouchViewControl),default);
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(TouchViewControl),16d);   
        
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(TouchViewControl), null);
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        public string FontFamily
        {
            get { return (string)GetValue(FontFamilyProperty); }
            set { SetValue(FontFamilyProperty, value); }
        }
        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        
        public event EventHandler<EventArgs> Tapped;

        // this should be wired to a Tapped Gesture Event on your XAML or Code behind.
        
        public TouchViewControl()
        {
            InitializeComponent();
        }

        protected void OnButtonTapped(object sender, EventArgs args)
        {

            if (Command!=null)
            {
                if (Command.CanExecute(CommandParameter))
                {
                    Command?.Execute(CommandParameter);
                }
            }
            else
            {
                Tapped?.Invoke(this, args);
            }
        
        }
    }
}