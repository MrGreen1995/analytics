using FluentValidation;

namespace UzEx.Analytics.Application.Deals.GetRevenueByTradeType;

public sealed class GetRevenueByTradeTypeRequestValidator : AbstractValidator<GetRevenueByTradeTypeRequest>
{
    public GetRevenueByTradeTypeRequestValidator()
    {
        RuleFor(x => x.StartDate)
             .LessThanOrEqualTo(x => x.EndDate)
             .WithMessage("Start date must be before or equal to End date.");
    }
}
