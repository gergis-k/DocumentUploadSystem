using Core.Entities;
using Core.IRepositories.Generics;

namespace Core.IRepositories;

public interface IUploadedFileRepository : IGenericRepository<UploadedFile>
{
    Task<UploadedFile?> GetByFileNameAsync(string fileName);
}
