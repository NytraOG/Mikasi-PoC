using System.Reflection;
using Domain.Data.Entities;
using Domain.Data.Entities.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data.Contexts;

public class DefaultContext(DbContextOptions<DefaultContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Wirtschaftseinheit> Wirtschaftseinheiten => Set<Wirtschaftseinheit>();
    public DbSet<Nutzungseinheit>    Nutzungseinheiten    => Set<Nutzungseinheit>();
    public DbSet<Etage>              Etagen               => Set<Etage>();
    public DbSet<Dokument>           Dokumente            => Set<Dokument>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("blackshield");

        modelBuilder.Entity<ApplicationUser>().ToTable("users", "identity");
        modelBuilder.Entity<IdentityRole>().ToTable("roles", "identity");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("user_roles", "identity");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("user_claims", "identity");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("user_logins", "identity");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("user_tokens", "identity");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("role_claims", "identity");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        configurationBuilder.Properties<string>().HaveMaxLength(500);
        configurationBuilder.Properties<DateTimeOffset>();
    }
}