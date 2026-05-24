using Application.DTO_s.AuthDTO;
using Application.UserCase.Adapters.User;
using Application.UserCase.Ports.User;
using Domain.ValueObjects;
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

    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDto request, 
        CancellationToken ct)
    {
        try 
        {
            // Convertimos strings a Value Objects de Dominio
            var username = new Username(request.Username);
            var email = new Email(request.Email);

            await _createUserUseCase.Execute(
                username, 
                email, 
                request.Password, 
                ct);

            return Ok(new { message = "Usuario creado exitosamente" });
        }
        catch (InvalidOperationException ex)
        {
            // Captura validaciones de tus Value Objects
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequestDto request,
        CancellationToken ct)
    {
        try
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
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return Unauthorized("Credenciales inválidas");
        }
    }
}