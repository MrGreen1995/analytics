using System.Text.Json;
using UzEx.Analytics.Application.Abstractions.Clock;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.DataMigrations;
using UzEx.Analytics.Domain.DataMigrations.Errors;

namespace UzEx.Analytics.Application.DataMigrations.CopyDataMigrations;

public sealed class DataMigrationQueryHandler : IQueryHandler<DataMigrationQuery, bool>
{
    private readonly INewSpotService _service;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IDataMigrationRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DataMigrationQueryHandler(
        INewSpotService service, 
        IDateTimeProvider dateTimeProvider, 
        IDataMigrationRepository repository, IUnitOfWork unitOfWork)
    {
        _service = service;
        _dateTimeProvider = dateTimeProvider;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DataMigrationQuery request, CancellationToken cancellationToken)
    {
        var dateTime = new DateTime(request.Date.Year, request.Date.Month, request.Date.Day, 0, 0, 0);
        var platformType = request.PlatformType;
        var dataType = request.DataType;

        switch (platformType)
        {
            case DataMigrationPlatformType.OldSp:
            {
                switch (dataType)
                {
                    case DataMigrationDataType.Order:
                    {
                        break;
                    }
                    case DataMigrationDataType.Deal:
                    {
                        break;
                    }
                    default:
                        return Result.Failure<bool>(DataMigrationErrors.NotFound);
                }
                break;
            }
            case DataMigrationPlatformType.NewSp:
            {
                switch (dataType)
                {
                    case DataMigrationDataType.Order:
                    {
                        var orders = await _service.GetOrders(dateTime, cancellationToken);

                        foreach (var order in orders)
                        {
                            var payload = JsonSerializer.Serialize(order);
            
                            var dataMigration = DataMigration.Create(
                                Guid.NewGuid(),
                                _dateTimeProvider.UtcNow,
                                DataMigrationPlatformType.NewSp,
                                DataMigrationDataType.Order,
                                payload);
            
                            _repository.Add(dataMigration);
                        }
                        
                        break;
                    }
                    case DataMigrationDataType.Deal:
                    {
                        var deals = await _service.GetDeals(dateTime, cancellationToken);
                        
                        foreach (var deal in deals)
                        {
                            var payload = JsonSerializer.Serialize(deal);
                            var dataMigration = DataMigration.Create(
                                Guid.NewGuid(),
                                _dateTimeProvider.UtcNow,
                                DataMigrationPlatformType.NewSp,
                                DataMigrationDataType.Deal,
                                payload);
            
                            _repository.Add(dataMigration);
                        }
                        
                        break;
                    }
                    default:
                        return Result.Failure<bool>(DataMigrationErrors.NotFound);
                }
                break;
            }
            default:
                return Result.Failure<bool>(DataMigrationErrors.NotFound);
        }
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return true;
    }
}