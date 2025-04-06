namespace LibraryService.Core.Responses;

public record AuthResponse(
    Guid Id,
    string AccessToken,
    string RefreshToken);