using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class VeterinaryDetailViewModel : BaseViewModel,IInitialize
    {
        private readonly IVeterinaryService _veterinaryService;
        public Veterinary Veterinary => _veterinary?.Value;
        private ObservableAsPropertyHelper<Veterinary> _veterinary;
        private int _id;
        public VeterinaryDetailViewModel(INavigationService navigationService, IPageDialogService dialogPage,IVeterinaryService veterinaryService) : base(navigationService, dialogPage)
        {
            _veterinaryService = veterinaryService;
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
        }

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
                    .Subscribe(observer)
                    .DisposeWith(disposable);
                return disposable;
            });
        }

        public void Initialize(INavigationParameters parameters)
        {
            _id = (int)parameters[ParameterConstant.VeterinaryId];
        }
    }
}