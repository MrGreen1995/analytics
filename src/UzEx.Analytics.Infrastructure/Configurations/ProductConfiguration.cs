using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzEx.Analytics.Domain.Products;

namespace UzEx.Analytics.Infrastructure.Configurations;

public class ProductConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        
        builder.HasKey(product => product.Id);
        
        builder.Property(product => product.Code)
            .IsRequired()
            .HasConversion(code => code.Value, value => new ProductCode(value));
        
        builder.Property(product => product.Name)
            .IsRequired()
            .HasConversion(name => name.Value, value => new ProductName(value));
    }
}