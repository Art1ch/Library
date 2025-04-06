using LibraryService.Application.Commands.Book.Create;
using LibraryService.Application.Commands.Book.Delete;
using LibraryService.Application.Commands.Book.Update;
using LibraryService.Application.Commands.Loan.Create;
using LibraryService.Application.Dto_s.Book.Create;
using LibraryService.Application.Dto_s.Book.Delete;
using LibraryService.Application.Dto_s.Book.Update;
using LibraryService.Application.Dto_s.Loan.Create;
using LibraryService.Application.Queries.Book.GetAll;
using LibraryService.Application.Queries.Book.GetAllByAuthorId;
using LibraryService.Application.Queries.Book.GetById;
using LibraryService.Application.Queries.Book.GetByISBN;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ISender _sender;

    public BookController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("books")]
    public async Task<IActionResult> GetBooksAsync([FromQuery] int page, [FromQuery] int pageSize)
    {
        var response = await _sender.Send(new BooksGetQuery(page, pageSize));
        return Ok(response);
    }

    [HttpGet("books/{id}")]
    public async Task<IActionResult> GetBooksAsync([FromRoute] Guid id)
    {
        var response = await _sender.Send(new BookGetByIdQuery(id));
        return Ok(response);
    }

    [HttpGet("books/author/{authorId}")]
    public async Task<IActionResult> GetBooksByAuthorAsync([FromRoute] Guid authorId)
    {
        var response = await _sender.Send(new BooksGetByAuthorIdQuery(authorId));
        return Ok(response);
    }

    [HttpGet("books/ISBN/{isbn}")]
    public async Task<IActionResult> GetBookByISBNAsync([FromRoute] string isbn)
    {
        var response = await _sender.Send(new BookGetByISBNQuery(isbn));
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateBookAsync([FromBody] BookCreateRequestDto dto)
    {
        var response = await _sender.Send(new BookCreateCommand(dto));
        return Ok(response);    
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<IActionResult> UpdateBookAsync([FromBody] BookUpdateRequestDto dto)
    {
        var response = await _sender.Send(new BookUpdateCommand(dto));
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteBookAsync([FromBody] BookDeleteRequestDto dto)
    {
        var response = await _sender.Send(new BookDeleteCommand(dto));
        return Ok(response);
    }

    [HttpPost("books/give")]
    public async Task<IActionResult> GiveBookToUserAsync([FromBody] LoanCreateRequestDto dto)
    {
        var response = await _sender.Send(new LoanCreateCommand(dto));
        return Ok(response);
    }
}