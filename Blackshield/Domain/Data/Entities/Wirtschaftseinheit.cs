using Domain.Data.BaseTypes;

namespace Domain.Data.Entities;

public class Wirtschaftseinheit : BaseEntity
{
    private readonly List<Etage>                          etagen            = [];
    private readonly List<Nutzungseinheit>                nutzungseinheiten = [];
    public           string?                              Bezeichnung       { get; set; }
    public           string?                              Straße            { get; set; }
    public           string?                              Hausnummer        { get; set; }
    public           string?                              PLZ               { get; set; }
    public           string?                              Ort               { get; set; }
    public           IReadOnlyCollection<Nutzungseinheit> Nutzungseinheiten => nutzungseinheiten;
    public           IReadOnlyCollection<Etage>           Etagen            => etagen;
}