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
using ReactiveUI.Fody.Helpers;

namespace NotiPetApp.ViewModels
{
    public class HelpPageViewModel : BaseViewModel,IInitialize
    {
        private readonly TicketService _ticketService;
        [Reactive] public string Title { get; set; }
        [Reactive]public string Comment { get; set; }
        public User User { get; set; }
        public HelpPageViewModel(INavigationService navigationService, IPageDialogService dialogPage,TicketService ticketService ) : base(navigationService, dialogPage)
        {
            _ticketService = ticketService;
            var canExecute =  this.WhenAnyValue(x => x.Comment).Select(x=>!string.IsNullOrEmpty(x));
            NavigateGoBackCommand = ReactiveCommand.CreateFromTask<Unit>((b, token) => NavigationService.GoBackAsync());
            SendCommand = ReactiveCommand.CreateFromObservable(SaveTicket,canExecute:canExecute);
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