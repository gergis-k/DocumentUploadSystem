using Core.Entities;
using Core.IRepositories.Generics;

namespace Core.IRepositories;

public interface IAllowedExtensionRepository : IGenericRepository<AllowedExtension>
{
    Task<AllowedExtension?> GetByNameAsync(string name);

    Task<AllowedExtension?> GetByNameAsNoTrackingAsync(string name);
}
