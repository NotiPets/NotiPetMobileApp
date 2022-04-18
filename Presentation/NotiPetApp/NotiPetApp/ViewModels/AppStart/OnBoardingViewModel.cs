using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using NotiPetApp.Helpers;
using NotiPetApp.Models;
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
            SetSkipButtonText("Siguiente");
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
                    Content = "Donde tu mascota estará segura!",
                    Image = "OnBoarding1.png"
                },
                new()
                {
                    Content = "Con seguimiento a tiempo real del estado de tus mascotas.",
                    Image = "OnBoarding2.png"
                },
                new()
                {
                    Content = "Servicio de atención de emergencias para sus mascotas.",
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
           return NavigationService.NavigateAsync(ConstantUri.SocialNetworkAuthentication);
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
                SetSkipButtonText("Empezemos");
            }
            else
            {
                SetSkipButtonText("Siguiente");
            }
        }

        public ReactiveCommand<Unit,Unit> SkipCommand { get; private set; }
        public ReactiveCommand<Unit,Unit> ExitCommand { get; private set; }


    }
}