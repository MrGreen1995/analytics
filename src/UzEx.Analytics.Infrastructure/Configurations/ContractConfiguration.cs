using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Infrastructure.Configurations;

public sealed class ContractConfiguration : IEntityTypeConfiguration<Contract>
{
    public void Configure(EntityTypeBuilder<Contract> builder)
    {
        builder.ToTable("contracts");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(contract => contract.BusinessKey)
            .HasMaxLength(50)
            .HasConversion(businessKey => businessKey.Value, value => new BusinessKey(value));
        
        builder.HasOne(contract => contract.Product)
            .WithMany(product => product.Contracts)
            .HasForeignKey(contract => contract.ProductId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.Property(contract => contract.Number)
            .HasMaxLength(50)
            .HasConversion(number => number.Value, value => new ContractNumber(value));
        
        builder.Property(contract => contract.Name)
            .HasMaxLength(512)
            .HasConversion(name => name.Value, value => new ContractName(value));
        
        builder.Property(contract => contract.Lot)
            .HasPrecision(18, 6)
            .HasConversion(lot => lot.Value, value => new ContractLot(value));
        
        builder.Property(contract => contract.Unit)
            .HasMaxLength(50)
            .HasConversion(unit => unit.Value, value => new ContractUnit(value));
        
        builder.Property(contract => contract.BasePrice)
            .HasPrecision(18, 2)
            .HasConversion(basePrice => basePrice.Value, value => new ContractBasePrice(value));
        
        builder.Property(contract => contract.Currency)
            .HasMaxLength(50)
            .HasConversion(currency => currency.Value, value => new ContractCurrency(value));
        
        builder.Property(contract => contract.DeliveryBase)
            .HasMaxLength(50)
            .HasConversion(delivery => delivery.Value, value => new ContractDeliveryBase(value));
        
        builder.Property(contract => contract.Warehouse)
            .HasMaxLength(2000)
            .HasConversion(warehouse => warehouse.Value, value => new ContractWarehouse(value));

        builder.Property(contract => contract.OriginCountry)
            .HasMaxLength(500)
            .HasConversion(originCountry => originCountry.Value, value => new ContractOriginCountry(value));
    }
}