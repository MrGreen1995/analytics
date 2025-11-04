using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.Calendars.GetCalendars;

namespace UzEx.Analytics.Api.Controllers.Calendars;

[Route("api/[controller]")]
[ApiController]
public class CalendarsController : ControllerBase
{
    private readonly ISender _sender;

    public CalendarsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetById([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCalendarQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
}