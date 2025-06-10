using Microsoft.AspNetCore.Mvc;
using Test2.Contracts.Requests;
using Test2.Contracts.Responces;
using Test2.Exceptions;
using Test2.Models;
using Test2.Services;

namespace Test2.Controllers;

[ApiController]
[Route("/books")]
public class BookController : ControllerBase
{
    private readonly IBookService _service;

    public BookController(IBookService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<BookResponse>>> GetAllBooksAsync([FromBody] FilterDataRequest request)
    {
        try
        {
            var result = await _service.GetBookListAsync(request);
            return Ok(result);
        }catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateBookAsync([FromBody] AddNewBookRequest request)
    {
        try
        {
            await _service.AddNewBookAsync(request);
            return Ok();
        }
        catch (Exception ex) when (ex is NoProvidedAuthorIdException or NoProvidedGenreIdException)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}