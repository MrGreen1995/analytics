using FluentValidation;

namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirection;

public sealed class GetTotalOrdersCountByDirectionRequestValidator : AbstractValidator<GetTotalOrdersCountByDirectionRequest>
{
    public GetTotalOrdersCountByDirectionRequestValidator()
    {
        RuleFor(x => x.StartDate)
                .LessThanOrEqualTo(x => x.EndDate)
                .WithMessage("Start date must be before or equal to End date.");
    }
}
