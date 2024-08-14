using APIBusService.Core.Abstractions;
using APIBusService.Core.CQRS.Queries.TicketQueries;
using APIBusService.Core.DTOs;
using MediatR;

namespace APIBusServiceAPI.Handlers.TicketHandlers;

public class GetAllTicketsQueryHandler : IRequestHandler<GetAllTicketsQuery, IEnumerable<TicketDto>>
{
    private readonly ITicketRepository _ticketRepository;

    public GetAllTicketsQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<IEnumerable<TicketDto>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
    {
        var tickets = await _ticketRepository.GetAllTickets();

        return tickets.Select(t => new TicketDto
        {
            Id = t.Id,
            TicketCode = t.TicketCode,
            DepartureDate = t.DepartureDate,
            ReturnDate = t.ReturnDate,
            UserId = t.UserId,
        });
    }
}
