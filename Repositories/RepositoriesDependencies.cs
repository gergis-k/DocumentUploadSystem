using Core.IRepositories;
using Core.IRepositories.Generics;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories;

public static class RepositoriesDependencies
{
    public static IServiceCollection AddRepositoriesDependencies(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IDocumentRepository, DocumentRepository>();
        services.AddScoped<IUploadedFileRepository, UploadedFileRepository>();
        services.AddScoped<IAllowedExtensionRepository, AllowedExtensionRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
