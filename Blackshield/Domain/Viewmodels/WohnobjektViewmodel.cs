using Domain.Data.Enums;

namespace Domain.Viewmodels;

public class WohnobjektViewmodel
{
    public Guid        WirtschaftseinheitId          { get; set; }
    public int         AnzahlZimmer                  { get; set; }
    public string?     BezeichnungWirtschaftseinheit { get; set; }
    public Guid        NutzungseinheitId             { get; set; }
    public string?     BezeichnungNutzungseinheit    { get; set; }
    public Guid        EtageId                       { get; set; }
    public string?     Etage                         { get; set; }
    public DateOnly    FrühesterEinzugAb             { get; set; }
    public decimal     Heizkosten                    { get; set; }
    public decimal     Kaltmiete                     { get; set; }
    public decimal     Kaution                       { get; set; }
    public decimal     Nebenkosten                   { get; set; }
    public string?     Straße                        { get; set; }
    public string?     Hausnummer                    { get; set; }
    public string?     PLZ                           { get; set; }
    public string?     Ort                           { get; set; }
    public Wohnungstyp Typ                           { get; set; }
    public Mietstatus  Status                        { get; set; }
    public decimal     Wohnfläche                    { get; set; }
    public string?     Beschreibung                  { get; set; }
}