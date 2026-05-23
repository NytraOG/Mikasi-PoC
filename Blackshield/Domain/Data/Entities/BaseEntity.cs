namespace Domain.Data.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
        Id        =   Guid.NewGuid();
        CreatedAt ??= DateTime.Now;
    }

    public Guid      Id        { get; }
    public DateTime? CreatedAt { get; }
}