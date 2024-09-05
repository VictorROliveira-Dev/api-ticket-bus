using APIPassagens.Core.Abstractions;
using APIPassagens.Core.Events;
using MediatR;

namespace APIPassagens.API.Handlers.TicketHandlers;

public class TicketCreatedEventHandler : INotificationHandler<TicketCreatedEvent>
{
    private readonly IEmailService _emailService;

    public TicketCreatedEventHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async Task Handle(TicketCreatedEvent notification, CancellationToken cancellationToken)
    {
        // Envia o email utilizando o serviço de email
        var subject = "Compra de passagem realizada!😁";
        var body = $"Seu ticket foi criado com sucesso.\n" +
                   $"Código do ticket: {notification.TicketCode}\n" +
                   $"Data de Início: {notification.DepartureDate:dd/MM/yyyy}\n" +
                   $"Data de Retorno: {(notification.ReturnDate.HasValue ? notification.ReturnDate.Value.ToString("dd/MM/yyyy") : "Não aplicável")}";

        await _emailService.SendEmailAsync(notification.Email, subject, body);
    }
}
