using NotiPetApp.Helpers;
using NotiPetApp.ViewModels;
using NotiPetApp.Views;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace NotiPetApp.Controls
{
    public class TabItemReactive<TViewModel>:ContentViewBase<TViewModel>,IActivatorView
    where TViewModel:BaseViewModel
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(ContentViewBase<TViewModel>), defaultValue: string.Empty);
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
      
        [Reactive] public bool IsSelected { get; private set; }
        public void SetSelected()
        {
            IsSelected = !IsSelected;

        }

        protected TabItemReactive()
        {
            BindingContext = CreateHelper.CreateViewModel<TViewModel>();
        }
    }

    public interface IActivatorView
    {
        public bool IsSelected { get; }
        public void SetSelected();
    }
}