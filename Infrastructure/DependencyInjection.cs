using Domain.Abstractions;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var host = Environment.GetEnvironmentVariable("DB_HOST");
        var port = Environment.GetEnvironmentVariable("DB_PORT");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var user = Environment.GetEnvironmentVariable("DB_USER");
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        // Check if any required environment variable is missing
        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(port) || string.IsNullOrEmpty(dbName) ||
            string.IsNullOrEmpty(user) || string.IsNullOrEmpty(password))
        {
            throw new InvalidOperationException("Database connection string environment variables are not fully set. Please check DB_HOST, DB_PORT, DB_NAME, DB_USER, and DB_PASSWORD.");
        }
        
        var connectionString = $"Host={host};Port={port};Username={user};Password={password};Database={dbName}";
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString, 
                sqlOptions => sqlOptions.MigrationsAssembly("WebApp"))
        );
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        
        return services;
    }
}