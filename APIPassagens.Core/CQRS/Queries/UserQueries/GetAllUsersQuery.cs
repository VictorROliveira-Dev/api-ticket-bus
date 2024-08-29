using APIBusService.Core.DTOs;
using MediatR;

namespace APIPassagens.Core.CQRS.Queries.UserQueries;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDto>>
{
}
