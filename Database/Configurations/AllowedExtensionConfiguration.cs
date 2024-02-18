using Core.Constants;
using Core.Entities;
using Database.Configurations.Bases;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configurations;

internal sealed class AllowedExtensionConfiguration : BaseEntityConfiguration<AllowedExtension>
{
    public override void Configure(EntityTypeBuilder<AllowedExtension> builder)
    {
        base.Configure(builder);

        builder.HasIndex(e => e.Extension).IsUnique();

        builder.Property(e => e.Extension)
            .HasMaxLength(ConfigurationsConstants.AllowedExtension.ExtensionMaxLength)
            .IsRequired();
    }
}