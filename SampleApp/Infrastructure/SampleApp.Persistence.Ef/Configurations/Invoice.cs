namespace SampleApp.Persistence.Ef.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleApp.Core;
using SampleApp.Core.Entities;
using SampleApp.Persistence.Ef.Common;

public sealed class InvoiceConfig : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable(nameof(Invoice), "sample");

        builder.HasKey(x => x.Id);

        builder.Property(e => e.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(Constants.MaxTitleLength);

        builder.Property(e => e.InvoiceDate)
                .HasColumnType("datetime2(2)");

        builder.Property(e => e.InvoiceType)
            .HasConversion(new EnumToStringConverter<InvoiceType>());
    }
}

