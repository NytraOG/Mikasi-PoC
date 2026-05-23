using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Data.Configurations;

internal sealed class WirtschaftseinheitConfiguration : BaseEntityConfiguration<Wirtschaftseinheit>
{
    public override void Configure(EntityTypeBuilder<Wirtschaftseinheit> builder)
    {
        base.Configure(builder);

        builder.Property(we => we.Bezeichnung).HasMaxLength(200);
        builder.Property(we => we.Straße).HasMaxLength(150);
        builder.Property(we => we.Hausnummer).HasMaxLength(20);
        builder.Property(we => we.PLZ).HasMaxLength(10);
        builder.Property(we => we.Ort).HasMaxLength(150);

        builder.HasMany(we => we.Nutzungseinheiten)
               .WithOne(ne => ne.Wirtschaftseinheit)
               .HasForeignKey(ne => ne.WirtschaftseinheitId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}