using Test2.Contracts.Responces;
using Test2.Models;

namespace Test2.Mappers;

public static class BookMapper
{
    public static BookResponse ToBookResponse(this Book book)
    {
        return new BookResponse()
        {
            Book = new BookDto()
            {
                IdBook = book.IdBook,
                Name = book.Name,
                ReleaseDate = book.ReleaseDate
            },
            PublishingHouse = new PublishingHouseDto()
            {
                Name = book.PublishingHouse.Name,
                Country = book.PublishingHouse.Country,
                City = book.PublishingHouse.City
            }
        };
    }
}