using APIBusService.Core.Abstractions;
using APIBusService.Core.CQRS.Commands.TicketCommands;
using APIBusService.Core.DTOs;
using MediatR;

namespace APIBusServiceAPI.Handlers.TicketHandlers;

public class UpdateTicketCommandHandler : IRequestHandler<UpdateTicketCommand, TicketDto>
{
    private readonly ITicketRepository _repository;

    public UpdateTicketCommandHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<TicketDto> Handle(UpdateTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetTicketById(request.Id);

        if (ticket == null)
        {
            throw new Exception(nameof(ticket));
        }

        ticket.DepartureDate = request.DepartureDate;
        ticket.ReturnDate = request.ReturnDate;

        await _repository.UpdateTicket(ticket);

        return new TicketDto
        {
            DepartureDate = ticket.DepartureDate,
            ReturnDate = ticket.ReturnDate,
        };
    }
}
