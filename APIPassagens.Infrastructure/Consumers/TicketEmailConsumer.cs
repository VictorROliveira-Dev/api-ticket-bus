using APIPassagens.Core.Abstractions;
using APIPassagens.Core.Events;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;


namespace APIPassagens.Infrastructure.Consumers;

public class TicketEmailConsumer : BackgroundService
{
    private readonly IMessageBus _messageBus;
    private readonly IEmailService _emailService;

    public TicketEmailConsumer(IMessageBus messageBus, IEmailService emailService)
    {
        _messageBus = messageBus;
        _emailService = emailService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _messageBus.SubscribeAsync("ticket_created", async (message) =>
        {
            var ticketCreatedEvent = JsonConvert.DeserializeObject<TicketCreatedEvent>(message);
            var subject = "Sua passagem foi comprada!";
            var emailMessage = $"<p>Olá,</p><p>Seu ticket com código <strong>{ticketCreatedEvent?.TicketCode}</strong> foi comprado com sucesso!</p><p>Data de Partida: {ticketCreatedEvent?.DepartureDate}</p><p>Data de Retorno: {ticketCreatedEvent?.ReturnDate}</p>";

            await _emailService.SendEmailAsync(ticketCreatedEvent.Email, subject, emailMessage);
        }, stoppingToken);
    }
}
