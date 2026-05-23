using System.Reflection;
using Domain.Data.Entities;
using Domain.Data.Entities.Security;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data.Contexts;

public class DefaultContext(DbContextOptions<DefaultContext> options) : DbContext(options)
{
    public DbSet<Wirtschaftseinheit> Wirtschaftseinheiten => Set<Wirtschaftseinheit>();
    public DbSet<Nutzungseinheit>    Nutzungseinheiten    => Set<Nutzungseinheit>();
    public DbSet<Etage>              Etagen               => Set<Etage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("blackshield");
        modelBuilder.Entity<ApplicationUser>().ToTable("users", schema: "identity");
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        configurationBuilder.Properties<string>().HaveMaxLength(500);
        configurationBuilder.Properties<DateTimeOffset>();
    }
}