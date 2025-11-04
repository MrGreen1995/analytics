using FluentValidation;

namespace UzEx.Analytics.Application.Deals.GetDealsCountOverTime;

public sealed class GetDealsCountOverTimeRequestValidator : AbstractValidator<GetDealsCountOverTimeRequest>
{
    public GetDealsCountOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
                   .LessThanOrEqualTo(x => x.EndDate)
                   .WithMessage("Start date must be before or equal to End date.");
    }
}
