using APIBusService.Core.DTOs;
using MediatR;

namespace APIBusService.Core.CQRS.Queries.TicketQueries;

public class GetAllTicketsQuery : IRequest<IEnumerable<TicketDto>>
{

}
