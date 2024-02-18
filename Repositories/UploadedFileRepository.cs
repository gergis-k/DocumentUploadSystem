using Core.Entities;
using Core.IRepositories;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public sealed class UploadedFileRepository : GenericRepository<UploadedFile>, IUploadedFileRepository
{
    public UploadedFileRepository(SystemDatabase systemDatabase) : base(systemDatabase) { }

    public async Task<UploadedFile?> GetByFileNameAsync(string fileName)
    {
        return await systemDatabase.UploadedFiles.SingleOrDefaultAsync(e => e.FileName == fileName);
    }
}
