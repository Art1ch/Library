using LibraryService.Application.Commands.User.Create;
using LibraryService.Application.Commands.User.Delete;
using LibraryService.Application.Commands.User.Update;
using LibraryService.Application.Dto_s.User;
using LibraryService.Application.Queries.User.GetAll;
using LibraryService.Application.Queries.User.GetByEmail;
using LibraryService.Application.Queries.User.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = await _sender.Send(new UsersGetQuery(page, pageSize));
        return Ok(response);
    }

    [HttpGet("users/id/{id}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id)
    {
        var response = await _sender.Send(new UserGetByIdQuery(id));
        return Ok(response);
    }

    [HttpGet("users/email/{email}")]
    public async Task<IActionResult> GetUserByEmailAsync([FromRoute] string email)
    {
        var response = await _sender.Send(new UserGetByEmailQuery(email));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserCreateRequestDto dto)
    {
        var response = await _sender.Send(new UserCreateCommand(dto));
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateRequestDto dto)
    {
        var response = await _sender.Send(new UserUpdateCommand(dto));
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserDeleteRequestDto dto)
    {
        var response = await _sender.Send(new UserDeleteCommand(dto));
        return Ok(response);
    }
}