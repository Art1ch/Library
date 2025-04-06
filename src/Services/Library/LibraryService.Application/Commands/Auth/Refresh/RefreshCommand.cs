using LibraryService.Application.Dto_s.Auth;
using LibraryService.Application.Dto_s.Auth.Refresh;
using MediatR;

namespace LibraryService.Application.Commands.Auth.Refresh;

public record RefreshCommand(
    RefreshRequestDto Dto) : IRequest<RefreshResponseDto>;