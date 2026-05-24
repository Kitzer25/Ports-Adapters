using System.Collections;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Domain.Ports.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infraestructure.Security;

public class AuthSecurity : IAuthSecurity
{
    private readonly IConfiguration _configuration;
    
    private readonly string _secretKey;
    private readonly int _expiration;
    
    //Condiguración
    private string SecretKey() => 
        _configuration["JwtSettings:SecretKey"]
        ?? throw new 
            InvalidOperationException("Sin clave secreta");

    private string Expiration() =>
        _configuration["JwtSettings:ExpirationMinutes"]
        ?? throw new 
            InvalidOperationException("Sin Valor de expiración");
    
    //Constructor
    public AuthSecurity(IConfiguration configuration)
    {
        _configuration = configuration;
        _secretKey = SecretKey();
        _expiration = int.Parse(Expiration());
    }

    
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username.Value),
            new Claim(ClaimTypes.Email, user.Email.Value)
        };

        foreach (var role in user.UserRoles)
        {
            ((IList)claims).Add(new Claim(ClaimTypes.Role, role.RoleId.ToString()));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_secretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expiration),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}