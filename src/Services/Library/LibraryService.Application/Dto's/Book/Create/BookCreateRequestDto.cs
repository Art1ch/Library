namespace LibraryService.Application.Dto_s.Book.Create;

public record BookCreateRequestDto(
    Guid AuthorId,
    string ISBN,
    string Name,
    string Description,
    string Genre
    /*byte[]? Image)*/);