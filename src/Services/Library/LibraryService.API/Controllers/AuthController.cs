using LibraryService.Application.Commands.Auth.Login;
using LibraryService.Application.Commands.Auth.Refresh;
using LibraryService.Application.Commands.Auth.Register;
using LibraryService.Application.Dto_s.Auth.Login;
using LibraryService.Application.Dto_s.Auth.Refresh;
using LibraryService.Application.Dto_s.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto dto)
    {
        var result = await _sender.Send(new RegisterCommand(dto));
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDto dto)
    {
        var result = await _sender.Send(new LoginCommand(dto));
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync([FromBody] RefreshRequestDto dto)
    {
        var result = await _sender.Send(new RefreshCommand(dto));
        return Ok(result);
    }
}

