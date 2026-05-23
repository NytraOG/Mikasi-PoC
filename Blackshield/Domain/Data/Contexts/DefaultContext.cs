using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.Data.Contexts;

public class DefaultContext(DbContextOptions<DefaultContext> options) : DbContext(options)
{
    public DbSet<Wirtschaftseinheit> Wirtschaftseinheiten { get; set; }
    public DbSet<Nutzungseinheit>    Nutzungseinheiten    { get; set; }
    public DbSet<Etage>              Etagen               { get; set; }
}