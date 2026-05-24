using Domain.Data.BaseTypes;

namespace Domain.Data.Entities;

public class Wirtschaftseinheit : BaseEntity
{
    private readonly List<Etage>                etagen = [];
    public required  string                     Bezeichnung { get; set; }
    public required  string                     Straße      { get; set; }
    public required  string                     Hausnummer  { get; set; }
    public required  string                     PLZ         { get; set; }
    public required  string                     Ort         { get; set; }
    public           IReadOnlyCollection<Etage> Etagen      => etagen;

    public void AddEtage(Etage etage) => etagen.Add(etage);
}