using Application.Utilities;
using Core.Entities;
using Core.IBuilders;

namespace Application.Builders;

public sealed class UploadedFileBuilder : IUploadedFileBuilder
{
    private readonly UploadedFile uploadedFile = new UploadedFile();

    public UploadedFile Build()
    {
        return uploadedFile;
    }

    public IUploadedFileBuilder SetFileName(string fileName)
    {
        uploadedFile.FileName = NameHelpers.CleanName(fileName);
        return this;
    }

    public IUploadedFileBuilder WithContentType(string contentType)
    {
        uploadedFile.ContentType = contentType;
        return this;
    }

    public IUploadedFileBuilder WithFakeName(string? fakeName = null)
    {
        if (string.IsNullOrEmpty(fakeName))
        {
            uploadedFile.FakeName = $"{Guid.NewGuid()}-{Path.GetRandomFileName()}";
            return this;
        }

        uploadedFile.FakeName = NameHelpers.CleanName(fakeName);
        return this;
    }
}
