using Domain.Data.Contexts;
using Domain.Data.Entities;
using Domain.Extensions;
using Domain.Viewmodels;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services;

public class WohnobjektService
{
    private readonly DefaultContext dbContext;

    public WohnobjektService(DefaultContext dbContext) => this.dbContext = dbContext;

    public async Task CreateWohnobjektAsync(WohnobjektViewmodel model)
    {
        //todo Validation

        var wirtschaftseinheit = await FindOrCreateWirtschaftseinheit(model);
        var etage              = FindOrCreateEtage(model, wirtschaftseinheit);

        CreateNutzungseinheit(model, etage);

        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateWohnobjectAsync(WohnobjektViewmodel viewmodel)
    {
        var wirtschaftseinheit = await FindWirtschaftseinheitOrDefaultAsync(viewmodel);

        UpdateWirtschaftseinheit(viewmodel, wirtschaftseinheit);

        var etage = UpdateEtage(viewmodel, wirtschaftseinheit);

        UpdateNutzungseinheit(viewmodel, etage);

        await dbContext.SaveChangesAsync();
    }

    private static void UpdateNutzungseinheit(WohnobjektViewmodel viewmodel, Etage etage)
    {
        var nutzungseinheit = etage.Nutzungseinheiten.FirstOrDefault(ne => ne.Id == viewmodel.NutzungseinheitId);

        if (nutzungseinheit is null)
            throw new Exception("Nutzungseinheit weg");

        nutzungseinheit.UpdateWith(viewmodel);
    }

    private Etage UpdateEtage(WohnobjektViewmodel viewmodel, Wirtschaftseinheit? wirtschaftseinheit)
    {
        var etage = wirtschaftseinheit.Etagen.FirstOrDefault(e => e.Bezeichnung == viewmodel.Etage);

        if (etage is null)
        {
            etage = CreateEtage(viewmodel, wirtschaftseinheit);

            dbContext.Etagen.Add(etage);
        }

        return etage;
    }

    private static void UpdateWirtschaftseinheit(WohnobjektViewmodel viewmodel, Wirtschaftseinheit? wirtschaftseinheit)
    {
        if (wirtschaftseinheit is null)
            throw new Exception("Wirtschaftseinheit weg");

        wirtschaftseinheit.Bezeichnung = viewmodel.BezeichnungWirtschaftseinheit!;
        wirtschaftseinheit.Straße      = viewmodel.Straße!;
        wirtschaftseinheit.Hausnummer  = viewmodel.Hausnummer!;
        wirtschaftseinheit.PLZ         = viewmodel.PLZ!;
        wirtschaftseinheit.Ort         = viewmodel.Ort!;
    }

    public Task<Nutzungseinheit?> LoadNutzungseinheitByIdAsync(Guid id) => dbContext.Nutzungseinheiten
                                                                                    .Include(ne => ne.Etage)
                                                                                    .ThenInclude(e => e.Wirtschaftseinheit)
                                                                                    .FirstOrDefaultAsync(ne => ne.Id == id);

    public Wirtschaftseinheit[] LoadAllWirtschaftseinheiten() => dbContext.Wirtschaftseinheiten.ToArray();

    public Task<Nutzungseinheit[]> LoadAllNutzungseinheitenAsync() => dbContext.Nutzungseinheiten.ToArrayAsync();

    private async Task<Wirtschaftseinheit> FindOrCreateWirtschaftseinheit(WohnobjektViewmodel viewmodel)
    {
        var wirtschaftseinheit = await FindWirtschaftseinheitOrDefaultAsync(viewmodel);

        if (wirtschaftseinheit is not null)
            return wirtschaftseinheit;

        wirtschaftseinheit = new Wirtschaftseinheit
        {
            Bezeichnung = viewmodel.BezeichnungWirtschaftseinheit!,
            Straße      = viewmodel.Straße!,
            Hausnummer  = viewmodel.Hausnummer!,
            PLZ         = viewmodel.PLZ!,
            Ort         = viewmodel.Ort!
        };

        dbContext.Wirtschaftseinheiten.Add(wirtschaftseinheit);

        return wirtschaftseinheit;
    }

    private async Task<Wirtschaftseinheit?> FindWirtschaftseinheitOrDefaultAsync(WohnobjektViewmodel model)
    {
        var wirtschaftseinheit = await dbContext.Wirtschaftseinheiten
                                                .Include(we => we.Etagen)
                                                .ThenInclude(etage => etage.Nutzungseinheiten)
                                                .FirstOrDefaultAsync(we => we.Straße == model.Straße &&
                                                                           we.Hausnummer == model.Hausnummer &&
                                                                           we.PLZ == model.PLZ &&
                                                                           we.Ort == model.Ort);
        return wirtschaftseinheit;
    }

    private Etage FindOrCreateEtage(WohnobjektViewmodel viewmodel, Wirtschaftseinheit wirtschaftseinheit)
    {
        var etage = wirtschaftseinheit.Etagen.FirstOrDefault(e => e.Bezeichnung == viewmodel.Etage);

        if (etage is null)
        {
            etage = CreateEtage(viewmodel, wirtschaftseinheit);

            dbContext.Etagen.Add(etage);
        }

        return etage;
    }

    private static Etage CreateEtage(WohnobjektViewmodel viewmodel, Wirtschaftseinheit wirtschaftseinheit)
    {
        var etage = new Etage { Wirtschaftseinheit = wirtschaftseinheit, Bezeichnung = viewmodel.Etage!.Trim() };
        wirtschaftseinheit.AddEtage(etage);
        return etage;
    }

    private void CreateNutzungseinheit(WohnobjektViewmodel model, Etage etage)
    {
        var nutzungseinheit = etage.Nutzungseinheiten.FirstOrDefault(ne => ne.Bezeichnung == model.BezeichnungNutzungseinheit);

        if (nutzungseinheit is null)
        {
            nutzungseinheit = model.ToNutzungseinheit(etage);

            etage.AddNutzungseinheit(nutzungseinheit);
            dbContext.Nutzungseinheiten.Add(nutzungseinheit);
        }
    }
}