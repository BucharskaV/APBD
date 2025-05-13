using Microsoft.AspNetCore.Mvc;
using Test.Exceptions;
using Test.Services;

namespace Test.Controllers;

[ApiController]
[Route("/api/tasks")]
public class TasksControllers : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksControllers(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpDelete]
    [Route("/api/tasks/{id:int}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteProject([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            await _taskService.DeleteProject(id, cancellationToken);
            return Ok();
        }
        catch (NoProjectWithProvidedIdException ex)
        {
            return NotFound(ex.Message);
        }
        catch (EnteredDataIsWrongException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}