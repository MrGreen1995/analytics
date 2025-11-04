using FluentValidation;

namespace UzEx.Analytics.Application.Brokers.GetBrokerClientsOverTime;

public sealed class GetBrokerClientsOverTimeRequestValidator
    : AbstractValidator<GetBrokerClientsOverTimeRequest>
{
    public GetBrokerClientsOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
           .LessThanOrEqualTo(x => x.EndDate)
           .WithMessage("Start date must be before or equal to End date.");
    }
}
