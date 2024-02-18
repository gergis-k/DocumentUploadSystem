using Core.Entities;
using Core.IRepositories;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public sealed class AllowedExtensionRepository : GenericRepository<AllowedExtension>, IAllowedExtensionRepository
{
    public AllowedExtensionRepository(SystemDatabase systemDatabase) : base(systemDatabase) { }

    public async Task<AllowedExtension?> GetByNameAsNoTrackingAsync(string name)
    {
        return await systemDatabase.AllowedExtensions
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Extension == name.ToUpper());
    }

    public async Task<AllowedExtension?> GetByNameAsync(string name)
    {
        return await systemDatabase.AllowedExtensions
            .AsTracking()
            .SingleOrDefaultAsync(e => e.Extension == name.ToUpper());
    }
}
