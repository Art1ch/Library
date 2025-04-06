namespace LibraryService.Application.Dto_s.User;

public record UserCreateRequestDto(
    string Email,
    string Name,
    string Password);