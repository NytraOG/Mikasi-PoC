using Domain.Data.BaseTypes;

namespace Domain.Data.Entities;

public class Dokument : BaseEntity
{
    public          Guid            NutzungseinheitId { get; set; }
    public          Nutzungseinheit Nutzungseinheit   { get; set; } = null!;
    public required string          Dateiname         { get; set; }
    public required string          StorageKey        { get; set; }
    public required string          ContentType       { get; set; }
    public          long            GrößeBytes        { get; set; }
}