using Domain.Data.BaseTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Domain.Data.Interceptors;

public sealed class AuditInterceptor(IHttpContextAccessor? httpContextAccessor = null) : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData ed, InterceptionResult<int> r, CancellationToken ct = default)
    {
        UpdateModifiedEntries(ed.Context);

        return base.SavingChangesAsync(ed, r, ct);
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateModifiedEntries(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    private void UpdateModifiedEntries(DbContext? context)
    {
        if (context is null) return;

        var now  = DateTimeOffset.UtcNow;
        var user = httpContextAccessor?.HttpContext?.User.Identity?.Name;

        foreach (var entry in context!.ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = user;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = user;
                    break;
            }
        }
    }
}