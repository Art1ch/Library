using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.User.GetByEmail;

public record UserGetByEmailQuery(string Email) : IRequest<UserEntity>;