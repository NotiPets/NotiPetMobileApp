using System.Collections;
using Xamarin.Forms;

namespace NotiPetApp.Controls
{
    public class StackLayoutListView:StackLayout
    {
        public static readonly BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(StackLayoutListView),propertyChanged:
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