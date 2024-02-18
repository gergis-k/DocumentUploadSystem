using Core.Constants;
using Core.Entities;
using Database.Configurations.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

internal sealed class UploadedFileConfiguration : BaseEntityConfiguration<UploadedFile>
{
    public override void Configure(EntityTypeBuilder<UploadedFile> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.FileName)
            .HasMaxLength(ConfigurationsConstants.UploadedFile.FileNameMaxLength);

        builder.HasIndex(e => e.FileName)
            .IsUnique()
            .HasFilter("[FileName] IS NOT NULL");

        builder.Property(e => e.FakeName)
            .IsRequired()
            .HasMaxLength(ConfigurationsConstants.UploadedFile.FakeNameMaxLength);

        builder.HasIndex(e => e.FakeName)
            .IsUnique();

        builder.Property(e => e.ContentType)
            .IsRequired()
            .HasMaxLength(ConfigurationsConstants.UploadedFile.ContentTypeMaxLength);

        builder.HasOne(e => e.Document)
            .WithMany(e => e.UploadedFiles)
            .HasForeignKey(e => e.DocumentId)
            .IsRequired();
    }
}
