using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Domain.Brokers;
using UzEx.Analytics.Domain.Calendars;
using UzEx.Analytics.Domain.Clients;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.DataMigrations;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<Contract> Contracts { get; }

    DbSet<Client> Clients { get; }

    DbSet<Order> Orders { get; }
    
    DbSet<Calendar> Calendars { get; }

    DbSet<Deal> Deals { get; }
    
    DbSet<Broker> Brokers { get; }
    
    DbSet<DataMigration> DataMigrations { get; }
}