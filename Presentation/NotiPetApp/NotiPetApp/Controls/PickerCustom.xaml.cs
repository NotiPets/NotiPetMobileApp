using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerCustom : IDisposable
    {
        private CompositeDisposable compositeDisposable = new CompositeDisposable();

        #region PLACEHOLDER

        public static readonly BindableProperty PlaceHolderAlignmentProperty 
            = BindableProperty.Create(nameof(PlaceHolderAlignment), typeof(TextAlignment), typeof(EntryCustom), defaultBindingMode: BindingMode.TwoWay,defaultValue:TextAlignment.Start);

        public TextAlignment PlaceHolderAlignment
        {
            get { return (TextAlignment)GetValue(PlaceHolderAlignmentProperty); }
            set { SetValue(PlaceHolderAlignmentProperty, value); }
        }
        public static readonly BindableProperty PlaceHolderProperty 
            = BindableProperty.Create(nameof(PlaceHolder), typeof(string), typeof(EntryCustom), defaultBindingMode: BindingMode.TwoWay);

        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public static readonly BindableProperty PlaceHolderSizeProperty 
            = BindableProperty.Create(nameof(PlaceHolderSize), typeof(double), typeof(EntryCustom), defaultBindingMode: BindingMode.OneWay,defaultValue:12d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double PlaceHolderSize
        {
            get => (double)GetValue(PlaceHolderSizeProperty);
            set => SetValue(PlaceHolderSizeProperty, value);
        }

        #endregion
        
        #region Text

        public static readonly BindableProperty TextAlignmentProperty 
            = BindableProperty.Create(nameof(TextAlignment), typeof(TextAlignment), typeof(EntryCustom),defaultValue: TextAlignment.Start);

        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }
        public static readonly BindableProperty TextProperty 
            = BindableProperty.Create(nameof(Text), typeof(string), typeof(EntryCustom), defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        
        
        public static readonly BindableProperty ValidationMessageProperty 
            = BindableProperty.Create(nameof(ValidationMessage), typeof(string), typeof(EntryCustom), defaultBindingMode: BindingMode.TwoWay);

        public string ValidationMessage
        {
            get =>  (string)GetValue(ValidationMessageProperty); 
            set => SetValue(ValidationMessageProperty, value);
        }
        public static readonly BindableProperty TextColorProperty 
            = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(EntryCustom),defaultValue:Color.Gray);
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly BindableProperty ValidationColorProperty 
            = BindableProperty.Create(nameof(ValidationColor), typeof(Color), typeof(EntryCustom),defaultValue:Color.Gray);
        public Color ValidationColor
        {
            get { return (Color)GetValue(ValidationColorProperty); }
            set { SetValue(ValidationColorProperty, value); }
        }
        public static readonly BindableProperty FontSizeProperty 
            = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(EntryCustom),defaultValue:12d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize 
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        public static readonly BindableProperty ItemSourcesProperty 
            = BindableProperty.Create(nameof(ItemSources), typeof(IEnumerable), typeof(EntryCustom));
        public IEnumerable ItemSources
        {
            get { return (IEnumerable)GetValue(ItemSourcesProperty); }
            set { SetValue(ItemSourcesProperty, value); }
        }
        public static readonly BindableProperty SelectedItemProperty 
            = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(EntryCustom));
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        public PickerCustom()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            compositeDisposable?.Dispose();
        }
    }
}