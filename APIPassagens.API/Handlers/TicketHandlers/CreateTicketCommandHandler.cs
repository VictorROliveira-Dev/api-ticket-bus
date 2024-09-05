using APIBusService.Core.Abstractions;
using APIBusService.Core.CQRS.Commands.TicketCommands;
using APIBusService.Core.DTOs;
using APIBusService.Core.Entities;
using APIPassagens.Core.Events;
using MediatR;

namespace APIBusServiceAPI.Handlers.TicketHandlers;

public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, TicketDto>
{
    private readonly ITicketRepository _ticketrepository;
    private readonly IMediator _mediator;
    private IUserRepository _userRepository;

    public CreateTicketCommandHandler(ITicketRepository ticketrepository, IUserRepository userRepository, IMediator mediator)
    {
        _ticketrepository = ticketrepository;
        _userRepository = userRepository;
        _mediator = mediator;
    }

    public async Task<TicketDto> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.UserId);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (request.ReturnDate.HasValue && request.ReturnDate.Value < request.DepartureDate)
        {
            throw new Exception("The return date cannot be earlier than the departure date.");
        }

        var ticketCode = GenerateTrackingCode();

        var ticket = new Ticket
        {
            TicketCode = ticketCode,
            DepartureDate = request.DepartureDate,
            ReturnDate = request.ReturnDate,
            UserId = request.UserId,
            User = user,
        };

        await _ticketrepository.AddTicket(ticket);

        var ticketCreatedEvent = new TicketCreatedEvent
        {
            TicketId = ticket.Id,
            Email = request.Email,
            TicketCode = ticket.TicketCode,
            DepartureDate = ticket.DepartureDate,
            ReturnDate = ticket.ReturnDate,
        };

        await _mediator.Publish(ticketCreatedEvent, cancellationToken);

        return new TicketDto
        {
            Id = ticket.Id,
            TicketCode = ticket.TicketCode,
            DepartureDate = ticket.DepartureDate,
            ReturnDate = ticket.ReturnDate,
            UserId = ticket.UserId,
            Email = ticket.User.Email
        };
    }

    private string GenerateTrackingCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string numbers = "0123456789";

        var code = new char[10];
        var random = new Random();

        for (var i = 0; i < 5; i++)
        {
            code[i] = chars[random.Next(chars.Length)];
        }

        for (var i = 5; i < 10; i++)
        {
            code[i] = numbers[random.Next(numbers.Length)];
        }

        return new string(code);
    }
}
