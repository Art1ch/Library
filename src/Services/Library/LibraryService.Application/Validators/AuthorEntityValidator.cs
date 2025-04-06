using FluentValidation;
using LibraryService.Core.Entities;

namespace LibraryService.Application.Validators;

public class AuthorEntityValidator : AbstractValidator<AuthorEntity>
{
    public AuthorEntityValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters")
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage("First name can only contain letters, spaces and hyphens");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters")
            .Matches("^[a-zA-Zа-яА-Я- ]+$").WithMessage("Last name can only contain letters, spaces and hyphens");

        RuleFor(x => x.CountryCode)
            .NotEmpty().WithMessage("Country code is required")
            .Length(2).WithMessage("Country code must be 2 characters")
            .Matches("^[A-Z]{2}$").WithMessage("Country code must be two uppercase letters");

        RuleFor(x => x.BirthDay)
            .NotEmpty().WithMessage("Birthday is required")
            .WithMessage("Author must be at least 10 years old");
    }
}
