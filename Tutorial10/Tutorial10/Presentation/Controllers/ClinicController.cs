using Microsoft.AspNetCore.Mvc;
using Tutorial10.Application.DTOs;
using Tutorial10.Application.Exceptions;
using Tutorial10.Application.Services;

namespace Tutorial10.Presentation.Controllers;

[ApiController]
[Route("api/")]
public class ClinicController(IClinicService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> IssuePrescriptionAsync([FromBody] NewPrescriptionRequest request)
    {
        try
        {
            await service.IssuePrescriptionAsync(request);
            return Ok();
        }
        catch (Exception ex) when (ex is MedicamentsNumberException or DueDateException)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex) when (ex is DoctorDoesNotExistsException or MedicamentsNumberException)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}