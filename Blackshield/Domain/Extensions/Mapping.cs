using Domain.Data.Entities;
using Domain.Viewmodels;

namespace Domain.Extensions;

public static class Mapping
{
    public static void UpdateWith(this Nutzungseinheit nutzungseinheit, WohnobjektViewmodel viewmodel)
    {
        nutzungseinheit.Bezeichnung       = viewmodel.BezeichnungNutzungseinheit!;
        nutzungseinheit.Beschreibung      = viewmodel.Beschreibung;
        nutzungseinheit.AnzahlZimmer      = viewmodel.AnzahlZimmer;
        nutzungseinheit.Wohnfläche        = viewmodel.Wohnfläche;
        nutzungseinheit.Typ               = viewmodel.Typ;
        nutzungseinheit.Kaltmiete         = new Preis(viewmodel.Kaltmiete);
        nutzungseinheit.Nebenkosten       = new Preis(viewmodel.Nebenkosten);
        nutzungseinheit.Heizkosten        = new Preis(viewmodel.Heizkosten);
        nutzungseinheit.Kaution           = new Preis(viewmodel.Kaution);
        nutzungseinheit.FrühesterEinzugAb = viewmodel.FrühesterEinzugAb;
    }

    public static Nutzungseinheit ToNutzungseinheit(this WohnobjektViewmodel viewmodel, Etage etage) => new()
    {
        Etage             = etage,
        Bezeichnung       = viewmodel.BezeichnungNutzungseinheit,
        Beschreibung      = viewmodel.Beschreibung,
        AnzahlZimmer      = viewmodel.AnzahlZimmer,
        Wohnfläche        = viewmodel.Wohnfläche,
        Typ               = viewmodel.Typ,
        Kaltmiete         = new Preis(viewmodel.Kaltmiete),
        Nebenkosten       = new Preis(viewmodel.Nebenkosten),
        Heizkosten        = new Preis(viewmodel.Heizkosten),
        Kaution           = new Preis(viewmodel.Kaution),
        FrühesterEinzugAb = viewmodel.FrühesterEinzugAb
    };

    public static WohnobjektViewmodel ToViewmodel(this Nutzungseinheit nutzungseinheit)
    {
        var viewmodel = new WohnobjektViewmodel
        {
            WirtschaftseinheitId          = nutzungseinheit.Etage.WirtschaftseinheitId,
            BezeichnungWirtschaftseinheit = nutzungseinheit.Etage.Wirtschaftseinheit.Bezeichnung,
            Etage                         = nutzungseinheit.Etage.Bezeichnung,
            BezeichnungNutzungseinheit    = nutzungseinheit.Bezeichnung,
            Straße                        = nutzungseinheit.Etage.Wirtschaftseinheit.Straße,
            Hausnummer                    = nutzungseinheit.Etage.Wirtschaftseinheit.Hausnummer,
            PLZ                           = nutzungseinheit.Etage.Wirtschaftseinheit.PLZ,
            Ort                           = nutzungseinheit.Etage.Wirtschaftseinheit.Ort,
            Beschreibung                  = nutzungseinheit.Beschreibung,
            AnzahlZimmer                  = nutzungseinheit.AnzahlZimmer,
            Wohnfläche                    = nutzungseinheit.Wohnfläche,
            Typ                           = nutzungseinheit.Typ,
            Kaltmiete                     = nutzungseinheit.Kaltmiete.Betrag,
            Nebenkosten                   = nutzungseinheit.Nebenkosten.Betrag,
            Heizkosten                    = nutzungseinheit.Heizkosten.Betrag,
            Kaution                       = nutzungseinheit.Kaution.Betrag,
            FrühesterEinzugAb             = nutzungseinheit.FrühesterEinzugAb,
            NutzungseinheitId             = nutzungseinheit.Id,
            EtageId                       = nutzungseinheit.EtageId
        };

        return viewmodel;
    }
}