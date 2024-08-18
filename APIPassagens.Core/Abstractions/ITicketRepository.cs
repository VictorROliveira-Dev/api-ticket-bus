using APIBusService.Core.Entities;
using System.Net.Sockets;

namespace APIBusService.Core.Abstractions;

public interface ITicketRepository
{
    Task<Ticket> GetTicketById(int id);
    Task<IEnumerable<Ticket>> GetAllTickets();
    Task<Ticket> AddTicket(Ticket ticket);
    Task<Ticket> UpdateTicket(Ticket ticket, int id);
    Task<bool> DeleteTicket(int id);
}
