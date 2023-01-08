namespace SampleApp.Persistence.Ef.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleApp.Core;
using SampleApp.Core.Entities;
using SampleApp.Persistence.Ef.Common;

public sealed class CustomerGroupConfig : IEntityTypeConfiguration<CustomerGroup>
{
    public void Configure(EntityTypeBuilder<CustomerGroup> builder)
    {
        builder.ToTable(nameof(CustomerGroup), "sample");

        builder.HasKey(x => x.Id);

        builder.HasOne(e => e.Customer)
                .WithMany(e => e.CustomerGroups)
                .HasForeignKey(e => e.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.Type)
                .HasConversion(new EnumToStringConverter<CustomerGroupType>())
                .HasMaxLength(255);

        builder.HasIndex(e => new { e.CustomerId, e.Type })
                .IsUnique();

        builder.HasData(
            new CustomerGroup() { Id = 1, CreateTime = new DateTime(2022, 10, 1), CustomerId = 100000, Type = CustomerGroupType.Government },
            new CustomerGroup() { Id = 2, CreateTime = new DateTime(2022, 10, 1), CustomerId = 100000, Type = CustomerGroupType.Suspended },
            new CustomerGroup() { Id = 3, CreateTime = new DateTime(2021, 1, 1), CustomerId = 100001, Type = CustomerGroupType.PrivateSector },
            new CustomerGroup() { Id = 4, CreateTime = new DateTime(2022, 5, 1), CustomerId = 100001, Type = CustomerGroupType.Golden },
            new CustomerGroup() { Id = 5, CreateTime = new DateTime(2021, 1, 1), CustomerId = 100002, Type = CustomerGroupType.PrivateSector },
            new CustomerGroup() { Id = 6, CreateTime = new DateTime(2022, 5, 1), CustomerId = 100002, Type = CustomerGroupType.Suspended },
            new CustomerGroup() { Id = 7, CreateTime = new DateTime(2022, 5, 1), CustomerId = 100002, Type = CustomerGroupType.Limited },
            new CustomerGroup() { Id = 8, CreateTime = new DateTime(2022, 4, 1), CustomerId = 100003, Type = CustomerGroupType.Government });
    }
}

