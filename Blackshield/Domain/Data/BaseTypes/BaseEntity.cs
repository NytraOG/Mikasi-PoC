namespace Domain.Data.BaseTypes;

public abstract class BaseEntity
{
    public Guid           Id        { get; private set; } = Guid.CreateVersion7();
    public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.UtcNow;
}