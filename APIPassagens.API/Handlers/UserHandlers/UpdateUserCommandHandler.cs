using APIBusService.Core.Abstractions;
using APIBusService.Core.DTOs;
using APIPassagens.Core.CQRS.Commands.UserCommands;
using MediatR;

namespace APIPassagens.API.Handlers.UserHandlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.Id);

        if (user == null)
        {
            throw new ArgumentException("User not found.");
        }

        user.Name = request.Name;
        user.Email = request.Email;
        user.Age = request.Age;

        await _userRepository.UpdateUser(user, user.Id);

        return new UserDto
        {
            Name = user.Name,
            Email = user.Email,
            Age = user.Age,
        };
    }
}
