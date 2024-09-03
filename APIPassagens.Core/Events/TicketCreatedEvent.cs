using MediatR;

namespace APIPassagens.Core.Events;

public class TicketCreatedEvent : INotification
{
    public int TicketId { get; set; }
    public string Email { get; set; }
    public string TicketCode { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
