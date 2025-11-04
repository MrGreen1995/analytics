using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzEx.Analytics.Domain.Clients;

namespace UzEx.Analytics.Infrastructure.Configurations
{
    public sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");

            builder.HasKey(x => x.Id);

            builder.Property(client => client.RegNumber)
                .HasMaxLength(512)
                .HasConversion(regNumber => regNumber.Value, value => new ClientRegNumber(value));

            builder.Property(client => client.Name)
                .HasMaxLength(1000)
                .HasConversion(name => name.Value, value => new ClientName(value));

            builder.Property(client => client.Country)
                .HasMaxLength(512)
                .HasConversion(country => country.Value, value => new ClientCountry(value));

            builder.Property(client => client.Region)
                .IsRequired(false)
                .HasMaxLength(512)
                .HasConversion(region => region.Value, value => new ClientRegion(value));

            builder.Property(client => client.District)
                .IsRequired(false)
                .HasMaxLength(512)
                .HasConversion(district => district.Value, value => new ClientDistrict(value));

            builder.Property(client => client.Address)
                .HasMaxLength(2000)                
                .HasConversion(address => address.Value, value => new ClientAddress(value));
            
            builder.Property(client => client.NewSpotKey)
                .HasMaxLength(100)                
                .HasConversion(key => key.Value, value => new ClientNewSpotKey(value));
            
            builder.Property(client => client.OldSpotKey)
                .HasMaxLength(100)                
                .HasConversion(key => key.Value, value => new ClientOldSpotKey(value));
        }
    }
}
