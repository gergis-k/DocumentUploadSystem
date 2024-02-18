using Core.Entities.Bases;

namespace Core.Entities;

public sealed class AllowedExtension : BaseEntity
{
    public string Extension { get; set; } = string.Empty;
}
