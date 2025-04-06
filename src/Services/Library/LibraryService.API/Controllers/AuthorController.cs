using LibraryService.Application.Commands.Author.Create;
using LibraryService.Application.Commands.Author.Delete;
using LibraryService.Application.Commands.Author.Update;
using LibraryService.Application.Dto_s.Author;
using LibraryService.Application.Queries.Author.GetAll;
using LibraryService.Application.Queries.Author.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AuthorController : ControllerBase
{
    private readonly ISender _sender;
    public AuthorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("authors")]
    public async Task<IActionResult> GetAuthorsAsync([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = await _sender.Send(new AuthorsGetQuery(page, pageSize));
        return Ok(response);
    }

    [HttpGet("authors/{id}")]
    public async Task<IActionResult> GetAuthorByIdAsync([FromRoute] Guid id)
    {
        var response = await _sender.Send(new AuthorGetByIdQuery(id));
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateAuthorAsync([FromBody] AuthorCreateRequestDto dto)
    {
        var response = await _sender.Send(new AuthorCreateCommand(dto));
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateAuthorAsync([FromBody] AuthorUpdateRequestDto dto)
    {
        var response = await _sender.Send(new AuthorUpdateCommand(dto));
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAuthorAsync([FromBody] AuthorDeleteRequestDto dto)
    {
        var response = await _sender.Send(new AuthorDeleteCommand(dto));
        return Ok(response);
    }
}