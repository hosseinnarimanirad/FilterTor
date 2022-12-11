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

        builder.HasOne(e => e.Invoice)
            .WithMany(e => e.InvoiceDetails)
            .HasForeignKey(e => e.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(
                new InvoiceDetail(id: 1, invoiceId: 100, productId: 100, count: 2, unitPrice: 2500m, discount: 1, isPrize: false),
                new InvoiceDetail(id: 2, invoiceId: 100, productId: 101, count: 9, unitPrice: 5000m, discount: 9, isPrize: false),
                new InvoiceDetail(id: 3, invoiceId: 100, productId: 102, count: 1, unitPrice: 1000m, discount: 1000, isPrize: true),

                new InvoiceDetail(id: 4, invoiceId: 101, productId: 103, count: 9, unitPrice: 4000m, discount: 1, isPrize: false),
                new InvoiceDetail(id: 5, invoiceId: 101, productId: 109, count: 2, unitPrice: 2000m, discount: 9, isPrize: false),

                new InvoiceDetail(id: 6, invoiceId: 102, productId: 100, count: 2, unitPrice: 2500m, discount: 1, isPrize: false),
                new InvoiceDetail(id: 7, invoiceId: 102, productId: 101, count: 5, unitPrice: 5000m, discount: 9, isPrize: false),
                new InvoiceDetail(id: 8, invoiceId: 102, productId: 103, count: 1, unitPrice: 4000m, discount: 1000, isPrize: true),

                new InvoiceDetail(id: 9, invoiceId: 103, productId: 105, count: 10, unitPrice: 2500m, discount: 1, isPrize: false),

                new InvoiceDetail(id: 10, invoiceId: 104, productId: 107, count: 10, unitPrice: 1000m, discount: 1, isPrize: false),

                new InvoiceDetail(id: 11, invoiceId: 105, productId: 107, count: 5, unitPrice: 1000m, discount: 1, isPrize: false),
                new InvoiceDetail(id: 12, invoiceId: 105, productId: 103, count: 2, unitPrice: 2500m, discount: 9, isPrize: false)
            );
    }
}

