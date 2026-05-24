using System.Text;
using Application.Configuration;
using Application.UserCase.Adapters.User;
using Application.UserCase.Ports.User;
using Domain.Ports;
using Domain.Ports.Repositories;
using Domain.Ports.Security;
using Infraestructure.Configuration;
using Infraestructure.Persistence;
using Infraestructure.Persistence.Repositories;
using Infraestructure.Persistence.Repositories.Entities;
using Infraestructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoleRepository = Infraestructure.Persistence.Repositories.Entities.RoleRepository;

var builder = WebApplication.CreateBuilder(args);

/*
 * DbContext
 */
builder.Services.AddDbContext<AppDbContext>(options =>
{
    var strings = builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(
        strings,
        ServerVersion.AutoDetect(strings)
    );
});

/*
 * Json Web Token
 */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"] 
                                       ?? throw new InvalidOperationException("SecretKey no encontrado")))
        };
    });

/*
 * Dependencias
 */
builder.Services.AddApplication();
builder.Services.AddInfrastructure();


/*
 * Swagger
 */
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
