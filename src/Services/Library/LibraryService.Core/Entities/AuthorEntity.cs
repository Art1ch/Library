using System.ComponentModel;

namespace LibraryService.Core.Entities;

public class AuthorEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CountryCode { get; set; }
    public List<BookEntity> Books { get; set; } = new();
    public DateOnly BirthDay { get; set; }
}