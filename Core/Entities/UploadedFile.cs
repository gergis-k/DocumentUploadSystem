using Core.Entities.Bases;

namespace Core.Entities;

public sealed class UploadedFile : BaseEntity
{
    public string? FileName { get; set; }

    public string FakeName { get; set; } = string.Empty;

    public string ContentType { get; set; } = string.Empty;

    public int DocumentId { get; set; }

    public Document Document { get; set; } = new Document();
}
