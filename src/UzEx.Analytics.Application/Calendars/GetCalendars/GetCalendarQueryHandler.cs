using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Calendars;
using UzEx.Analytics.Domain.Calendars.Errors;

namespace UzEx.Analytics.Application.Calendars.GetCalendars;

public sealed class GetCalendarQueryHandler : IQueryHandler<GetCalendarQuery, GetCalendarResponse>
{
    private readonly ICalendarRepository _repository;

    public GetCalendarQueryHandler(ICalendarRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetCalendarResponse>> Handle(GetCalendarQuery request, CancellationToken cancellationToken)
    {
        var calendar = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (calendar is null)
        {
            return Result.Failure<GetCalendarResponse>(CalendarErrors.NotFound);
        }

        var response = new GetCalendarResponse
        {
            Guid = calendar.Id,
            Date = new DateTime(calendar.Date.Year, calendar.Date.Month, calendar.Date.Day)
        };
        
        return response;
    }
}