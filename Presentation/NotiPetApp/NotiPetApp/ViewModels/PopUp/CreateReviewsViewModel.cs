using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels.PopUp
{
    public class CreateReviewsViewModel: BaseViewModel,IInitialize
    {
        private readonly IRatingsService _ratingsService;
        [Reactive]   public int VeterinaryId { get; set; }
        [Reactive]public string Comment { get; set; }
        [Reactive] public int ValueRating { get; set; }
        public CreateReviewsViewModel(INavigationService navigationService, IPageDialogService dialogPage,IRatingsService ratingsService) : base(navigationService, dialogPage)
        {
            _ratingsService = ratingsService;
          var canExecute =  this.WhenAnyValue(x => x.Comment).Select(x=>!string.IsNullOrEmpty(x));
            CreateReviewsCommand = ReactiveCommand.CreateFromObservable(CreateReview,canExecute:canExecute);
            
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            CreateReviewsCommand.InvokeCommand(NavigateGoBackCommand);
           
        }

        public ReactiveCommand<Unit, Unit> NavigateGoBackCommand { get; set; }

        public ReactiveCommand<Unit, Unit> CreateReviewsCommand { get; set; }

        IObservable<Unit> CreateReview()
        {
            return _ratingsService.CreateReviews(new Review(Guid.NewGuid().ToString(), VeterinaryId, null,
                Settings.UserId, null, Comment, ValueRating)).Select(x=>Unit.Default);
        }
        public void Initialize(INavigationParameters parameters)
        {
            VeterinaryId = (int) parameters[ParameterConstant.VeterinaryId];
        }
    }
}