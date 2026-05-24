using Application.UserCase.User;
using Domain.DTO_s.AuthDTO;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace L10MauricioCV.PortsAdapters.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly  CreateUserUseCase _createUserUseCase;

    public UsersController(CreateUserUseCase createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequestDTO request, 
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
        catch (ArgumentException ex)
        {
            // Captura validaciones de tus Value Objects
            return BadRequest(ex.Message);
        }
        catch (Exception)
        {
            return StatusCode(500, "Error interno del servidor");
        }
    }
}