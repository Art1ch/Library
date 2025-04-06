using FluentValidation;
using LibraryService.Core.Entities;

namespace LibraryService.Application.Validators;

public class LoanEntityValidator : AbstractValidator<LoanEntity>
{
    public LoanEntityValidator()
    {
        RuleFor(x => x.TakenDate)
            .NotEmpty().WithMessage("Taken date is required")
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Taken date cannot be in the future");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Due date is required")
            .GreaterThan(x => x.TakenDate)
            .WithMessage("Due date must be after taken date")
            .LessThanOrEqualTo(x => x.TakenDate.AddMonths(3))
            .WithMessage("Loan period cannot exceed 3 months");
    }
}
