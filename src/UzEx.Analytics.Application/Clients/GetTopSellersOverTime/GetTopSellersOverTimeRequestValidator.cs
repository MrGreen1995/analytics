using FluentValidation;

namespace UzEx.Analytics.Application.Clients.GetTopSellersOverTime;

public sealed class GetTopSellersOverTimeRequestValidator
    : AbstractValidator<GetTopSellersOverTimeRequest>
{
    public GetTopSellersOverTimeRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("Start date must be before or equal to End date.");
    }
}
