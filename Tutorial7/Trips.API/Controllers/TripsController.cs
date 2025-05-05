using Microsoft.AspNetCore.Mvc;
using Trips.API.Entities;
using Trips.API.Repositories.Interfaces;
using Trips.API.Services;
using Trips.API.Services.Interfaces;

namespace Trips.API.Controllers;

[ApiController]
[Route("api/trips")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    
    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTripsAsync()
    {
        var trips =  await _tripService.GetAllTripsWithCountriesAsync();
        return Ok(trips);
    }
}