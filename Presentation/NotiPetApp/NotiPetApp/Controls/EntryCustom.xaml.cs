using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Behaviors;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryCustom : PancakeView,IDisposable
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
        public static readonly BindableProperty TextColorProperty 
            = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(EntryCustom),defaultValue:Color.Gray);
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }
        public static readonly BindableProperty FontSizeProperty 
            = BindableProperty.Create(nameof(FontSize), typeof(double), typeof(EntryCustom),defaultValue:12d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize 
        {
            get { return (double)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }

        #endregion
        
        public static readonly BindableProperty LineColorProperty 
            = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(EntryCustom), defaultBindingMode: BindingMode.OneWay,defaultValue:Color.Gray,propertyChanged:
                (bindable, value, newValue) =>
                {
                    if (bindable is EntryCustom bd)
                    {
                        bd.Line.BackgroundColor = (Color)newValue;
                    }
                });

        public static readonly BindableProperty ActiveLineColorProperty 
            = BindableProperty.Create(nameof(ActiveLineColor), typeof(Color), typeof(EntryCustom), defaultBindingMode: BindingMode.OneWay,defaultValue:Color.Black);

        public Color LineColor
        {
            get { return (Color)GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }
        public Color ActiveLineColor
        {
            get { return (Color)GetValue(ActiveLineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public EntryCustom()
        {
            InitializeComponent();
      
            Observable.FromEventPattern<FocusEventArgs>(handler =>
            {
                EntryText.Focused += handler;
            }, handler =>
            {
                EntryText.Focused -= handler;
            })
                .Subscribe(x =>
                {
                    Line.BackgroundColor = ActiveLineColor ;
                    PlaceHolderLb.TextColor = ActiveLineColor;
                }, x => { })
                .DisposeWith(compositeDisposable);
            Observable.FromEventPattern<FocusEventArgs>(handler =>
                {
                    EntryText.Unfocused += handler;
                   
                }, handler =>
                {
                    EntryText.Unfocused -= handler;
                    
                })
                .Subscribe(x =>
                {
                    Line.BackgroundColor = LineColor;
                    PlaceHolderLb.TextColor = LineColor;
                }, x => { })
                .DisposeWith(compositeDisposable);
        }
        
        public void Dispose()
        {
            
            compositeDisposable?.Dispose();
        }
    }


}