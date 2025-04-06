namespace LibraryService.Application.Dto_s.Author;

public record AuthorCreateRequestDto(
    string FirstName,
    string LastName,
    string CountryCode,
    DateOnly BirthDay);