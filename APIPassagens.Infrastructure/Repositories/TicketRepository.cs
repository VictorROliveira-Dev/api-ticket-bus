using APIBusService.Core.Abstractions;
using APIBusService.Core.Entities;
using APIBusService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace APIBusService.Infrastructure.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket> AddTicket(Ticket ticket)
    {
        if (ticket == null)
        {
            throw new ArgumentException(nameof(ticket));
        }

        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();

        return ticket;
    }

    public async Task<bool> DeleteTicket(int id)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        
        if (ticket == null)
        {
            throw new InvalidOperationException("Ticket id not found.");
        }

        _context.Tickets.Remove(ticket);

        return true;
    }

    public async Task<IEnumerable<Ticket>> GetAllTickets()
    {
        var ticketList = await _context.Tickets.ToListAsync();
        return ticketList ?? Enumerable.Empty<Ticket>();
    }

    public async Task<Ticket> GetTicketById(int id)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticket == null)
        {
            throw new InvalidOperationException("Ticket id not found.");
        }

        return ticket;
    }

    public async Task<Ticket> UpdateTicket(Ticket ticket, int id)
    {
        var ticketId = await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);

        if (ticketId == null)
        {
            throw new InvalidOperationException("Ticket id not found");
        }

        ticketId.DepartureDate = ticket.DepartureDate;
        ticketId.ReturnDate = ticket.ReturnDate;

        _context.Update(ticketId);
        await _context.SaveChangesAsync();

        return ticketId;
    }
}
