using LibraryService.Application.Dto_s.Auth.Register;
using MediatR;

namespace LibraryService.Application.Commands.Auth.Register;

public record RegisterCommand(
    RegisterRequestDto Dto) : IRequest<RegisterResponseDto>;