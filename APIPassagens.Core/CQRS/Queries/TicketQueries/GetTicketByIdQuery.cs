using APIBusService.Core.DTOs;
using MediatR;

namespace APIBusService.Core.CQRS.Queries.TicketQueries;

public class GetTicketByIdQuery : IRequest<TicketDto>
{
    public int Id { get; set; }
}
