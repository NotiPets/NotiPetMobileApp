
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using NotiPetApp.Properties;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.CommunityToolkit.Helpers;

namespace NotiPetApp.ViewModels
{
    public class SettingsViewModel:BaseViewModel
    {
        public List<Language> Languages { get; set; }
        [Reactive] public Language SelectedLanguage { get; set; }
        public SettingsViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b,token) =>   NavigationService.GoBackAsync());
            Languages = new List<Language>()
            {
                new ()
                {
                    Name = AppResources.English,
                    Code = "en"
                },
                new ()
                {
                    Name = AppResources.Spanish,
                    Code = "es"
                },
            };
            SelectedLanguage = Languages.FirstOrDefault(x => x.Code.Contains(Settings.Language) );
            this.WhenAnyValue(x => x.SelectedLanguage)
                .Skip(1)
                .Where(x => x!=null && ! string.IsNullOrEmpty(x.Code))
                .Select(x => x)
                .Subscribe(x =>
                {
                    Settings.Language = x.Code;
                    LocalizationResourceManager.Current.CurrentCulture = CultureInfo.GetCultureInfo(x.Code);
                });
        }

        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; }
    }
}