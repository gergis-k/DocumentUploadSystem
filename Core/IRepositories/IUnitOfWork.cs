using Core.Entities.Bases;
using Core.IRepositories.Generics;

namespace Core.IRepositories;

public interface IUnitOfWork
{
    public IGenericRepository<BaseEntity> GenericRepository { get; }
    public IDocumentRepository DocumentRepository { get; }
    public IUploadedFileRepository UploadedFileRepository { get; }
    public IAllowedExtensionRepository AllowedExtensionRepository { get; }
}
