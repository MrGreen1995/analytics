using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.Orders.ExportOrdersToExcel;
using UzEx.Analytics.Application.Orders.GetTotalOrdersCount;
using UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirection;
using UzEx.Analytics.Application.Orders.GetTotalOrdersCountByDirectionOverTime;
using UzEx.Analytics.Application.Orders.SearchOrders;

namespace UzEx.Analytics.Api.Controllers.Orders;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly ISender _sender;

    public OrdersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("SearchOrders")]
    public async Task<IActionResult> SearchOrders([FromBody] SearchOrdersRequest request, CancellationToken cancellationToken)
    {
        var query = new SearchOrdersQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("DownloadExcelFile")]
    public async Task<IActionResult> ExportToExcel([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new ExportOrdersToExcelRequest
        {
            StartDate = startDate,
            EndDate = endDate
        };
        var query = new ExportOrdersToExcelQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        if (result.IsSuccess)
        {
            byte[] fileBytes = result.Value;

            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            string fileName = $"Orders_{request.StartDate}_{request.EndDate}.xlsx";


            return File(fileBytes, contentType, fileName);
        }

        return NotFound();
    }

    [HttpGet("TotalOrdersCount")]
    public async Task<IActionResult> TotalOrdersCount([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetTotalOrdersCountRequest()
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var query = new GetTotalOrdersCountQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("TotalOrdersCountByDirection")]
    public async Task<IActionResult> TotalOrdersCountByDirection([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetTotalOrdersCountByDirectionRequest()
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var query = new GetTotalOrdersCountByDirectionQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("TotalOrdersCountByDirectionOverTimeSpan")]
    public async Task<IActionResult> TotalOrdersCountByDirectionOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetTotalOrdersCountByDirectionOverTimeRequest()
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var query = new GetTotalOrdersCountByDirectionOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
}