using Test2.Contracts.Requests;
using Test2.Contracts.Responces;
using Test2.Models;

namespace Test2.Services;

public interface IBookService
{
    Task AddNewBookAsync(AddNewBookRequest request);
    Task<List<BookResponse>> GetBookListAsync(FilterDataRequest request);
}