using APIBusService.Core.Abstractions;
using APIBusService.Core.CQRS.Queries.TicketQueries;
using APIBusService.Core.DTOs;
using MediatR;

namespace APIBusServiceAPI.Handlers.TicketHandlers;

public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;

    public GetTicketByIdQueryHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketDto> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetTicketById(request.Id);

        if (ticket == null)
        {
            throw new Exception("Ticket not found");
        }

        return new TicketDto
        {
            Id = request.Id,
            TicketCode = ticket.TicketCode,
            DepartureDate = ticket.DepartureDate,
            ReturnDate = ticket.ReturnDate,
            UserId = ticket.UserId,
        };
    }
}
