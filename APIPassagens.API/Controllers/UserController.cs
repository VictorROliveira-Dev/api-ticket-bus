using APIPassagens.Core.CQRS.Commands.UserCommands;
using APIPassagens.Core.CQRS.Queries.UserQueries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIPassagens.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = id });

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var user = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(UpdateUserCommand command, int id)
    {
        if (id != command.Id)
        {
            return BadRequest("Id mismatch");
        }

        var user = await _mediator.Send(new GetUserByIdQuery { Id = id });

        if (user == null)
        {
            return NotFound();
        }

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = id });  

        if (user == null)
        {
            return NotFound();
        }

        await _mediator.Send(new DeleteUserCommand { Id = id });

        return NoContent();
    }
}
