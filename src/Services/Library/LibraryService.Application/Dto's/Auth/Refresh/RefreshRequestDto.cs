namespace LibraryService.Application.Dto_s.Auth.Refresh;

public record RefreshRequestDto(
    string AccessToken,
    string RefreshToken);