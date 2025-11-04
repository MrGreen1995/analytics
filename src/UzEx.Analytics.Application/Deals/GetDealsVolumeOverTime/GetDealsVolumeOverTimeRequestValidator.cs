using FluentValidation;

namespace UzEx.Analytics.Application.Deals.GetDealsVolumeOverTime;

public sealed class GetDealsVolumeOverTimeRequestValidator : AbstractValidator<GetDealsVolumeOverTimeRequest>
{
    public GetDealsVolumeOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
                   .LessThanOrEqualTo(x => x.EndDate)
                   .WithMessage("Start date must be before or equal to End date.");
    }
}
