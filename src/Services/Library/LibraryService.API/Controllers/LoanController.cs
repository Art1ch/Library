using LibraryService.Application.Commands.Loan.Create;
using LibraryService.Application.Commands.Loan.Delete;
using LibraryService.Application.Commands.Loan.Update;
using LibraryService.Application.Dto_s.Loan.Create;
using LibraryService.Application.Dto_s.Loan.Delete;
using LibraryService.Application.Dto_s.Loan.Update;
using LibraryService.Application.Queries.Loan.GetAllByUserId;
using LibraryService.Application.Queries.Loan.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryService.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class LoanController : ControllerBase
{
    private readonly ISender _sender;

    public LoanController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("loans/user/{userId}")]
    public async Task<IActionResult> GetLoansAsync([FromRoute] Guid userId)
    {
        var response = await _sender.Send(new LoansGetByUserIdQuery(userId));
        return Ok(response);
    }

    [HttpGet("loans/{id}")]
    public async Task<IActionResult> GetLoanAsync([FromRoute] Guid id)
    {
        var response = await _sender.Send(new LoanGetByIdQuery(id));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLoanAsync([FromBody] LoanCreateRequestDto dto)
    {
        var response = await _sender.Send(new LoanCreateCommand(dto));
        return Ok(response);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateLoanAsync([FromBody] LoanUpdateRequestDto dto)
    {
        var response = await _sender.Send(new LoanUpdateCommand(dto));
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteLoanAsync([FromBody] LoanDeleteRequestDto dto)
    {
        var response = await _sender.Send(new LoanDeleteCommand(dto));
        return Ok(response);
    }
}
