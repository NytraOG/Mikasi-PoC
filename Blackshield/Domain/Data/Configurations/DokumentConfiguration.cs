using Domain.Data.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Data.Configurations;

public class DokumentConfiguration : BaseEntityConfiguration<Dokument>
{
    public override void Configure(EntityTypeBuilder<Dokument> builder) => base.Configure(builder);
}