using Domain.Ports;
using Domain.Ports.Persistence.Repositories;
using Domain.Ports.Security;
using Infraestructure.Persistence.Repositories;
using Infraestructure.Persistence.Repositories.Entities;
using Infraestructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthSecurity, AuthSecurity>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGRepository<>), typeof(GRepository<>));

        return services;
    }
}