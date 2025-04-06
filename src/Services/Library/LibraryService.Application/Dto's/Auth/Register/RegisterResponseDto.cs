namespace LibraryService.Application.Dto_s.Auth.Register;

public record RegisterResponseDto(
    Guid Id,
    string AccessToken,
    string RefreshToken);