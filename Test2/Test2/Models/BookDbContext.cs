using Microsoft.EntityFrameworkCore;

namespace Test2.Models;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }
    public DbSet<Book> Books { get; set; }
    public DbSet<BookGenre> BookGenres { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<PublishingHouse> PublishingHouses { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<PublishingHouse>().ToTable("PublishingHouse");
        b.Entity<PublishingHouse>().HasKey(e => e.IdPublishingHouse);
        b.Entity<PublishingHouse>().Property(e=>e.IdPublishingHouse)
            .IsRequired();
        b.Entity<PublishingHouse>().Property(e=>e.Name)
            .IsRequired()
            .HasMaxLength(50);
        b.Entity<PublishingHouse>().Property(e=>e.Country)
            .IsRequired()
            .HasMaxLength(50);
        b.Entity<PublishingHouse>().Property(e=>e.City)
            .IsRequired()
            .HasMaxLength(50);
        b.Entity<PublishingHouse>().HasMany(e => e.Books)
            .WithOne(e => e.PublishingHouse)
            .HasForeignKey(e => e.IdPublishingHouse)
            .OnDelete(DeleteBehavior.Cascade);
        
        b.Entity<Book>().ToTable("Book");
        b.Entity<Book>().HasKey(e => e.IdBook);
        b.Entity<Book>().Property(e=>e.IdBook)
            .IsRequired();
        b.Entity<Book>().Property(e=>e.Name)
            .IsRequired()
            .HasMaxLength(50);
        b.Entity<Book>().Property(e=>e.ReleaseDate)
            .IsRequired()
            .HasColumnType("datetime");
        b.Entity<Book>().HasMany(e => e.BookGenres)
            .WithOne(e => e.Book)
            .HasForeignKey(e => e.IdBook)
            .OnDelete(DeleteBehavior.Cascade);
        b.Entity<Book>().HasMany(e => e.BookAuthors)
            .WithOne(e => e.Book)
            .HasForeignKey(e => e.IdBook)
            .OnDelete(DeleteBehavior.Cascade);
        
        b.Entity<Genre>().ToTable("Genre");
        b.Entity<Genre>().HasKey(e => e.IdGenre);
        b.Entity<Genre>().Property(e=>e.IdGenre)
            .IsRequired();
        b.Entity<Genre>().Property(e=>e.Name)
            .IsRequired()
            .HasMaxLength(50);
        b.Entity<Genre>().HasMany(e => e.BookGenres)
            .WithOne(e => e.Genre)
            .HasForeignKey(e => e.IdGenre)
            .OnDelete(DeleteBehavior.Cascade);
        
        b.Entity<BookGenre>().ToTable("BookGenre");
        b.Entity<BookGenre>().HasKey(e => new {e.IdGenre, e.IdBook});
        
        b.Entity<Author>().ToTable("Author");
        b.Entity<Author>().HasKey(e => e.IdAuthor);
        b.Entity<Author>().Property(e=>e.IdAuthor)
            .IsRequired();
        b.Entity<Author>().Property(e=>e.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        b.Entity<Author>().Property(e=>e.LastName)
            .IsRequired()
            .HasMaxLength(50);
        b.Entity<Author>().HasMany(e => e.BookAuthors)
            .WithOne(e => e.Author)
            .HasForeignKey(e => e.IdAuthor)
            .OnDelete(DeleteBehavior.Cascade);
        
        b.Entity<BookAuthor>().ToTable("BookAuthor");
        b.Entity<BookAuthor>().HasKey(e => new {e.IdBook, e.IdAuthor});


        b.Entity<PublishingHouse>().HasData(
            new PublishingHouse(){IdPublishingHouse = 1, Name = "gra", Country = "Poland", City = "Warsaw"});

        b.Entity<Genre>().HasData(
            new Genre() { IdGenre = 1, Name = "Horror"});
        
        b.Entity<Author>().HasData(
            new Author() { IdAuthor = 1, FirstName = "Ana", LastName = "Brown"});
        
        b.Entity<Book>().HasData(
            new Book() { IdBook = 1, Name = "Book", ReleaseDate = new DateTime(2025, 01, 01), IdPublishingHouse = 1});

        b.Entity<BookGenre>().HasData(new BookGenre() { IdGenre = 1, IdBook = 1 });
        b.Entity<BookAuthor>().HasData(new BookAuthor() { IdAuthor = 1, IdBook = 1 });
    }
}