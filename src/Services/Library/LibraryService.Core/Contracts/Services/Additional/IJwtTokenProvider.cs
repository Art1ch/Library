using LibraryService.Core.Entities;
using System.Security.Claims;

namespace LibraryService.Core.Contracts.Services.Additional;

public interface IJwtTokenProvider
{
    public string GenerateAccessToken(UserEntity user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal? ValidateToken(string token);
}