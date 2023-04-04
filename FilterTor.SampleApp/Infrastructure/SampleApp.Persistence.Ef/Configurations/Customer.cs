namespace SampleApp.Persistence.Ef.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleApp.Core;
using SampleApp.Core.Entities;
using SampleApp.Persistence.Ef.Common;

public sealed class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(nameof(Customer), "sample");

        builder.HasKey(x => x.Id);

        builder.Property(e => e.RegisteredDate).HasColumnType("DATE");

        builder.Property(e => e.Credit).HasPrecision(18, 2);

        var navigation = builder.Metadata.FindNavigation(nameof(Customer.CustomerGroups));

        if (navigation is null)
            throw new NotImplementedException("CustomerConfig > Configure");

        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.HasData(
            new Customer("Customer A", new DateTime(2021, 1, 1), 100000m) { Id = 100000 },
            new Customer("Customer B", new DateTime(2020, 6, 1), 200000m) { Id = 100001 },
            new Customer("Customer C", new DateTime(2020, 1, 1), 300000m) { Id = 100002 },
            new Customer("Customer D", new DateTime(2019, 1, 1), 400000m) { Id = 100003 }
            );
    }
}

