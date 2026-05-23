namespace Domain.Data.Entities;

public class Wirtschaftseinheit : BaseEntity
{
    public string                Bezeichnung       { get; set; }
    public string                Straße            { get; set; }
    public string                Hausnummer        { get; set; }
    public string                PLZ               { get; set; }
    public string                Ort               { get; set; }
    public List<Nutzungseinheit> Nutzungseinheiten { get; set; } = new();
}