using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using NotiPetApp.Properties;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;

namespace NotiPetApp.ViewModels
{
    public class OnBoardingViewModel : BaseViewModel
    {
        private int _position;

        private void SetSkipButtonText(string skipButtonText)
                => SkipButtonText = skipButtonText;
        public OnBoardingViewModel(INavigationService navigationService, IPageDialogService dialogPage) : base(navigationService, dialogPage)
        {
            SetSkipButtonText(AppResources.Next);
            InitializeOnBoarding();
            InitializeSkipCommand();
            ExitCommand = ReactiveCommand.CreateFromTask(ExitOnBoarding);
        }
        private void InitializeOnBoarding()
        {
            Items = new ObservableCollection<OnboardingModel>
            {
                new()
                {
                    Content = AppResources.OnBoarding1,
                    Image = "OnBoarding1.png"
                },
                new()
                {
                    Content = AppResources.OnBoarding2,
                    Image = "OnBoarding2.png"
                },
                new()
                {
                    Content = AppResources.OnBoarding3,
                    Image = "OnBoarding3.png"
                }
            };
        }

        private void InitializeSkipCommand()
        {
            SkipCommand = ReactiveCommand.CreateFromTask( () =>
            {
                if (LastPositionReached())
                {
                    return ExitOnBoarding();
                }
                else
                {
                    return MoveToNextPosition();
                }
            });
        }

        private  Task ExitOnBoarding()
        {
            Settings.ShowOnBoarding = false;
           return NavigationService.NavigateAsync(ConstantUri.Login);
        }
        private Task MoveToNextPosition()
        {
            var nextPosition = ++Position;
            Position = nextPosition;
            return Task.CompletedTask;
        }

        private bool LastPositionReached()
            => Position == Items.Count - 1;

        [Reactive] public ObservableCollection<OnboardingModel> Items { get; set; }

        [Reactive] public string SkipButtonText { get; set; }

        [Reactive] public int Position
        {
            get => _position;
            set
            {
                _position = value;
                UpdateSkipButtonText();
            }
        }

        private void UpdateSkipButtonText()
        {
            if (LastPositionReached())
            {
                SetSkipButtonText(AppResources.Start);
            }
            else
            {
                SetSkipButtonText(AppResources.Next);
            }
        }

        public ReactiveCommand<Unit,Unit> SkipCommand { get; private set; }
        public ReactiveCommand<Unit,Unit> ExitCommand { get; private set; }


    }
}