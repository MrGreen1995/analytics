using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeTotalSellersCount;

public sealed class GetExchangeTotalSellersCountQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetExchangeTotalSellersCountQuery, GetExchangeTotalSellersCountResponse>
{
    public async Task<Result<GetExchangeTotalSellersCountResponse>> Handle(GetExchangeTotalSellersCountQuery request, CancellationToken cancellationToken)
    {
        var orderCount = await dbContext.Orders
            .AsNoTracking()
            .Where(order => order.Calendar.Date.Year == request.Year
                            && order.Contract.Type == (ContractType)request.Type
                            && order.Direction == OrderDirectionType.Ask
                            && (order.Status != OrderStatus.New 
                                || order.Status != OrderStatus.Rejected 
                                || order.Status != OrderStatus.Cancelled))
            .LongCountAsync(cancellationToken);

        decimal count;
        string unit;
        
        switch (orderCount)
        {
            case >= 1000 and < 1000000:
                count = Math.Round((decimal)orderCount / 1000, 2);
                unit = "тыс.";
                break;
            case >= 1000000 and < 1000000000:
                count = Math.Round((decimal)orderCount / 1000000, 2);
                unit = "млн.";
                break;
            case >= 1000000000 and < 1000000000000:
                count = Math.Round((decimal)orderCount / 1000000000, 2);
                unit = "млрд.";
                break;
            case >= 1000000000000 and < 1000000000000000:
                count = Math.Round((decimal)orderCount / 1000000000000, 2);
                unit = "трлн.";
                break;
            default:
                count = orderCount;
                unit = string.Empty;
                break;
        }

        var response = new GetExchangeTotalSellersCountResponse
        {
            Count = count,
            CountUnit = unit,
        };

        return response;
    }
}