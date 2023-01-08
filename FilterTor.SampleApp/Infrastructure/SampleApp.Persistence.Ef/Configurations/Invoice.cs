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
                .HasColumnType("date")
                .IsRequired();

        builder.Property(e => e.InvoiceType)
            .HasConversion(new EnumToStringConverter<InvoiceType>());

        builder.HasOne(e => e.Customer)
            .WithMany()
            .HasForeignKey(e => e.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
            new Invoice("100100", new DateTime(2022, 12, 9), false, 50000m, customerId: 100000, invoiceType: InvoiceType.FMCG) { Id = 100 },
            new Invoice("100101", new DateTime(2022, 11, 8), true, 40000m, customerId: 100001, invoiceType: InvoiceType.Medical) { Id = 101 },
            new Invoice("100102", new DateTime(2022, 1, 12), false, 20000m, customerId: 100001, invoiceType: InvoiceType.FMCG) { Id = 102 },
            new Invoice("100103", new DateTime(2022, 10, 4), false, 25000m, customerId: 100002, invoiceType: InvoiceType.FMCG) { Id = 103 },
            new Invoice("100104", new DateTime(2021, 7, 3), true, 10000m, customerId: 100003, invoiceType: InvoiceType.Medical) { Id = 104 },
            new Invoice("100105", new DateTime(2021, 4, 9), false, 10000m, customerId: 100002, invoiceType: InvoiceType.FMCG) { Id = 105 }
            );
    }
}

