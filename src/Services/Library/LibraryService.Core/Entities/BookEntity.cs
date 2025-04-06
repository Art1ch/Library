namespace LibraryService.Core.Entities;

public class BookEntity
{
    public Guid Id { get; set; }
    public Guid AuthorId { get; set; }
    public AuthorEntity Author { get; set; }
    public string ISBN { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    //public byte[]? Image { get; set; }
}
