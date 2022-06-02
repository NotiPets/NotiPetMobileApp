using System.Collections;
using Xamarin.Forms;

namespace NotiPetApp.Controls
{
    public class FlexListView:FlexLayout
    {
        public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(FlexListView),propertyChanged:
                (bindable, value, newValue) =>
                {
                    BindableLayout.SetItemsSource(bindable,newValue as IEnumerable); ;  
                });

        public IEnumerable ItemSource
        {
            get => (IEnumerable) GetValue(ItemSourceProperty) ; 
            set=>SetValue(ItemSourceProperty,value); 
        }
    }
}