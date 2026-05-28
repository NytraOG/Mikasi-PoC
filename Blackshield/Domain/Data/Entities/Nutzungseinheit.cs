using Domain.Data.BaseTypes;
using Domain.Data.Enums;

namespace Domain.Data.Entities;

public class Nutzungseinheit : BaseEntity
{
    private readonly List<Dokument>                dokumente = [];
    public required  string                        Bezeichnung       { get; set; }
    public           Guid                          EtageId           { get; set; }
    public required  Etage                         Etage             { get; set; }
    public           int                           AnzahlZimmer      { get; set; }
    public           decimal                       Wohnfläche        { get; set; }
    public           Wohnungstyp                   Typ               { get; set; }
    public           Mietstatus                    Status            { get; set; }
    public           Preis                         Kaltmiete         { get; set; }
    public           Preis                         Nebenkosten       { get; set; }
    public           Preis                         Heizkosten        { get; set; }
    public           Preis                         Kaution           { get; set; }
    public           DateOnly                      FrühesterEinzugAb { get; set; }
    public           string?                       Beschreibung      { get; set; }
    public           decimal                       Warmmiete         => Kaltmiete.Betrag + Nebenkosten.Betrag + Heizkosten.Betrag;
    public           IReadOnlyCollection<Dokument> Dokumente         => dokumente;

    public void AddDokument(Dokument dokument) => dokumente.Add(dokument);

    public string? GetFullAddress() => Etage?.Wirtschaftseinheit?.GetFullAddress();
}