using Domain.Data.Contexts;
using Domain.Data.Entities;
using Domain.Extensions;
using Domain.Services.Abstractions;
using Domain.Viewmodels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Domain.Services;

public class WohnobjektService
{
    private readonly DefaultContext   dbContext;
    private readonly IDocumentStorage documentStorage;
    private readonly HttpContext      httpContext;

    public WohnobjektService(DefaultContext dbContext, IHttpContextAccessor httpContextAccessor, IDocumentStorage documentStorage)
    {
        this.dbContext       = dbContext;
        this.documentStorage = documentStorage;
        httpContext          = httpContextAccessor.HttpContext;
    }

    public Task<Nutzungseinheit?> LoadNutzungseinheitByIdAsync(Guid id) => dbContext.Nutzungseinheiten
                                                                                    .Include(ne => ne.Dokumente)
                                                                                    .Include(ne => ne.Etage)
                                                                                    .ThenInclude(e => e.Wirtschaftseinheit)
                                                                                    .FirstOrDefaultAsync(ne => ne.Id == id);

    public Wirtschaftseinheit[] LoadAllWirtschaftseinheiten() => dbContext.Wirtschaftseinheiten.ToArray();

    public async Task<Wirtschaftseinheit[]> LoadAllWirtschaftseinheitenOfCurrentUser() => await dbContext.Wirtschaftseinheiten.ToArrayAsync();

    public Task<Nutzungseinheit[]> LoadAllNutzungseinheitenAsync() => dbContext.Nutzungseinheiten.ToArrayAsync();

    public Task<Nutzungseinheit[]> LoadAllNutzungseinheitenOfCurrentUserAsync()
    {
        var userName = httpContext.User?.Identity?.Name;

        return dbContext.Nutzungseinheiten.Where(ne => ne.CreatedBy == userName).ToArrayAsync();
    }

    public async Task CreateWohnobjektAsync(WohnobjektViewmodel model)
    {
        //todo Validation

        var wirtschaftseinheit = await FindOrCreateWirtschaftseinheit(model);
        var etage              = FindOrCreateEtage(model, wirtschaftseinheit);
        var nutzungseinheit    = CreateNutzungseinheit(model, etage);

        await PersistDataAsync(model, nutzungseinheit);
    }

    private async Task PersistDataAsync(WohnobjektViewmodel model, Nutzungseinheit nutzungseinheit)
    {
        var tasks = new List<Task>();

        StoreDocuments(model, nutzungseinheit, tasks);

        var saveTask = dbContext.SaveChangesAsync();
        tasks.Add(saveTask);

        try
        {
            await Task.WhenAll(tasks);
        }
        catch (Exception e)
        {
            //documentStorage.Delete angelegte dokumente
            dbContext.ChangeTracker.Clear();
            throw;
        }
    }

    private void StoreDocuments(WohnobjektViewmodel model, Nutzungseinheit nutzungseinheit, List<Task> tasks)
    {

        foreach (var browserFile in model.Dokumente)
        {
            var dokument = new Dokument
            {
                Dateiname         = browserFile.Name,
                ContentType       = browserFile.ContentType,
                LastModified      = browserFile.LastModified,
                GrößeBytes        = browserFile.Size,
                NutzungseinheitId = nutzungseinheit.Id,
                Nutzungseinheit   = nutzungseinheit
            };

            dbContext.Dokumente.Add(dokument);

            var uploadTask = documentStorage.UploadAsync(dokument.StorageKey, browserFile.OpenReadStream(Konstanten.MaxDocumentBytes), dokument.ContentType);
            tasks.Add(uploadTask);
        }
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
                                                .FirstOrDefaultAsync(we => we.Id == model.WirtschaftseinheitId);

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

    private Nutzungseinheit CreateNutzungseinheit(WohnobjektViewmodel model, Etage etage)
    {
        var nutzungseinheit = etage.Nutzungseinheiten.FirstOrDefault(ne => ne.Bezeichnung == model.BezeichnungNutzungseinheit);

        if (nutzungseinheit is null)
        {
            nutzungseinheit = model.ToNutzungseinheit(etage);

            etage.AddNutzungseinheit(nutzungseinheit);
            dbContext.Nutzungseinheiten.Add(nutzungseinheit);
        }

        return nutzungseinheit;
    }
}