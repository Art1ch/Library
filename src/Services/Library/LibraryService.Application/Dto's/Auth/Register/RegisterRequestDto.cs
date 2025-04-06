namespace LibraryService.Application.Dto_s.Auth.Register;

public record RegisterRequestDto(
    string Email,
    string Name,
    string Password);