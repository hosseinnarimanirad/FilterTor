namespace SampleApp.Persistence.Ef.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleApp.Core;
using SampleApp.Core.Entities;
using SampleApp.Persistence.Ef.Common;

public sealed class PrizeStoreConfig : IEntityTypeConfiguration<PrizeStore>
{
    public void Configure(EntityTypeBuilder<PrizeStore> builder)
    {
        builder.ToTable(nameof(PrizeStore));

        builder.HasKey(x => x.Id);

        builder.Property(e => e.InvoiceConditionJson)
                .IsUnicode();

        builder.Property(e => e.CustomerConditionJson)
                .IsUnicode();

        builder.Property(e => e.StartDate)
                .HasColumnType("datetime2(0)")
                .IsRequired();

        builder.Property(e => e.EndDate)
                .HasColumnType("datetime2(0)")
                .IsRequired();
    }
}

