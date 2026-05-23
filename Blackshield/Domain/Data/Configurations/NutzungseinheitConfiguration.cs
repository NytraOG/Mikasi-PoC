using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Data.Configurations;

internal sealed class NutzungseinheitConfiguration : BaseEntityConfiguration<Nutzungseinheit>
{
    public override void Configure(EntityTypeBuilder<Nutzungseinheit> builder)
    {
        base.Configure(builder);

        builder.Property(ne => ne.Bezeichnung).HasMaxLength(200);
        builder.Property(ne => ne.Beschreibung).HasMaxLength(2000);
        builder.Property(ne => ne.Wohnfläche).HasPrecision(8, 2);
        builder.Property(ne => ne.Typ).HasConversion<string>().HasMaxLength(50);
        builder.ComplexProperty(ne => ne.Kaltmiete, ConfigureGeld);
        builder.ComplexProperty(ne => ne.Nebenkosten, ConfigureGeld);
        builder.ComplexProperty(ne => ne.Heizkosten, ConfigureGeld);
        builder.ComplexProperty(ne => ne.Kaution, ConfigureGeld);
    }

    private static void ConfigureGeld(ComplexPropertyBuilder<Preis> b)
    {
        b.Property(g => g.Betrag).HasPrecision(12, 2);
        b.Property(g => g.Währung).HasMaxLength(3).IsFixedLength();
    }
}