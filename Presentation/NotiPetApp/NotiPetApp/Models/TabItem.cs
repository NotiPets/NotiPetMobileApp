using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using DynamicData.Binding;
using DynamicData.PLinq;
using NotiPetApp.Helpers;
using NotiPetApp.ViewModels;
using NotiPetApp.Views;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.XamForms;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace NotiPetApp.Models
{
    public class TabItemReactive:ContentView


    {
        [Reactive] public bool IsSelected { get; private set; }
        public string Text { get; private set; }
        public BaseViewModel ViewModel { get; set; }

        public void SetSelected(bool value)
        {
            IsSelected = value;

        }

        public  static TabItemReactive CreateReactive<TView,TViewModel>(string text) 
             where TViewModel:BaseViewModel
            where TView: ContentView
        {
            var reactive = new TabItemReactive
            {
                Text = text,
                ViewModel = CreateHelper.CreateViewModel<TViewModel>(),
                Content = CreateHelper.CreateView<TView>()
            };
            reactive.Content.BindingContext = reactive.ViewModel;
            return reactive;
        }
    }
}