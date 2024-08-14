using APIBusService.Core.Abstractions;
using APIBusService.Core.Entities;

namespace APIBusService.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    public Task<Ticket> AddTicket(Ticket ticket)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTicket(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Ticket>> GetAllTickets()
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> GetTicketById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Ticket> UpdateTicket(Ticket ticket)
    {
        throw new NotImplementedException();
    }
}
