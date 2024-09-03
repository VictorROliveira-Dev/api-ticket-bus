using APIPassagens.Core.Abstractions;
using APIPassagens.Core.Events;
using MediatR;

namespace APIPassagens.API.Handlers.TicketHandlers;

public class TicketCreatedEventHandler : INotificationHandler<TicketCreatedEvent>
{
    private readonly IMessageBus _messageBus;

    public TicketCreatedEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
    {
        var message = new
        {
            notification.TicketId,
            notification.Email,
            notification.TicketCode,
        };

        await _messageBus.PublishAsync("ticket_created", message);
    }
}
