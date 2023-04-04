namespace SampleApp.Persistence.Ef.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleApp.Core;
using SampleApp.Core.Entities;
using SampleApp.Persistence.Ef.Common;


public sealed class InvoiceDetailConfig : IEntityTypeConfiguration<InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
    {
        builder.ToTable(nameof(InvoiceDetail), "sample");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Discount).HasPrecision(18, 2);

        builder.Property(e => e.UnitPrice).HasPrecision(18, 2);

        builder.HasOne(e => e.Invoice)
            .WithMany(e => e.InvoiceDetails)
            .HasForeignKey(e => e.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
                new InvoiceDetail(invoiceId: 100, productId: 100, count: 2, unitPrice: 2500m, discount: 1, isPrize: false) { Id = 1 },
                new InvoiceDetail(invoiceId: 100, productId: 101, count: 9, unitPrice: 5000m, discount: 9, isPrize: false) { Id = 2 },
                new InvoiceDetail(invoiceId: 100, productId: 102, count: 1, unitPrice: 1000m, discount: 1000, isPrize: true) { Id = 3 },

                new InvoiceDetail(invoiceId: 101, productId: 103, count: 9, unitPrice: 4000m, discount: 1, isPrize: false) { Id = 4 },
                new InvoiceDetail(invoiceId: 101, productId: 109, count: 2, unitPrice: 2000m, discount: 9, isPrize: false) { Id = 5 },

                new InvoiceDetail(invoiceId: 102, productId: 100, count: 2, unitPrice: 2500m, discount: 1, isPrize: false) { Id = 6 },
                new InvoiceDetail(invoiceId: 102, productId: 101, count: 5, unitPrice: 5000m, discount: 9, isPrize: false) { Id = 7 },
                new InvoiceDetail(invoiceId: 102, productId: 103, count: 1, unitPrice: 4000m, discount: 1000, isPrize: true) { Id = 8 },

                new InvoiceDetail(invoiceId: 103, productId: 105, count: 10, unitPrice: 2500m, discount: 1, isPrize: false) { Id = 9 },

                new InvoiceDetail(invoiceId: 104, productId: 107, count: 10, unitPrice: 1000m, discount: 1, isPrize: false) { Id = 10 },

                new InvoiceDetail(invoiceId: 105, productId: 107, count: 5, unitPrice: 1000m, discount: 1, isPrize: false) { Id = 11 },
                new InvoiceDetail(invoiceId: 105, productId: 103, count: 2, unitPrice: 2500m, discount: 9, isPrize: false) { Id = 12 }
            );
    }
}

