using LibraryService.Core.Entities;

namespace LibraryService.Application.Dto_s.Author;

public record AuthorUpdateRequestDto(
    Guid Id,
    string FirstName,
    string LastName,
    string CountryCode,
    List<BookEntity> Books,
    DateOnly BirthDay);