using Domain.Data.Contexts;
using Domain.Data.Entities;
using Domain.Viewmodels;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services;

public class WohnobjektService
{
    private readonly DefaultContext dbContext;

    public WohnobjektService(DefaultContext dbContext) => this.dbContext = dbContext;

    public async Task SaveWohnobjektAsync(NeuesWohnobjektViewmodel model)
    {
        //todo Validation

        var wirtschaftseinheit = await FindOrCreateWirtschaftseinheit(model);
        var etage              = FindOrCreateEtage(model, wirtschaftseinheit);

        CreateNutzungseinheit(model, etage);

        await dbContext.SaveChangesAsync();
    }

    public async Task<Nutzungseinheit> LoadNutzungseinheitByIdAsync(Guid id) => await dbContext.Nutzungseinheiten.FirstOrDefaultAsync(ne => ne.Id == id);

    public Wirtschaftseinheit[] LoadAllWirtschaftseinheiten() => dbContext.Wirtschaftseinheiten.ToArray();

    public Task<Nutzungseinheit[]> LoadAllNutzungseinheitenAsync() => dbContext.Nutzungseinheiten.ToArrayAsync();

    private async Task<Wirtschaftseinheit> FindOrCreateWirtschaftseinheit(NeuesWohnobjektViewmodel model)
    {
        var wirtschaftseinheit = await dbContext.Wirtschaftseinheiten
                                                .Include(we => we.Etagen)
                                                .ThenInclude(etage => etage.Nutzungseinheiten)
                                                .FirstOrDefaultAsync(we => we.Straße == model.Straße &&
                                                                           we.Hausnummer == model.Hausnummer &&
                                                                           we.PLZ == model.PLZ &&
                                                                           we.Ort == model.Ort);

        if (wirtschaftseinheit is null)
        {
            wirtschaftseinheit = new Wirtschaftseinheit
            {
                Bezeichnung = model.BezeichnungWirtschaftseinheit,
                Straße      = model.Straße,
                Hausnummer  = model.Hausnummer,
                PLZ         = model.PLZ,
                Ort         = model.Ort
            };

            dbContext.Wirtschaftseinheiten.Add(wirtschaftseinheit);
        }

        return wirtschaftseinheit;
    }

    private Etage FindOrCreateEtage(NeuesWohnobjektViewmodel model, Wirtschaftseinheit wirtschaftseinheit)
    {
        var etage = wirtschaftseinheit.Etagen.FirstOrDefault(e => e.Bezeichnung == model.Etage);

        if (etage is null)
        {
            etage = new Etage { Bezeichnung = model.Etage!.Trim() };

            wirtschaftseinheit.AddEtage(etage);
            dbContext.Etagen.Add(etage);
        }

        return etage;
    }

    private void CreateNutzungseinheit(NeuesWohnobjektViewmodel model, Etage etage)
    {
        var nutzungseinheit = etage.Nutzungseinheiten.FirstOrDefault(ne => ne.Bezeichnung == model.BezeichnungNutzungseinheit);

        if (nutzungseinheit is null)
        {
            nutzungseinheit = new Nutzungseinheit
            {
                Bezeichnung       = model.BezeichnungNutzungseinheit,
                Beschreibung      = model.Beschreibung,
                AnzahlZimmer      = model.AnzahlZimmer,
                Wohnfläche        = model.Wohnfläche,
                Typ               = model.Typ,
                Kaltmiete         = new Preis(model.Kaltmiete),
                Nebenkosten       = new Preis(model.Nebenkosten),
                Heizkosten        = new Preis(model.Heizkosten),
                Kaution           = new Preis(model.Kaution),
                FrühesterEinzugAb = model.FrühesterEinzugAb
            };

            etage.AddNutzungseinheit(nutzungseinheit);
            dbContext.Nutzungseinheiten.Add(nutzungseinheit);
        }
    }
}