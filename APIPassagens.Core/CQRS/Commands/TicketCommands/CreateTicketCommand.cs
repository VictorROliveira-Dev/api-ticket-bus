using APIBusService.Core.DTOs;
using MediatR;

namespace APIBusService.Core.CQRS.Commands.TicketCommands;

public class CreateTicketCommand : IRequest<TicketDto>
{
    public DateTime DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int UserId { get; set; }
}