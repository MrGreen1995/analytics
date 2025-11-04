using Microsoft.EntityFrameworkCore;
using System.Globalization;
using UzEx.Analytics.Application.Abstractions.Data;
using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Application.Deals.GetDealsCountTrendOverTime;

public sealed class GetDealsCountTrendOverTimeQueryHandler
    : IQueryHandler<GetDealsCountTrendOverTimeQuery, GetDealsCountTrendOverTimeResponse>
{

    private readonly IApplicationDbContext _dbContext;

    public GetDealsCountTrendOverTimeQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetDealsCountTrendOverTimeResponse>> Handle(GetDealsCountTrendOverTimeQuery request, CancellationToken cancellationToken)
    {
        var startDate = request.Request.StartDate.ToDateTime(TimeOnly.MinValue);
        var endDate = request.Request.EndDate.ToDateTime(TimeOnly.MaxValue);

        var previouseStartDate = request.Request.StartDate.AddYears(-1).ToDateTime(TimeOnly.MinValue);
        var previouseEndDate = request.Request.EndDate.AddYears(-1).ToDateTime(TimeOnly.MaxValue);



        var previouseDealsCount = await _dbContext.Deals
            .Where(d => d.DateOnUtc >= startDate.ToUniversalTime() && d.DateOnUtc <= endDate.ToUniversalTime())
            .AsNoTracking()
            .CountAsync(cancellationToken);

        var currentDealsCount = await _dbContext.Deals
            .Where(d => d.DateOnUtc >= previouseStartDate.ToUniversalTime() && d.DateOnUtc <= previouseEndDate.ToUniversalTime())
            .AsNoTracking()
            .CountAsync(cancellationToken);


        // Edge case: Previous value is zero (division by zero)
        if (previouseDealsCount == 0)
        {
            if (currentDealsCount > 0)
            {
                // Trend is infinite rise from zero (Treat as max positive)
                return new GetDealsCountTrendOverTimeResponse
                {
                    PercentageChange = 1000000,
                    FormattedChange = "+∞%",
                    Icon = "trending_up",
                    CssClass = ".rz-color-success"
                };
            }

            // Trend is stable at zero (or no change)
            return new GetDealsCountTrendOverTimeResponse
            {
                PercentageChange = 0,
                FormattedChange = "0.00%",
                Icon = "pause",
                CssClass = ".rz-color-secondary"
            };
        }

        var change = currentDealsCount - previouseDealsCount;
        var percentageChange = (change / previouseDealsCount) * 100;


        decimal tolerance = 0.0001M;

        string formattedChange = string.Empty;
        string icon = string.Empty;
        string cssClass = string.Empty;

        if (percentageChange > tolerance)
        {
            icon = "trending_up";
            cssClass = ".rz-color-success";
            formattedChange = percentageChange.ToString("P", CultureInfo.InvariantCulture);
        }
        else if (percentageChange < tolerance)
        {
            icon = "trending_down";
            cssClass = ".rz-color-danger";
            formattedChange = percentageChange.ToString("P", CultureInfo.InvariantCulture);
        }
        else
        {
            icon = "pause";
            cssClass = ".rz-color-secondary";
            formattedChange = "0.00%";
        }


        return new GetDealsCountTrendOverTimeResponse
        {
            PercentageChange = percentageChange,
            FormattedChange = formattedChange,
            Icon = icon,
            CssClass = cssClass
        };
    }
}
