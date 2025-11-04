using Microsoft.EntityFrameworkCore;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;
using UzEx.Analytics.Domain.Orders;

namespace UzEx.Analytics.Application.Dashboards.GetExchangeAverageBuySum;

public sealed class GetExchangeAverageBuySumQueryHandler(IApplicationDbContext dbContext) 
    : IQueryHandler<GetExchangeAverageBuySumQuery, GetExchangeAverageBuySumResponse>
{
    public async Task<Result<GetExchangeAverageBuySumResponse>> Handle(GetExchangeAverageBuySumQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .AsNoTracking()
            .Where(order => order.Calendar.Date.Year == request.Year
                            && order.Contract.Type == (ContractType)request.Type
                            && order.Direction == OrderDirectionType.Bid
                            && (order.Status != OrderStatus.New 
                                || order.Status != OrderStatus.Rejected 
                                || order.Status != OrderStatus.Cancelled))
            .Select(order => new
            {
                Price = order.Price.Value,
                Amount = order.Amount.Value,
            })
            .ToListAsync(cancellationToken);

        var priceSum = orders.Sum(x => x.Price);
        var amountSum = orders.Sum(x => x.Amount);
        
        var ordersAvg = priceSum > 0 && amountSum > 0 
            ? Math.Round(priceSum / amountSum, 2) 
            : 0;
        
        decimal sum;
        string unit;
        
        switch (ordersAvg)
        {
            case >= 1000 and < 1000000:
                sum = Math.Round(ordersAvg / 1000, 2);
                unit = "тыс.";
                break;
            case >= 1000000 and < 1000000000:
                sum = Math.Round(ordersAvg / 1000000, 2);
                unit = "млн.";
                break;
            case >= 1000000000 and < 1000000000000:
                sum = Math.Round(ordersAvg / 1000000000, 2);
                unit = "млрд.";
                break;
            case >= 1000000000000 and < 1000000000000000:
                sum = Math.Round(ordersAvg / 1000000000000, 2);
                unit = "трлн.";
                break;
            default:
                sum = ordersAvg;
                unit = string.Empty;
                break;
        }
        
        var response = new GetExchangeAverageBuySumResponse()
        {
            Sum = sum,
            SumUnit = unit,
        };
        
        return response;
    }
}