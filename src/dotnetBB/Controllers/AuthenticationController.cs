using System.Net;
using dotnetBB.Models.Request;
using dotnetBB.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnetBB.Controllers;

[Controller]
public class AuthenticationController : ControllerBase
{
    private readonly UserService _userService;

    public AuthenticationController(UserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost]
    [Authorize(Policy = "Anonymous")]
    [Route("/auth/login")]
    public async Task<ActionResult> LogInAsync(LoginModel model)
    {
        try
        {
            await _userService.LogInAsync(model.EmailOrUsername, model.Password, HttpContext);
        }
        catch (ArgumentException)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, "Incorrect credentials.");
        }

        return Ok();
    }

    [HttpPost]
    [Authorize]
    [Route("/auth/logout")]
    public async Task<ActionResult> LogOutAsync()
    {
        await HttpContext.SignOutAsync();
        return Ok();
    }

    [HttpPost]
    [Authorize(Policy = "Anonymous")]
    [Route("/auth/register")]
    public async Task<ActionResult> RegisterAsync(RegistrationModel model)
    {
        try
        {
            var user = await _userService.CreateAsync(model.Username, model.Email, model.Password);
            await _userService.LogInAsync(user, HttpContext);
        }
        catch (ArgumentException e)
        {
            return StatusCode((int)HttpStatusCode.Forbidden, e.Message);
        }

        return Ok();
    }
}
