﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NotiPetApp.ViewModels;
using NotiPetApp.Views.MenuPages;
using Prism.Ioc;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NotiPetApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabMenuPage 
    {
        private readonly IContainerProvider _containerProvider;

        public TabMenuPage(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;
            InitializeComponent();

        }

        protected override void OnAppearing()
        {
            if(TabMenu.SelectedIndex == -1) return;
            
            var tab = TabMenu.TabItems[TabMenu.SelectedIndex] ;
            if (tab.CurrentContent?.BindingContext is ProfileViewModel profileViewModel)
            {
                profileViewModel.InitializeCommand
                    .Execute().Subscribe();

            }
            base.OnAppearing();
        }
    }
}