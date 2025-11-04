using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzEx.Analytics.Domain.Brokers;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Infrastructure.Configurations;

public class BrokerConfiguration : IEntityTypeConfiguration<Broker>
{
    public void Configure(EntityTypeBuilder<Broker> builder)
    {
        builder.ToTable("brokers");
        
        builder.HasKey(b => b.Id);
        
        builder.Property(broker => broker.BusinessKey)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion(key => key.Value, value => new BusinessKey(value));
        
        builder.Property(broker => broker.RegNumber)
            .IsRequired()
            .HasMaxLength(50)
            .HasConversion(number => number.Value, value => new BrokerRegNumber(value));
        
        builder.Property(broker => broker.Name)
            .IsRequired()
            .HasMaxLength(1000)
            .HasConversion(name => name.Value, value => new BrokerName(value));
        
        builder.Property(broker => broker.Number)
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion(number => number.Value, value => new BrokerNumber(value));
        
        builder.Property(broker => broker.Region)
            .IsRequired()
            .HasMaxLength(256)
            .HasConversion(region => region.Value, value => new BrokerRegion(value));
        
        builder.Property(broker => broker.NewSpotKey)
            .HasMaxLength(100)
            .HasConversion(key => key.Value, value => new BrokerNewSpotKey(value));
            
        builder.Property(broker => broker.OldSpotKey)
            .HasMaxLength(100)                
            .HasConversion(key => key.Value, value => new BrokerOldSpotKey(value));
    }
}