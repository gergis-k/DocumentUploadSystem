using Core.Constants;
using Core.Entities.Bases;

namespace Core.Entities;

public sealed class Document : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Directory { get; set; }

    public PriorityTypes Priority { get; set; }

    public DateTime? DueDate { get; set; }

    public ICollection<UploadedFile> UploadedFiles { get; set; } = [];
}
