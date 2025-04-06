using LibraryService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Context;
public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<BookEntity> Books { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<LoanEntity> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AuthorEntity>(author =>
        {
            author.HasKey(a => a.Id);
            author.HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<BookEntity>(book =>
        {
            book.HasKey(b => b.Id);
            book.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);
        });

        modelBuilder.Entity<UserEntity>(user =>
        {
            user.HasKey(u => u.Id);
            user.HasMany(u => u.Loans).
                WithOne(b => b.User).
                HasForeignKey(b => b.UserId).
                OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LoanEntity>(loan =>
        {
            loan.HasKey(l => l.Id);
            loan.HasOne(l => l.Book)
                .WithMany()
                .HasForeignKey(l => l.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            loan.HasKey(l => l.Id);
            loan.HasOne(l => l.User).
                WithMany(u => u.Loans).
                HasForeignKey(l => l.UserId).
                OnDelete(DeleteBehavior.Restrict);
        });
    }
}