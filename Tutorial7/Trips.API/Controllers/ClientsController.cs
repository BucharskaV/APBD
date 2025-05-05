using Microsoft.AspNetCore.Mvc;
using Trips.API.Contracts.Requests;
using Trips.API.Entities;
using Trips.API.Exceptions;
using Trips.API.Services;

namespace Trips.API.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly ClientService _clientService;
    private readonly TripService _tripService;

    public ClientsController(ClientService clientService, TripService tripService)
    {
        _clientService = clientService;
        _tripService = tripService;
    }

    [HttpGet("{clientId:int}/trips")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllTripsByClient([FromRoute] int clientId)
    {
        try
        {
            if (clientId <= 0)
                return BadRequest();
            var result = await _clientService.GetAllClientTripsAsync(clientId);
            if (result is null)
                throw new ClientDoesNotExistException(clientId);
            return Ok(result);
        }
        catch (ClientDoesNotExistException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientRequest client)
    {
        try
        {
            var clientId = await _clientService.CreateClientAsync(client);
            return CreatedAtAction(nameof(GetAllTripsByClient), new { clientId }, clientId);
        }
        catch (Exception)
        {
            return BadRequest("Server Error.");
        }
    }
    
    [HttpPut("{clientId:int}/trips/{tripId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateClientTripAsync([FromRoute] int clientId, [FromRoute] int tripId)
    {
        if (clientId <= 0) return BadRequest();
        if (tripId <= 0) return BadRequest();
        try
        {
            var result = await _tripService.RegisterClientToTripAsync(tripId, clientId);
            return Ok();
        }
        catch (TripDoesNotExistException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ClientDoesNotExistException ex)
        {
            return NotFound(ex.Message);
        }
    }
    
    [HttpDelete("{clientId:int}/trips/{tripId:int}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteReservation(int clientId, int tripId)
    {
        try
        {
            var result = await _tripService.DeleteReservationAsync(clientId, tripId);
            return Ok(result);
        }
        catch(ReservationDoesNotExistException ex)
        {
            return NotFound(ex.Message);
        }
    }
}