using Domain.Data.BaseTypes;

namespace Domain.Data.Entities;

public class Etage : BaseEntity
{
    private readonly List<Nutzungseinheit>                nutzungseinheiten = [];
    public required  string                               Bezeichnung          { get; set; }
    public           Guid                                 WirtschaftseinheitId { get; set; }
    public required  Wirtschaftseinheit                   Wirtschaftseinheit   { get; set; }
    public           IReadOnlyCollection<Nutzungseinheit> Nutzungseinheiten    => nutzungseinheiten;

    public void AddNutzungseinheit(Nutzungseinheit nutzungseinheit) => nutzungseinheiten.Add(nutzungseinheit);
}