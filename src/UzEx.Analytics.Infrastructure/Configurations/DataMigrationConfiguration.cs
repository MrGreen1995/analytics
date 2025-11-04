using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzEx.Analytics.Domain.DataMigrations;

namespace UzEx.Analytics.Infrastructure.Configurations;

public sealed class DataMigrationConfiguration : IEntityTypeConfiguration<DataMigration>
{
    public void Configure(EntityTypeBuilder<DataMigration> builder)
    {
        builder.ToTable("data_migrations");
        
        builder.HasKey(dataMigration => dataMigration.Id);
        
        builder.Property(dataMigration => dataMigration.Payload)
            .HasConversion(payload => payload.Value, value => new DataMigrationPayload(value));
    }
}