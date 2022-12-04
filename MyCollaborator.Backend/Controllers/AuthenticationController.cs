using Microsoft.AspNetCore.Mvc;
using MyCollaborator.Backend.Services.Interfaces;
using MyCollaborator.Shared.DTOs;

namespace MyCollaborator.Backend.Controllers;

[ApiController]
[Route("api/myCollaborator/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationQuery query)
    {
        try
        {
            return Ok(await _authenticationService.AuthenticateAsync(query));
        }
        catch (Exception e)
        {
            var response = new Response<Exception>(Status.ERROR, e.Message, e);
            return BadRequest(response);
        }
    }
}