using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.User.GetById;

public record UserGetByIdQuery(Guid Id) : IRequest<UserEntity>;