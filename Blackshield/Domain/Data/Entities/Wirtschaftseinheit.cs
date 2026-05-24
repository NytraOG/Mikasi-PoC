using Domain.Data.BaseTypes;

namespace Domain.Data.Entities;

public class Wirtschaftseinheit : BaseEntity
{
    private readonly List<Etage>                          etagen            = [];
    private readonly List<Nutzungseinheit>                nutzungseinheiten = [];
    public required  string                               Bezeichnung       { get; set; }
    public required  string                               Straße            { get; set; }
    public required  string                               Hausnummer        { get; set; }
    public required  string                               PLZ               { get; set; }
    public required  string                               Ort               { get; set; }
    public           IReadOnlyCollection<Nutzungseinheit> Nutzungseinheiten => nutzungseinheiten;
    public           IReadOnlyCollection<Etage>           Etagen            => etagen;
}