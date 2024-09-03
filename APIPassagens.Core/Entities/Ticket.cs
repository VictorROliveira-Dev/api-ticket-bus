namespace APIBusService.Core.Entities;

public class Ticket
{
    public int Id { get; set; }
    public string TicketCode { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
