namespace Domain.Data.BaseTypes;

public class AuditableEntity
{
    public DateTimeOffset? UpdatedAt { get; }
    public string?         CreatedBy { get; }
    public string?         UpdatedBy { get; }
}