using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using DynamicData;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using NotiPetApp.Services;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace NotiPetApp.ViewModels
{
    public class VeterinaryDetailViewModel : BaseViewModel,IInitialize,INavigatedAware
    {
        private readonly IVeterinaryService _veterinaryService;
        private readonly IRatingsService _ratingsService;
        private readonly IDeviceUtils _deviceUtils;
        public Veterinary Veterinary => _veterinary?.Value;
        private ObservableAsPropertyHelper<Veterinary> _veterinary;
        private int _id;
        private ReadOnlyObservableCollection<Review> _reviews;
        public ReadOnlyObservableCollection<Review> Review=>_reviews;
       [Reactive] public List<Pin> Pins { get; set; }
       public string Distance { get; set; }
        public VeterinaryDetailViewModel(INavigationService navigationService, IPageDialogService dialogPage,IVeterinaryService veterinaryService,
            IRatingsService ratingsService,ISchedulerProvider schedulerProvider,IDeviceUtils deviceUtils) : base(navigationService, dialogPage)
        {
            _veterinaryService = veterinaryService;
            _ratingsService = ratingsService;
            _deviceUtils = deviceUtils;
            _ratingsService.ReviewsSource.Connect()
                .ObserveOn(schedulerProvider.MainThread)
                .Bind(out _reviews)
                .DisposeMany()
                .Subscribe()
                .DisposeWith(Subscriptions);
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            CreateReviewsCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.NavigateAsync(ConstantUri.CreateReviewsPage, new NavigationParameters()
            {
                {ParameterConstant.VeterinaryId,_id}
            }));
            this.WhenAnyValue(x => x.Veterinary)
                .WhereNotNull()
                .InvokeCommand(new Command(LoadPins));
        }

        public ReactiveCommand<Unit, Unit> CreateReviewsCommand { get; set; }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get;  }


        protected override IObservable<Unit> ExecuteInitialize()
        {
            return Observable.Create<Unit>((observer) =>
            {
                var disposable = new CompositeDisposable();
                var getData = _veterinaryService.GetVeterinary(_id);
                _veterinary = getData.ToProperty(this, x => x.Veterinary);
                getData
                    .Select(x => Unit.Default)
                    .Subscribe()
                    .DisposeWith(disposable);
                _ratingsService.GetReviewsByBusinessId(_id)
                    .Select(e => Unit.Default)
                    .Subscribe(observer)
                    .DisposeWith(disposable);
               
                return disposable;
            });
        }

        void LoadPins()
        {
            if(Veterinary==null)
                return;
            var position = new Position(Veterinary.Latitude, Veterinary.Longitude);
            Pins = new List<Pin>
            {
                new Pin()
                {
                    Position = position,
                    Label = Veterinary.Name,
                    Address = Veterinary.FullAddress,
                    Type = PinType.Place
                }
            };
            _deviceUtils.GetCurrentLocation();
            Distance = $"{Math.Round(_deviceUtils.CalculateDistance(position),2)}";
        }
        public void Initialize(INavigationParameters parameters)
        {
            _id = (int)parameters[ParameterConstant.VeterinaryId];
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
          
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.GetNavigationMode() == NavigationMode.Back)
            {
                InitializeCommand.Execute()
                    .Subscribe()
                    .DisposeWith(Subscriptions);
            }
        }
    }
}