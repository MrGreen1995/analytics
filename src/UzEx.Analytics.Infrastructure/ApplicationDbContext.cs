using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UzEx.Analytics.Application.Abstractions.Clock;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Exceptions;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Brokers;
using UzEx.Analytics.Domain.Calendars;
using UzEx.Analytics.Domain.Clients;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.DataMigrations;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Orders;
using UzEx.Analytics.Infrastructure.Outbox;

namespace UzEx.Analytics.Infrastructure;

public sealed class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };
    
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public ApplicationDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : base(options)
    {
        _dateTimeProvider = dateTimeProvider;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    
        base.OnModelCreating(modelBuilder);
    }
    
    private void AddDomainEventsAsOutboxMessages()
    {
        var entities = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity);
        
        var events = entities
            .SelectMany(entity =>
            {
                var events = new List<IDomainEvent>();
                
                var domainEvents = entity.GetDomainEvents();
                events.AddRange(domainEvents);
                
                entity.ClearDomainEvents();
                
                return events;
            });
            
        var outboxMessages = events
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(), 
                _dateTimeProvider.UtcNow, 
                domainEvent.GetType().Name, 
                 JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
            .ToList();

        AddRange(outboxMessages);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            AddDomainEventsAsOutboxMessages();

            var result = await base.SaveChangesAsync(cancellationToken);          
    
            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occured", ex);
        }
    }

    public DbSet<Contract> Contracts { get; private set; }

    public DbSet<Client> Clients { get; private set; }
    
    public DbSet<Order> Orders { get; private set; }
    
    public DbSet<Calendar> Calendars { get; private set;  }

    public DbSet<Deal> Deals { get; private set;  }
    
    public DbSet<Broker> Brokers { get; private set;  }
    
    public DbSet<DataMigration> DataMigrations { get; private set;  }
}