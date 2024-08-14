using APIBusService.Core.Abstractions;
using APIBusService.Core.CQRS.Commands.TicketCommands;
using APIBusService.Core.DTOs;
using MediatR;

namespace APIBusServiceAPI.Handlers.TicketHandlers;

public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketRepository;

    public DeleteTicketCommandHandler(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public async Task<TicketDto> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetTicketById(request.Id);

        if (ticket == null)
        {
            throw new Exception(nameof(ticket));
        }

        await _ticketRepository.DeleteTicket(request.Id);

        return new TicketDto { Id = request.Id };
    }
}
