using Domain.Data.Entities;
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
        builder.OwnsOne(ne => ne.Kaltmiete, GeldConfigurieren);
        builder.OwnsOne(ne => ne.Nebenkosten, GeldConfigurieren);
        builder.OwnsOne(ne => ne.Heizkosten, GeldConfigurieren);
        builder.OwnsOne(ne => ne.Kaution, GeldConfigurieren);
    }

    private void GeldConfigurieren(OwnedNavigationBuilder<Nutzungseinheit, Preis> obj) { }
}