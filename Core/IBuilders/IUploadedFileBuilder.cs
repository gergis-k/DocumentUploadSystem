using Core.Entities;
using Core.IBuilders.Generics;

namespace Core.IBuilders;

public interface IUploadedFileBuilder : IBuilder<UploadedFile>
{
    IUploadedFileBuilder SetFileName(string fileName);

    IUploadedFileBuilder WithFakeName(string? fakeName = null);

    IUploadedFileBuilder WithContentType(string contentType);
}