using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.Contracts.GetContractFromNewSpot;
using UzEx.Analytics.Application.NewSpot.GetBroker;
using UzEx.Analytics.Application.NewSpot.GetClient;
using UzEx.Analytics.Application.NewSpot.GetOrders;

namespace UzEx.Analytics.Api.Controllers.NewSpots;

[ApiController]
[Route("api/[controller]")]
public class NewSpotController : ControllerBase
{
    private readonly ISender _sender;

    public NewSpotController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("GetOrders")]
    public async Task<IActionResult> GetOrders([FromQuery] DateTime date, CancellationToken cancellationToken)
    {
        var query = new GetOrdersFromNewSpotQuery(date);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("GetContract")]
    public async Task<IActionResult> GetContract(long id, CancellationToken cancellationToken)
    {
        var query = new GetContractFromNewSpotQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("GetClient")]
    public async Task<IActionResult> GetClient(string id, CancellationToken cancellationToken)
    {
        var query = new GetClientFromNewSpotQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("GetBroker")]
    public async Task<IActionResult> GetBroker(string id, CancellationToken cancellationToken)
    {
        var query = new GetBrokerFromNewSpotQuery(id);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
}