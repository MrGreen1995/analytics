using FluentValidation;

namespace UzEx.Analytics.Application.Deals.ExportDealsToExcel;

public sealed class ExportDealsToExcelRequestValidator : AbstractValidator<ExportDealsToExcelRequest>
{
    public ExportDealsToExcelRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .LessThanOrEqualTo(x => x.EndDate)
            .WithMessage("Start date must be before or equal to End date.");

        RuleFor(x => x)
            .Must(x => (x.EndDate.DayNumber - x.StartDate.DayNumber) <= 365)
            .When(x => x.StartDate <= x.EndDate)
            .WithMessage("Date range cannot exceed 1 year.");
    }
}
