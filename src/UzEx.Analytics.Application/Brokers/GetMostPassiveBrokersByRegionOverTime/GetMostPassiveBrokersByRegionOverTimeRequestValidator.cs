using FluentValidation;

namespace UzEx.Analytics.Application.Brokers.GetMostPassiveBrokersByRegionOverTime;

public sealed class GetMostPassiveBrokersByRegionOverTimeRequestValidator
    : AbstractValidator<GetMostPassiveBrokersByRegionOverTimeRequest>
{
    public GetMostPassiveBrokersByRegionOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
                   .LessThanOrEqualTo(x => x.EndDate)
                   .WithMessage("Start date must be before or equal to End date.");

        RuleFor(x => x)
            .Must(x => (x.EndDate.ToDateTime(TimeOnly.MinValue) -
            x.StartDate.ToDateTime(TimeOnly.MinValue)).TotalDays <= 365)
            .WithMessage("Date range cannot exceed 1 year.");

        RuleFor(x => x.Region)
            .NotNull().WithMessage("Region is required")
            .NotEmpty().WithMessage("Region is required")
            .InclusiveBetween(1, 14)
            .WithMessage("Region number must be between 1 and 14");
    }
}
