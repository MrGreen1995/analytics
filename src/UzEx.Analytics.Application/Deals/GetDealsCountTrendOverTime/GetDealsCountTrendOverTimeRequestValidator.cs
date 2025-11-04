using FluentValidation;

namespace UzEx.Analytics.Application.Deals.GetDealsCountTrendOverTime;

public sealed class GetDealsCountTrendOverTimeRequestValidator : AbstractValidator<GetDealsCountTrendOverTimeRequest>
{
    public GetDealsCountTrendOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
                   .LessThanOrEqualTo(x => x.EndDate)
                   .WithMessage("Start date must be before or equal to End date.");
    }
}
