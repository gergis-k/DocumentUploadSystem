using Database;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace WebAPIs;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDatabaseDependencies(builder.Configuration.GetConnectionString("Default"));

        builder.Services.AddRepositoriesDependencies();

        builder.Services.AddDataProtection();

        var app = builder.Build();

        var scope = app.Services.CreateScope();
        var provider = scope.ServiceProvider;

        var logger = provider.GetRequiredService<ILogger<Program>>();

        try
        {
            var systemDatabase = provider.GetRequiredService<SystemDatabase>();
            await systemDatabase.Database.MigrateAsync();

            logger.LogInformation($"The {nameof(SystemDatabase)} has been updated successfully...");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
