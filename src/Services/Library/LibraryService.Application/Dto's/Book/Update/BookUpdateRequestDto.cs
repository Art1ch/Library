namespace LibraryService.Application.Dto_s.Book.Update;

public record BookUpdateRequestDto(
    Guid BookId,
    Guid AuthorId,
    string ISBN,
    string Name,
    string Description,
    string Genre
    /*byte[]? Image*/);