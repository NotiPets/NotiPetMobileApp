using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using NotiPet.Data.Services;
using NotiPet.Domain.Models;
using NotiPetApp.Helpers;
using Prism.Navigation;
using Prism.Services;
using ReactiveUI;

namespace NotiPetApp.ViewModels
{
    public class HelpPageViewModel : BaseViewModel,IInitialize
    {
        private readonly TicketService _ticketService;
        public string Title { get; set; }
        public string Comment { get; set; }
        public User User { get; set; }
        public HelpPageViewModel(INavigationService navigationService, IPageDialogService dialogPage,TicketService ticketService ) : base(navigationService, dialogPage)
        {
            _ticketService = ticketService;
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            SendCommand = ReactiveCommand.CreateFromObservable(SaveTicket);
            SendCommand.InvokeCommand(NavigateGoBackCommand);
        }

        IObservable<Unit> SaveTicket()
        {
            return _ticketService.SaveTicket(new Ticket(Title,Comment,User.FullName,User.Phone,User.Email))
                .Select(x=>Unit.Default);
        }
        public ReactiveCommand<Unit,Unit> SendCommand { get; set; }
        public ReactiveCommand<Unit,Unit> NavigateGoBackCommand { get; set; }
        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(ParameterConstant.User))
            {
                User = parameters[ParameterConstant.User] as User;
            }
        }
    }
}