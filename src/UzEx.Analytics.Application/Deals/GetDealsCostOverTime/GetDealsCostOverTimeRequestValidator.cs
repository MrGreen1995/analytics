using FluentValidation;

namespace UzEx.Analytics.Application.Deals.GetDealsCostOverTime;

public sealed class GetDealsCostOverTimeRequestValidator : AbstractValidator<GetDealsCostOverTimeRequest>
{
    public GetDealsCostOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("Start date must be before or equal to End date.");
    }
}
