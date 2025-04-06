namespace LibraryService.Core.Entities;

public class LoanEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid BookId { get; set; }
    public BookEntity Book { get; set; }
    public DateOnly TakenDate { get; set; }
    public DateOnly DueDate { get; set; }
}