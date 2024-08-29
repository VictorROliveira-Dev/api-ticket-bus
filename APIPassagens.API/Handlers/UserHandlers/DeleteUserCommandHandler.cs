using APIBusService.Core.Abstractions;
using APIBusService.Core.DTOs;
using APIPassagens.Core.CQRS.Commands.UserCommands;
using MediatR;

namespace APIPassagens.API.Handlers.UserHandlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserById(request.Id);

        if (user == null)
        {
            throw new Exception(nameof(user));
        }

        await _userRepository.DeleteUser(user.Id);

        return new UserDto { Id = request.Id };
    }
}