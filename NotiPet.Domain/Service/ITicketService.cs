using System;
using NotiPet.Domain.Models;

namespace NotiPet.Domain.Service
{
    public interface ITicketService
    {
        IObservable<Ticket> SaveTicket(Ticket ticket);
    }
}