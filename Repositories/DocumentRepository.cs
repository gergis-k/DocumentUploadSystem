using Core.Entities;
using Core.IRepositories;
using Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public sealed class DocumentRepository : GenericRepository<Document>, IDocumentRepository
{
    public DocumentRepository(SystemDatabase systemDatabase) : base(systemDatabase) { }

    public async Task<Document?> GetByIdIncludeFilesAsync(int id)
    {
        return await systemDatabase.Documents
            .Include(e => e.UploadedFiles)
            .SingleOrDefaultAsync(e => e.Id == id);
    }
}
