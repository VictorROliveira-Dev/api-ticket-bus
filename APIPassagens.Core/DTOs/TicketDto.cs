using System.ComponentModel.DataAnnotations;

namespace APIBusService.Core.DTOs;

public class TicketDto
{
    public int Id { get; set; }
    public string TicketCode { get; set; }
    public DateTime DepartureDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public int UserId { get; set; }
    public string Email {  get; set; }
}
