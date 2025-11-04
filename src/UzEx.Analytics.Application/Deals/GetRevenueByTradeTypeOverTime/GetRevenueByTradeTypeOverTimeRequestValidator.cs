using FluentValidation;

namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeTypeOverTime;

public sealed class GetRevenueByTradeTypeOverTimeRequestValidator : AbstractValidator<GetRevenueByTradeTypeOverTimeRequest>
{
    public GetRevenueByTradeTypeOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("Start date must be before or equal to End date.");

        RuleFor(x => x)
            .Must(x => (x.EndDate.ToDateTime(TimeOnly.MinValue) -
            x.StartDate.ToDateTime(TimeOnly.MinValue)).TotalDays <= 365)
            .WithMessage("Date range cannot exceed 1 year.");
    }
}
