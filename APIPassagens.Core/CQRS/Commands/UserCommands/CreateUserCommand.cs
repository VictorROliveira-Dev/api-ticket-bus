using APIBusService.Core.DTOs;
using MediatR;

namespace APIPassagens.Core.CQRS.Commands.UserCommands;

public class CreateUserCommand : IRequest<UserDto>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
}
