using Domain.Data.BaseTypes;

namespace Domain.Data.Entities;

public class Dokument : BaseEntity
{
    public          Guid            NutzungseinheitId { get; set; }
    public          Nutzungseinheit Nutzungseinheit   { get; set; } = null!;
    public required string          Dateiname         { get; set; }
    public required string          ContentType       { get; set; }
    public          DateTimeOffset  LastModified      { get; set; }
    public          long            GrößeBytes        { get; set; }
    public          string          StorageKey        => $"{nameof(Entities.Nutzungseinheit)}/{NutzungseinheitId}/{nameof(Dokument)}/{Id}";
}