using LibraryService.Core.Entities;

namespace LibraryService.Core.Contracts.Repositories;
public interface IBookRepository
{ 
    public Task<List<BookEntity>> GetBooksAsync(int page, int pageSize, CancellationToken ct = default);
    public Task<BookEntity> GetBookByIdAsync(Guid id, CancellationToken ct = default);
    public Task<List<BookEntity>> GetBooksByAuthorIdAsync(Guid authorId, CancellationToken ct = default);
    public Task<BookEntity> GetBookByISBNAsync(string isbn, CancellationToken ct = default);
    public Task<Guid> CreateBookAsync(BookEntity book, CancellationToken ct = default);
    public Task<Guid> UpdateBookAsync(BookEntity book, CancellationToken ct = default);
    public Task DeleteBookAsync(Guid id, CancellationToken ct = default);
    //public Task UploadImageAsync(Guid id, byte[] image, CancellationToken ct = default);
}

