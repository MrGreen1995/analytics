using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UzEx.Analytics.Domain.Calendars;

namespace UzEx.Analytics.Infrastructure.Configurations;

public class CalendarConfiguration: IEntityTypeConfiguration<Calendar>
{
    public void Configure(EntityTypeBuilder<Calendar> builder)
    {
        builder.ToTable("calendars");
        
        builder.HasKey(x => x.Id);

        builder.OwnsOne(calendar => calendar.Date);

        builder.HasIndex(calendar => calendar.DateKey).IsUnique();
    }
}