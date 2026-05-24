using Application.DTO_s.AuthDTO;
using Domain.ValueObjects;
using Application.UserCase.Ports.User;
using Domain.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace L10MauricioCV.PortsAdapters.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly ILoginUserUseCase _loginUserUseCase;

    public UsersController(
        ICreateUserUseCase createUserUseCase,
        ILoginUserUseCase loginUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
        _loginUserUseCase = loginUserUseCase;
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDto request, 
        CancellationToken ct)
    {
        var username = new Username(request.Username);
        var email = new Email(request.Email);

        await _createUserUseCase.Execute(username, email, request.Password, ct);

        return Ok("Usuario registrado");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken ct)
    {
        var username = new Username(request.Username);

        var result = await _loginUserUseCase.Execute(
            username,
            request.Password,
            ct);

        var response = new LoginResponseDto
        {
            Username = result.Username,
            Token = result.Token
        };

        return Ok(response);
    }
}