using APIBusService.Core.CQRS.Commands.TicketCommands;
using APIBusService.Core.CQRS.Queries.TicketQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIPassagens.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketController : ControllerBase
{
    private readonly IMediator _mediator;

    public TicketController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTickets()
    {
        var tickets = await _mediator.Send(new GetAllTicketsQuery());
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketById(int id)
    {
        var ticket = await _mediator.Send(new GetTicketByIdQuery { Id = id });

        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket(CreateTicketCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var ticket = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetTicketById), new { id = ticket.Id }, ticket);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket(UpdateTicketCommand command, int id)
    {
        if (id != command.Id)
        {
            return BadRequest("Id mismatch");
        }

        var ticketCheck = await _mediator.Send(new GetTicketByIdQuery { Id = id });

        if (ticketCheck == null)
        {
            return NotFound();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        var ticketCheck = await _mediator.Send(new GetTicketByIdQuery { Id = id });

        if (ticketCheck == null)
        {
            return NotFound();
        }

        await _mediator.Send(new DeleteTicketCommand { Id = id });
        
        return NoContent();
    }
}
