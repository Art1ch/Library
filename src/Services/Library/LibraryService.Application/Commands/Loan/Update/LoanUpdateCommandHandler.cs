using AutoMapper;
using FluentValidation;
using LibraryService.Application.Dto_s.Loan.Update;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.Loan.Update;

public class LoanUpdateCommandHandler :
    IRequestHandler<LoanUpdateCommand, LoanUpdateResponseDto>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<LoanEntity> _validator;

    public LoanUpdateCommandHandler(
        ILoanRepository loanRepository,
        IMapper mapper,
        IValidator<LoanEntity> validator)
    {
        _loanRepository = loanRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<LoanUpdateResponseDto> Handle(LoanUpdateCommand request, CancellationToken cancellationToken)
    {
        var loan = _mapper.Map<LoanEntity>(request.Dto);

        var validationResult = await _validator.ValidateAsync(loan);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _loanRepository.UpdateLoanAsync(loan);
        return _mapper.Map<LoanUpdateResponseDto>(loan);
    }
}
