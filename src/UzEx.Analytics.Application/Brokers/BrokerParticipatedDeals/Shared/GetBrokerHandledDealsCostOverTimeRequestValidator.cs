using FluentValidation;

namespace UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.Shared;

public sealed class GetBrokerHandledDealsCostOverTimeRequestValidator
    : AbstractValidator<GetBrokerHandledDealsCostOverTimeRequest>
{
    public GetBrokerHandledDealsCostOverTimeRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Broker Id is required")
            .NotNull().WithMessage("Broker Id is required");

        RuleFor(x => x.StartDate)
.LessThanOrEqualTo(x => x.EndDate)
.WithMessage("Start date must be before or equal to End date.");

        RuleFor(x => x)
            .Must(x => (x.EndDate.ToDateTime(TimeOnly.MinValue) -
            x.StartDate.ToDateTime(TimeOnly.MinValue)).TotalDays <= 365)
            .WithMessage("Date range cannot exceed 1 year.");
    }
}
