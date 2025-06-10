using Microsoft.EntityFrameworkCore;
using Test2.Contracts.Requests;
using Test2.Contracts.Responces;
using Test2.Exceptions;
using Test2.Mappers;
using Test2.Models;

namespace Test2.Services;

public class BookService(BookDbContext context) : IBookService
{
    public async Task AddNewBookAsync(AddNewBookRequest request)
    {
        var publishingHouse = await context.PublishingHouses.FirstOrDefaultAsync(x => x.IdPublishingHouse == request.IdPublishingHouse);
        if (publishingHouse == null)
        {
            publishingHouse = new PublishingHouse()
            {
                IdPublishingHouse = request.IdPublishingHouse,
                Name = request.Name,
                Country = request.Country,
                City = request.City,
            };
        }
        context.PublishingHouses.Add(publishingHouse);

        
        var bookToAdd = new Book()
        {
            IdPublishingHouse = request.IdPublishingHouse,
            Name = request.NameBook,
            ReleaseDate = DateTime.Now,
            IdBook = request.IdBook,
            PublishingHouse = publishingHouse,
            BookAuthors = new List<BookAuthor>(),
            BookGenres = new List<BookGenre>()
        };
        foreach (var idAuthor in request.AuthorIds)
        {
            if (await context.Authors.FirstOrDefaultAsync(e => e.IdAuthor == idAuthor) == null)
            {
                throw new NoProvidedAuthorIdException(idAuthor);
            }

            var bookAuthor = new BookAuthor() { IdAuthor = idAuthor, IdBook = bookToAdd.IdBook };
            context.BookAuthors.Add(bookAuthor);
            bookToAdd.BookAuthors.Add(bookAuthor);
        }
        foreach (var idGenre in request.GenreIds)
        {
            if (await context.Genres.FirstOrDefaultAsync(e => e.IdGenre == idGenre) == null)
            {
                throw new NoProvidedGenreIdException(idGenre);
            }

            var bg = new BookGenre() { IdGenre = idGenre, IdBook = bookToAdd.IdBook };
            context.BookGenres.Add(bg);
            bookToAdd.BookGenres.Add(bg);
        }
        
        context.Books.Add(bookToAdd);
        await context.SaveChangesAsync();
    }

    public async Task<List<BookResponse>> GetBookListAsync(FilterDataRequest request)
    {
        List<Book> books = null;
        var filteringData = request.ReleaseDate;
        if(filteringData == null)
        {
            books = await context.Books
                        .Include(b => b.BookAuthors)
                        .ThenInclude(ba => ba.Author)
                        .Include(b => b.BookGenres)
                        .ThenInclude(bg => bg.Genre)
                        .ToListAsync();
        }
        else
        {
            books = await context.Books
                .Where(b => filteringData == b.ReleaseDate)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .ToListAsync();
        }
        
        var mappedBooks = books.Select(b => BookMapper.ToBookResponse(b)).ToList();
        return mappedBooks;
    }
}