namespace SampleApp.Persistence.Ef.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleApp.Core;
using SampleApp.Core.Entities;
using SampleApp.Persistence.Ef.Common;

public sealed class PolyFilterConfig : IEntityTypeConfiguration<PolyFilter>
{
    public void Configure(EntityTypeBuilder<PolyFilter> builder)
    {
        builder.ToTable(nameof(PolyFilter));

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(Constants.MaxTitleLength)
                .HasDefaultValue(Constants.DefaultTitle);

        builder.Property(e => e.Note)
                .HasMaxLength(Constants.MaxNoteLength)
                .HasDefaultValue(Constants.DefaultNote);

        builder.Property(e => e.ConditionJson)
                .IsUnicode()
                .IsRequired();

        // 1401.09.09
        //builder.Ignore(e => e.Condition);
    }
}

