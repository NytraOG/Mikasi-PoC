using Domain.Data.Enums;

namespace Domain.Data.Entities;

public class Nutzungseinheit : BaseEntity
{
    public string             Bezeichnung          { get; set; }
    public Etage              InEtage              { get; set; }
    public int                AnzahlZimmer         { get; set; }
    public int                Wohnfläche           { get; set; }
    public Wirtschaftseinheit InWirtschaftseinheit { get; set; }
    public Wohnungstyp        Typ                  { get; set; }
    public decimal            Kaltmiete            { get; set; }
    public decimal            Nebenkosten          { get; set; }
    public decimal            Heizkosten           { get; set; }
    public decimal            Kaution              { get; set; }
    public DateTime           FrühesterEinzugAb    { get; set; }
    public string             Beschreibung         { get; set; }
}