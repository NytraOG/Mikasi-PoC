namespace Domain.Data.Entities;

public class Etage : BaseEntity
{
    public string          Bezeichnung       { get; set; }
    public Nutzungseinheit InNutzungseinheit { get; set; }
}