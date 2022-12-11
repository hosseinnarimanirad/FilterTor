namespace Grid.Persistence.Ef.Extensions;
 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SampleApp.Core;
using SampleApp.Persistence.Ef.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MigrationExtensions
{
    public static Microsoft.EntityFrameworkCore.Metadata.Builders.PropertyBuilder GetProperty<T>(ModelBuilder modelBuilder, IMutableEntityType entityType, string propertyName)
    {
        var propertry = entityType.GetProperties().SingleOrDefault(p => p.ClrType == typeof(T) && p.Name == propertyName);

        if (propertry != null)
        {
            return modelBuilder.Entity(propertry.DeclaringEntityType.ClrType).Property(propertyName);
        }

        return null;
    }

    public static void ConfigureInterfaces(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(IHasCreateTimeRequired).IsAssignableFrom(entity.ClrType))
                continue;

            ConfigureInterfaces(modelBuilder, entity);
        }
    }

    private static void ConfigureInterfaces(ModelBuilder modelBuilder, IMutableEntityType entity)
    {
        // CREATED BY 
        if (typeof(IHasCreatedByRequired).IsAssignableFrom(entity.ClrType))
        {
            GetProperty<int>(modelBuilder, entity, nameof(IHasCreatedByRequired.CreatedById))
                .IsRequired(required: true);

            GetProperty<string>(modelBuilder, entity, nameof(IHasCreatedByRequired.CreatedByFullName))
                .IsRequired(required: true)
                .HasMaxLength(Constants.MaxFullNameLength);
        }

        if (typeof(IHasCreatedByOptional).IsAssignableFrom(entity.ClrType))
        {
            GetProperty<int?>(modelBuilder, entity, nameof(IHasCreatedByRequired.CreatedById))
               .IsRequired(required: false);

            GetProperty<string>(modelBuilder, entity, nameof(IHasCreatedByRequired.CreatedByFullName))
                .IsRequired(required: false)
                .HasMaxLength(Constants.MaxFullNameLength);
        }

        // CREATED TIME
        if (typeof(IHasCreateTimeOptional).IsAssignableFrom(entity.ClrType))
        {
            GetProperty<DateTime?>(modelBuilder, entity, nameof(IHasCreateTimeOptional.CreateTime))
                        .HasColumnType("datetime2(2)")
                        .HasDefaultValueSql("GETDATE()");
        }

        if (typeof(IHasCreateTimeRequired).IsAssignableFrom(entity.ClrType))
        {
            GetProperty<DateTime>(modelBuilder, entity, nameof(IHasCreateTimeRequired.CreateTime))
                        .HasColumnType("datetime2(2)")
                        .HasDefaultValueSql("GETDATE()")
                        .IsRequired();
        }
         
    }

}
