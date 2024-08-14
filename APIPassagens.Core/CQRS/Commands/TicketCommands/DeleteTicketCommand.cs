using APIBusService.Core.DTOs;
using MediatR;

namespace APIBusService.Core.CQRS.Commands.TicketCommands;

public class DeleteTicketCommand : IRequest<TicketDto>
{
    public int Id { get; set; }
}
