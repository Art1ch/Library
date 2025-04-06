using LibraryService.Core.Entities;
using MediatR;

namespace LibraryService.Application.Queries.User.GetAll;

public record UsersGetQuery(int Page, int PageSize)
    : IRequest<List<UserEntity>>;