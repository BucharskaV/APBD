﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Warehouse.API.Contracts.Requests;
using Warehouse.API.Exceptions;
using Warehouse.API.Services;

namespace Warehouse.API.Controllers;

[ApiController]
[Route("api/warehouse")]
public class WarehouseController : ControllerBase
{
    private readonly IWarehouseService _warehouseService;
    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterProductInWarehouseAsync([FromBody] ProductWarehouseRequest request,
        CancellationToken token = default)
    {
        try
        {
            var id = await _warehouseService.RegisterProductWarehouseAsync(request, token);
            return Ok(new { Id = id });
        }
        catch (AmountMustBeGreaterThanZeroException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (AppropriateOrderNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (OrderIsCompletedException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ProductDoesNotExistException ex)
        {
            return NotFound(ex.Message);
        }
        catch (WarehouseDoesNotExistException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
    
    [HttpPost("procedure")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterProductWarehouseWithProcedureAsync([FromBody] ProductWarehouseRequest request, CancellationToken token = default)
    {
        try
        {
            var id = await _warehouseService.RegisterProductWarehouseWithProcedureAsync(request, token);
            return Ok(new { Id = id });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    
}