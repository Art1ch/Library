using FluentAssertions;
using LibraryService.Application.Queries.Author.GetAll;
using LibraryService.Core.Contracts.Repositories;
using LibraryService.Core.Entities;
using Moq;

namespace LibraryService.Tests.Queries.Author;

public class AuthorsGetQueryHandlerTests
{
    private readonly Mock<IAuthorRepository> _repositoryMock;
    private readonly AuthorsGetQueryHandler _handler;

    public AuthorsGetQueryHandlerTests()
    {
        _repositoryMock = new Mock<IAuthorRepository>();
        _handler = new AuthorsGetQueryHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidRequest_ReturnsAuthorsList()
    {
        var page = 1;
        var pageSize = 10;
        var query = new AuthorsGetQuery(page, pageSize);

        var expectedAuthors = new List<AuthorEntity>
        {
            new() { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe" },
            new() { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
        };

        _repositoryMock
            .Setup(r => r.GetAuthorsAsync(page, pageSize, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedAuthors);

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expectedAuthors);
        _repositoryMock.Verify(r =>
            r.GetAuthorsAsync(page, pageSize, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_RepositoryReturnsEmptyList_ReturnsEmptyList()
    {
        var query = new AuthorsGetQuery(1, 10);
        _repositoryMock
            .Setup(r => r.GetAuthorsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<AuthorEntity>());

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task Handle_RepositoryThrowsException_PropagatesException()
    {
        var query = new AuthorsGetQuery(1, 10);
        _repositoryMock
            .Setup(r => r.GetAuthorsAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("DB error"));

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(query, CancellationToken.None));
    }
}
