using System;
using NotiPet.Data.Dtos;

namespace NotiPet.Data.Services
{
    public interface ITicketServiceApi
    {
        IObservable<TicketDto> SaveTicket(TicketDto ticket);
    }
}