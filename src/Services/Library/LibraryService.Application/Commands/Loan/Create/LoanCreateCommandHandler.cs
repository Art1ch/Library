using AutoMapper;
using FluentValidation;
using LibraryService.Application.Dto_s.Loan.Create;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Commands.Loan.Create;

public class LoanCreateCommandHandler :
    IRequestHandler<LoanCreateCommand, LoanCreateResponseDto>
{
    private readonly ILoanRepository _loanRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<LoanEntity> _validator;
    public LoanCreateCommandHandler(
        ILoanRepository loanRepository,
        IMapper mapper,
        IValidator<LoanEntity> validator)
    {
        _loanRepository = loanRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<LoanCreateResponseDto> Handle(LoanCreateCommand request, CancellationToken cancellationToken)
    {
        var loan = _mapper.Map<LoanEntity>(request.Dto);

        var validationResult = await _validator.ValidateAsync(loan);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        await _loanRepository.CreateLoanAsync(loan, cancellationToken);
        return _mapper.Map<LoanCreateResponseDto>(loan);
    }
}
