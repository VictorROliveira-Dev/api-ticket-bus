using APIBusService.Core.DTOs;
using MediatR;

namespace APIPassagens.Core.CQRS.Queries.UserQueries;

public class GetUserByIdQuery : IRequest<UserDto>
{
    public int Id { get; set; }
}
