using System.Collections.ObjectModel;
using System.Windows.Input;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace NotiPetApp.ViewModels
{
    public class OnBoardingViewModel :  MvvmHelpers.BaseViewModel
    {
        protected static INavigationService NavigationService { get; set; }

        private ObservableCollection<OnboardingModel> items;
        private int position;
        private string skipButtonText;
        public OnBoardingViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
            SetSkipButtonText("Siguiente");
            InitializeOnBoarding();
            InitializeSkipCommand();
            ExitCommand = new Command(ExitOnBoarding);
        }

        private void SetSkipButtonText(string skipButtonText)
                => SkipButtonText = skipButtonText;

        private void InitializeOnBoarding()
        {
            Items = new ObservableCollection<OnboardingModel>
            {
                new OnboardingModel
                {
                    Content = "Donde tu mascota estará segura!",
                    Image = "OnBoardingComplete1.png"
                },
                new OnboardingModel
                {
                    Content = "Con seguimiento a tiempo real del estado de tus mascotas.",
                    Image = "OnBoardingComplete2.png"
                },
                new OnboardingModel
                {
                    Content = "Servicio de atención de emergencias para sus mascotas.",
                    Image = "OnBoardingComplete3.png"
                }
            };
        }

        private void InitializeSkipCommand()
        {
            SkipCommand = new Command(() =>
            {
                if (LastPositionReached())
                {
                    ExitOnBoarding();
                }
                else
                {
                    MoveToNextPosition();
                }
            });
        }

        private static void ExitOnBoarding()
            => NavigationService.NavigateAsync(ConstantUri.SocialNetworkAuthentication);

        private void MoveToNextPosition()
        {
            var nextPosition = ++Position;
            Position = nextPosition;
        }

        private bool LastPositionReached()
            => Position == Items.Count - 1;

        public ObservableCollection<OnboardingModel> Items
        {
            get => items;
            set => SetProperty(ref items, value);
        }

        public string SkipButtonText
        {
            get => skipButtonText;
            set => SetProperty(ref skipButtonText, value);
        }

        public int Position
        {
            get => position;
            set
            {
                if (SetProperty(ref position, value))
                {
                    UpdateSkipButtonText();
                }
            }
        }

        private void UpdateSkipButtonText()
        {
            if (LastPositionReached())
            {
                SetSkipButtonText("Empezemos");
            }
            else
            {
                SetSkipButtonText("Siguiente");
            }
        }

        public ICommand SkipCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }

    }
}