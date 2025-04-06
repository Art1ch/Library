using LibraryService.Core.Entities;

namespace LibraryService.Core.Contracts.Repositories;
public interface IAuthorRepository
{
    public Task<List<AuthorEntity>> GetAuthorsAsync(int page, int pageSize, CancellationToken ct = default);
    public Task<AuthorEntity> GetAuthorByIdAsync(Guid id, CancellationToken ct = default);
    public Task<Guid> CreateAuthorAsync(AuthorEntity author, CancellationToken ct = default);
    public Task<Guid> UpdateAuthorAsync(AuthorEntity author, CancellationToken ct = default);
    public Task DeleteAuthorAsync(Guid id, CancellationToken ct = default);
}
