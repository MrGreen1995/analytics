using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Shared;

namespace UzEx.Analytics.Infrastructure.Configurations
{
    public sealed class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            builder.ToTable("deals");

            builder.HasKey(x => x.Id);

            builder.Property(deal => deal.ModifiedOnUtc)
                .IsRequired(false);

            builder.HasOne(deal => deal.Calendar)
                .WithMany(calendar => calendar.Deals)
                .HasForeignKey(deal => deal.CalendarId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(deal => deal.Contract)
                .WithMany(contract => contract.Deals)
                .HasForeignKey(deal => deal.ContractId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(deal => deal.SellerClient)
                .WithMany(client => client.SellerDeals)
                .HasForeignKey(deal => deal.SellerClientId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(deal => deal.SellerBroker)
                .WithMany(broker => broker.SellerBrokerDeals)
                .HasForeignKey(deal => deal.SellerBrokerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(deal => deal.BuyerClient)
                .WithMany(client => client.BuyerDeals)
                .HasForeignKey(deal => deal.BuyerClientId)
                .OnDelete(DeleteBehavior.NoAction);
            
            builder.HasOne(deal => deal.BuyerBroker)
                .WithMany(broker => broker.BuyerBrokerDeals)
                .HasForeignKey(deal => deal.BuyerBrokerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Property(deal => deal.BusinessKey)
                .IsRequired()
                .HasMaxLength(50)
                .HasConversion(businessKey => businessKey.Value, value => new BusinessKey(value));

            builder.Property(deal => deal.Number)
                .IsRequired()
                .HasMaxLength(512)
                .HasConversion(number => number.Value, value => new DealNumber(value));

            builder.Property(deal => deal.Amount)
                .IsRequired()
                .HasPrecision(18, 6)
                .HasConversion(amount => amount.Value, value => new DealAmount(value));

            builder.Property(deal => deal.Price)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasConversion(price => price.Value, value => new DealPrice(value));

            builder.OwnsOne(deal => deal.Cost, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });

            builder.Property(deal => deal.PaymentDays)
                .IsRequired()                
                .HasConversion(days => days.Value, value => new DealPaymentDays(value));

            builder.Property(deal => deal.DeliveryDays)
                .IsRequired()                
                .HasConversion(days => days.Value, value => new DealDeliveryDays(value));
            
            builder.OwnsOne(deal => deal.SellerTradeCommission, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });
            
            builder.OwnsOne(deal => deal.SellerClearingCommission, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });
            
            builder.OwnsOne(deal => deal.SellerPledge, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });
            
            builder.OwnsOne(deal => deal.BuyerTradeCommission, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });
            
            builder.OwnsOne(deal => deal.BuyerClearingCommission, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });
            
            builder.OwnsOne(deal => deal.BuyerPledge, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });
        }
    }
}
