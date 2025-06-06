using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tutorial10.Application.Contracts.Requests;
using Tutorial10.Application.Services.Abstraction;

namespace Tutorial10.Presentation.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController: ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterUserRequest request)
    {
        await _userService.RegisterUserAsync(request);
        return Ok();
    }
    
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserRequest request)
    {
        return Ok(await _userService.LoginUserAsync(request));
    }
    
    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost]
    [Route("refresh")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RefreshUserAsync([FromBody] RefreshTokenRequest request)
    {
        return Ok(await _userService.RefreshTokensAsync(request));
    }
}