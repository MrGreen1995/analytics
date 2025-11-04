using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzEx.Analytics.Domain.Orders;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Infrastructure.Configurations;

public sealed class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");
        
        builder.HasKey(x => x.Id);

        builder.HasOne(order => order.Calendar)
            .WithMany(calendar => calendar.Orders)
            .HasForeignKey(order => order.CalendarId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(order => order.Contract)
            .WithMany(contract => contract.Orders)
            .HasForeignKey(order => order.ContractId)
            .OnDelete(DeleteBehavior.NoAction); ;

        builder.HasOne(order => order.Client)
            .WithMany(client => client.Orders)
            .HasForeignKey(order => order.ClientId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(order => order.Broker)
            .WithMany(broker => broker.Orders)
            .HasForeignKey(order => order.BrokerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(contract => contract.BusinessKey)
            .HasMaxLength(50)
            .HasConversion(businessKey => businessKey.Value, value => new BusinessKey(value));
        
        //builder.HasOne<Calendar>()
        //    .WithMany()
        //    .HasForeignKey(order => order.CalendarId)
        //    .OnDelete(DeleteBehavior.NoAction);        
        
        //builder.HasOne<Contract>()
        //    .WithMany()
        //    .HasForeignKey(order => order.ContractId)
        //    .OnDelete(DeleteBehavior.NoAction);

        //builder.HasOne<Client>()
        //    .WithMany()
        //    .HasForeignKey(order => order.ClientId)
        //    .OnDelete(DeleteBehavior.NoAction);

        builder.Property(order => order.Amount)
            .HasPrecision(18, 6)
            .HasConversion(amount => amount.Value, value => new OrderAmount(value));
        
        builder.Property(order => order.Price)
            .HasPrecision(18, 2)
            .HasConversion(price => price.Value, value => new OrderPrice(value));
    }
}