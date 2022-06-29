using System;
using System.Reactive.Linq;
using AutoMapper;
using NotiPet.Data.Dtos;
using NotiPet.Domain.Models;
using NotiPet.Domain.Service;

namespace NotiPet.Data.Services
{
    public class TicketService:ITicketService
    {
        private readonly IMapper _mapper;
        private readonly ITicketServiceApi _ticketServiceApi;

        public TicketService(IMapper mapper,ITicketServiceApi ticketServiceApi)
        {
            _mapper = mapper;
            _ticketServiceApi = ticketServiceApi;
        }
        public IObservable<Ticket> SaveTicket(Ticket ticket)
        {
            return _ticketServiceApi.SaveTicket(_mapper.Map<TicketDto>(ticket)).Select(_mapper.Map<Ticket>);
        }
    }
}