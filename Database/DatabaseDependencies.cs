using Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database;

public static class DatabaseDependencies
{
    public static IServiceCollection AddDatabaseDependencies(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<SystemDatabase>(opt => opt.UseSqlServer(connectionString));

        return services;
    }
}
