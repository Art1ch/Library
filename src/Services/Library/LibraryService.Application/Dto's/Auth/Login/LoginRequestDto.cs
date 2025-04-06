namespace LibraryService.Application.Dto_s.Auth.Login;

public record LoginRequestDto(
    string Email,
    string Password);