using LibraryService.Core.Roles;

namespace LibraryService.Application.Dto_s.User;

public record UserUpdateRequestDto(
    Guid Id,
    string Email,
    string Name,
    Role Role);
