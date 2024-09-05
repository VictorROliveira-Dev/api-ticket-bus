using APIBusService.Core.Abstractions;
using APIBusService.Core.DTOs;
using APIBusService.Core.Entities;
using APIPassagens.Core.CQRS.Commands.UserCommands;
using MediatR;

namespace APIPassagens.API.Handlers.UserHandlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Age = request.Age,
        };

        await _userRepository.AddUser(user);

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Age = user.Age,
        };
    }
}
