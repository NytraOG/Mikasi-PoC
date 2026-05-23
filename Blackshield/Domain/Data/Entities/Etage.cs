using Domain.Data.BaseTypes;

namespace Domain.Data.Entities;

public class Etage : BaseEntity
{
    private readonly List<Nutzungseinheit>                nutzungseinheiten = new();
    public required  string                               Bezeichnung       { get; set; }
    public           IReadOnlyCollection<Nutzungseinheit> Nutzungseinheiten => nutzungseinheiten;
}