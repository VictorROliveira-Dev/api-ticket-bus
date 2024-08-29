using APIBusService.Core.DTOs;
using MediatR;

namespace APIPassagens.Core.CQRS.Commands.UserCommands;

public class DeleteUserCommand : IRequest<UserDto>
{
    public int Id { get; set; } 
}
