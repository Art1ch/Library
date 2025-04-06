namespace LibraryService.Application.Dto_s.Auth;

public record RefreshResponseDto(
    Guid Id,
    string AccessToken,
    string RefreshToken);