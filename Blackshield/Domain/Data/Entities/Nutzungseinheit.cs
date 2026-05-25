using Domain.Data.BaseTypes;
using Domain.Data.Enums;

namespace Domain.Data.Entities;

public class Nutzungseinheit : BaseEntity
{
    public required string      Bezeichnung       { get; set; }
    public          Guid        EtageId           { get; set; }
    public          Etage?      Etage             { get; set; }
    public          int         AnzahlZimmer      { get; set; }
    public          decimal     Wohnfläche        { get; set; }
    public          Wohnungstyp Typ               { get; set; }
    public          Mietstatus  Status            { get; set; }
    public          Preis       Kaltmiete         { get; set; }
    public          Preis       Nebenkosten       { get; set; }
    public          Preis       Heizkosten        { get; set; }
    public          Preis       Kaution           { get; set; }
    public          DateOnly    FrühesterEinzugAb { get; set; }
    public          string?     Beschreibung      { get; set; }

    public string? GetFullAddress() => Etage?.Wirtschaftseinheit?.GetFullAddress();
}