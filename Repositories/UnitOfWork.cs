using Core.Entities.Bases;
using Core.IRepositories;
using Core.IRepositories.Generics;

namespace Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
    public IGenericRepository<BaseEntity> GenericRepository { get; private set; }

    public IDocumentRepository DocumentRepository { get; private set; }

    public IUploadedFileRepository UploadedFileRepository { get; private set; }

    public IAllowedExtensionRepository AllowedExtensionRepository { get; private set; }

    public UnitOfWork(
        IGenericRepository<BaseEntity> genericRepository,
        IDocumentRepository documentRepository,
        IUploadedFileRepository uploadedFileRepository,
        IAllowedExtensionRepository allowedExtensionRepository)
    {
        GenericRepository = genericRepository;
        DocumentRepository = documentRepository;
        UploadedFileRepository = uploadedFileRepository;
        AllowedExtensionRepository = allowedExtensionRepository;
    }
}
