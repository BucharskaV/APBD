using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TripApp.Application.DTOs;
using TripApp.Application.Exceptions;
using TripApp.Application.Services.Interfaces;
using TripApp.Core.Model;

namespace Trip.API.Controllers;

[ApiController]
[Route("api/trips")]
public class TripController(
    ITripService tripService) 
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResult<GetTripDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<GetTripDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTrips(
        [FromQuery(Name = "page")] int? page,
        [FromQuery(Name = "pageSize")] int? pageSize,
        CancellationToken cancellationToken = default)
    {
        if (page is null && pageSize is null)
        {
            var trips = await tripService.GetAllTripsAsync();
            return Ok(trips);
        }

        var paginatedTrips = await tripService.GetPaginatedTripsAsync(page ?? 1, pageSize ?? 10);
        return Ok(paginatedTrips);
    }

    [HttpPost]
    [Route("{idTrip:int}/clients")]
    [ProducesResponseType( StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AssignClientToTrip([FromRoute]int idTrip,[FromBody] ClientRequestDto dto, CancellationToken cancellationToken = default)
    {
        try
        {
            await tripService.AssignClientToTripAsync(dto);
            return Ok();
        }
        catch (Exception ex) when (ex is ClientExceptions.ClientExistsByPeselException 
                                   || ex is ClientExceptions.ClientRegisteredByPeselException 
                                   || ex is TripExceptions.TripHasAlreadyOccuredException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex) when (ex is TripExceptions.TripDoesNotExistException)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}