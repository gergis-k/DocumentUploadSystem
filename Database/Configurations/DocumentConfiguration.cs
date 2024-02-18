using Core.Constants;
using Core.Entities;
using Database.Configurations.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

internal sealed class DocumentConfiguration : BaseEntityConfiguration<Document>
{
    public override void Configure(EntityTypeBuilder<Document> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(ConfigurationsConstants.Document.NameMaxLength);

        builder.HasIndex(e => e.Name)
            .IsUnique();

        builder.Property(e => e.Directory)
            .HasMaxLength(ConfigurationsConstants.Document.DirectoryMaxLength);

        builder.HasIndex(e => e.Directory)
            .IsUnique()
            .HasFilter("[Directory] IS NOT NULL");

        builder.Property(e => e.Priority)
            .HasConversion<string>()
            .HasMaxLength(ConfigurationsConstants.Document.PriorityMaxLength);

        builder.Property(e => e.DueDate)
            .HasDefaultValueSql("NULL");

        builder.HasMany(e => e.UploadedFiles)
            .WithOne(e => e.Document)
            .HasForeignKey(e => e.DocumentId)
            .IsRequired();
    }
}
