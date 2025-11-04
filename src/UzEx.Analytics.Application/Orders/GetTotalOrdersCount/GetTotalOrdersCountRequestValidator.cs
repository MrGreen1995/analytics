using FluentValidation;

namespace UzEx.Analytics.Application.Orders.GetTotalOrdersCount;

public sealed class GetTotalOrdersCountRequestValidator : AbstractValidator<GetTotalOrdersCountRequest>
{
    public GetTotalOrdersCountRequestValidator()
    {
        RuleFor(x => x.StartDate)
        .LessThanOrEqualTo(x => x.EndDate)
        .WithMessage("Start date must be before or equal to End date.");
    }
}
