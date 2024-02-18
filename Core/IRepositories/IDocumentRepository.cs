using Core.Entities;
using Core.IRepositories.Generics;

namespace Core.IRepositories;

public interface IDocumentRepository : IGenericRepository<Document>
{
    Task<Document?> GetByIdIncludeFilesAsync(int id);
}
