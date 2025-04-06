using FluentValidation;
using LibraryService.Core.Entities;

namespace LibraryService.Application.Validators;

public class BookEntityValidator : AbstractValidator<BookEntity>
{
    public BookEntityValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Book name is required")
            .Length(2, 100).WithMessage("Book name must be between 2 and 100 characters");

        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required")
            .Matches(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$")
            .WithMessage("Invalid ISBN format");

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre is required")
            .Length(2, 50).WithMessage("Genre must be between 2 and 50 characters");
    }
}
