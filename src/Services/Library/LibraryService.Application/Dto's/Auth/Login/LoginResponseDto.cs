namespace LibraryService.Application.Dto_s.Auth;

public record LoginResponseDto(
    Guid Id,
    string AccessToken,
    string RefreshToken);