using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.Contracts.GetContract;
using UzEx.Analytics.Application.Contracts.GetTotalContractsCountByTradeTypeOverTime;
using UzEx.Analytics.Application.Contracts.SearchContracts;

namespace UzEx.Analytics.Api.Controllers.Contracts;

[ApiController]
[Route("api/[controller]")]
public class ContractsController : ControllerBase
{
    private readonly ISender _sender;

    public ContractsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("SearchContracts")]
    public async Task<IActionResult> SearchContracts([FromBody] SearchContractsRequest request, CancellationToken cancellationToken)
    {
        var query = new SearchContractsQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetContractQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("TotalContractsCountByTradeTypeOverTimeSpan")]
    public async Task<IActionResult> TotalContractsCountByTradeTypeOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetTotalContractsCountByTradeTypeOverTimeRequest()
        {
            StartDate = startDate,
            EndDate = endDate,
        };

        var query = new GetTotalContractsCountByTradeTypeOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
}