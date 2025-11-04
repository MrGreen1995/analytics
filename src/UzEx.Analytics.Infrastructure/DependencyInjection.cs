using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Quartz;
using UzEx.Analytics.Application.Abstractions.Caching;
using UzEx.Analytics.Application.Abstractions.Clock;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Calendars;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Orders;
using UzEx.Analytics.Infrastructure.Clock;
using UzEx.Analytics.Infrastructure.Data;
using UzEx.Analytics.Infrastructure.Repositories;
using UzEx.Analytics.Application.Abstractions.Soliq;
using UzEx.Analytics.Domain.Clients;
using UzEx.Analytics.Domain.Deals;
using UzEx.Analytics.Infrastructure.NewSpot;
using UzEx.Analytics.Infrastructure.Soliq;
using UzEx.Analytics.Application.Abstractions.HandBook;
using UzEx.Analytics.Domain.Brokers;
using UzEx.Analytics.Domain.DataMigrations;
using UzEx.Analytics.Infrastructure.Caching;
using UzEx.Analytics.Infrastructure.HandBook;
using UzEx.Analytics.Infrastructure.Outbox;

namespace UzEx.Analytics.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        AddPersistence(services, configuration);

        AddSoliq(services, configuration);

        AddHandBook(services, configuration);

        AddNewSpotService(services, configuration);
        
        AddBackgroundJobs(services, configuration);
        
        AddCaching(services, configuration);

        return services;
    }

    private static void AddPersistence(IServiceCollection services, IConfiguration configuration)
    {
        // Connection string
        var postgresConnectionString = configuration.GetConnectionString("Postgres")
                               ?? throw new ArgumentNullException(nameof(configuration));

        var mssqlConnectionString = configuration.GetConnectionString("MSSQL")
                               ?? throw new ArgumentNullException(nameof(configuration));

        var oracleConnectionString = configuration.GetConnectionString("Oracle")
                                    ?? throw new ArgumentNullException(nameof(configuration));


        // Entity framework database context
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(postgresConnectionString)
                .UseSnakeCaseNamingConvention();
        });


        // Repository services
        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<ICalendarRepository, CalendarRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IBrokerRepository, BrokerRepository>();
        services.AddScoped<IDealRepository, DealRepository>();
        services.AddScoped<IDataMigrationRepository, DataMigrationRepository>();


        // Unit of work
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());


        // Sql connection factory
        services.AddSingleton<ISqlConnectionFactory>(_ => new SqlConnectionFactory(postgresConnectionString, mssqlConnectionString, oracleConnectionString));


        // Add EF core 
        services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());

        // Sql type handlers
        SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
    }

    private static void AddSoliq(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<ISoliqService, SoliqService>(client =>
        {
            client.BaseAddress = new Uri("http://sp-id-api.uzex.uz");
        });
    }

    private static void AddHandBook(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IHandBookService, HandBookService>(client =>
        {
            client.BaseAddress = new Uri("https://spot-handbookapi.uzex.uz/");
        });
    }

    private static void AddNewSpotService(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<NewSpotOptions>(configuration.GetSection("NewSpotOptions"));
        services.AddHttpClient<INewSpotService, NewSpotService>((serviceProvider ,client) =>
        {
            var options = serviceProvider.GetRequiredService<IOptions<NewSpotOptions>>().Value;
            client.BaseAddress = new Uri(options.Url);
        });
    }

    private static void AddBackgroundJobs(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<OutboxOptions>(configuration.GetSection("Outbox"));
        services.AddQuartz();
        
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
    
        services.ConfigureOptions<ProcessOutboxMessagesJobSetup>();
    }

    private static void AddCaching(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Cache")
            ?? throw new ArgumentNullException(nameof(configuration));
        
        services.AddStackExchangeRedisCache(options => options.Configuration = connectionString);

        services.AddSingleton<ICacheService, CacheService>();
    }
}