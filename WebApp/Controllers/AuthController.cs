using System.Security.Claims;
using Application.Dtos;
using Application.Features.Auth.Commands.CreateToken;
using Application.Features.Auth.Commands.GenerateAndSaveRefreshToken;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Logout;
using Application.Features.Auth.Commands.RefreshToken;
using Application.Features.Auth.Commands.Register;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

[Route("api/auth/")]
[ApiController]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserDto request)
    {
        var registerUserCommand = new RegisterUserCommand()
        {
            Username = request.Username,
            Password = request.Password
        };

        var user = await mediator.Send(registerUserCommand);
        if (user is null)
        {
            return BadRequest("Username already exists");
        }
        
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
    {
        var loginUserCommand = new LoginUserCommand()
        {
            Username = request.Username,
            Password = request.Password
        };
        var result = await mediator.Send(loginUserCommand);
        
        if (result is null)
        {
            return BadRequest("Invalid username or password");
        }
        
        var response = new TokenResponseDto
        {
            AccessToken = await mediator.Send(new CreateTokenCommand() { User = result }),
            RefreshToken = await mediator.Send(new GenerateAndSaveRefreshTokenCommand() { User = result }),
        };
        
        return Ok(response);
    }

    [Authorize(Roles="Admin")]
    [HttpGet]
    public IActionResult AuthenticatedOnlyEndpoint()
    {
        return Ok("You are an Admin");
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
    {
        var result = await mediator.Send(new RefreshTokenCommand()
        {
            RefreshTokenRequestDto = request
        });

        if (result is null)
        {
            return BadRequest("Error while validating refresh token");
        }
        
        var response = new TokenResponseDto
        {
            AccessToken = await mediator.Send(new CreateTokenCommand(){User = result}),
            RefreshToken = await mediator.Send(new GenerateAndSaveRefreshTokenCommand(){User = result})
        };
        
        
        if(response is null || response.AccessToken is null || response.RefreshToken is null)
        {
            return Unauthorized("Invalid refresh token");
        }

        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult> Logout()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null)
        {
            return Unauthorized();
        }

        var result = await mediator.Send(new LogoutUserCommand() { UserId = Guid.Parse(userId) });
        if (!result)
        {
            return BadRequest("Logout failed");
        }

        return Ok("Logout successful");
    }
}