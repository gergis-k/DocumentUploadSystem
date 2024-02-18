namespace Core.Entities.Bases;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public byte[] Version { get; set; } = [];
}
