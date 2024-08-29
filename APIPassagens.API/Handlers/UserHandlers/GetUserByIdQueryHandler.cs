using APIBusService.Core.Abstractions;
using APIBusService.Core.DTOs;
using APIPassagens.Core.CQRS.Queries.UserQueries;
using MediatR;

namespace APIPassagens.API.Handlers.UserHandlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.Id);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Age = user.Age,
        };
    }
}
