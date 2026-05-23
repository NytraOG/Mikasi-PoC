using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Data.Configurations;

internal sealed class EtageConfiguration : BaseEntityConfiguration<Etage>
{
    public override void Configure(EntityTypeBuilder<Etage> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Bezeichnung).HasMaxLength(200);

        builder.HasMany(e => e.Nutzungseinheiten)
               .WithOne(ne => ne.Etage)
               .HasForeignKey(ne => ne.EtageId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}