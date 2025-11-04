using FluentValidation;

namespace UzEx.Analytics.Application.Clients.GetTopBuyersOverTime;

public sealed class GetTopBuyersOverTimeRequestValidator
    : AbstractValidator<GetTopBuyersOverTimeRequest>
{
    public GetTopBuyersOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("Start date must be before or equal to End date.");
    }
}
