using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.Brokers.BrokerClients.GetBuyerBrokerClientsOverTime;
using UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.GetBrokerBuyerDealsCostOverTime;
using UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.GetBrokerSellerDealsCostOverTime;
using UzEx.Analytics.Application.Brokers.BrokerParticipatedDeals.Shared;
using UzEx.Analytics.Application.Brokers.GetBroker;
using UzEx.Analytics.Application.Brokers.GetBrokerClientsOverTime;
using UzEx.Analytics.Application.Brokers.GetBrokersLastDealsOverTime;
using UzEx.Analytics.Application.Brokers.GetBrokersLastOrdersOverTime;
using UzEx.Analytics.Application.Brokers.GetMostActiveBrokersByRegionOverTime;
using UzEx.Analytics.Application.Brokers.GetMostPassiveBrokersByRegionOverTime;
using UzEx.Analytics.Application.Brokers.GetTotalBrokersCountByRegionsOverTime;
using UzEx.Analytics.Application.Brokers.SearchBrokers;

namespace UzEx.Analytics.Api.Controllers.Brokers;

[Route("api/[controller]")]
[ApiController]
public class BrokerController : ControllerBase
{
    private readonly ISender _sender;

    public BrokerController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBrokerQuery(id);
        var broker = await _sender.Send(query, cancellationToken);
        return Ok(broker);
    }

    [HttpPost("SearchBrokers")]
    public async Task<IActionResult> SearchBrokers([FromBody] SearchBrokersRequest request, CancellationToken cancellationToken)
    {
        var query = new SearchBrokersQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet("TotalBrokersCountByRegionsOverTimeSpan")]
    public async Task<IActionResult> TotalBrokersCountByRegionsOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetTotalBrokersCountByRegionsOverTimeRequest
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var query = new GetTotalBrokersCountByRegionsOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("MostPassiveBrokersByRegionOverTimeSpan")]
    public async Task<IActionResult> MostPassiveBrokersByRegionOverTimeSpan([FromQuery] int PageNumber, [FromQuery] int PageSize, [FromQuery] int Region, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetMostPassiveBrokersByRegionOverTimeRequest
        {
            PageSize = PageSize,
            PageNumber = PageNumber,
            StartDate = startDate,
            EndDate = endDate,
            Region = Region
        };

        var query = new GetMostPassiveBrokersByRegionOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("MostActiveBrokersByRegionOverTimeSpan")]
    public async Task<IActionResult> MostActiveBrokersByRegionOverTimeSpan([FromQuery] int PageNumber, [FromQuery] int PageSize, [FromQuery] int Region, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetMostActiveBrokersByRegionOverTimeRequest
        {
            PageSize = PageSize,
            PageNumber = PageNumber,
            StartDate = startDate,
            EndDate = endDate,
            Region = Region
        };

        var query = new GetMostActiveBrokersByRegionOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("BrokersLastDealsOverTimeSpan")]
    public async Task<IActionResult> BrokersLastDealsOverTimeSpan([FromQuery] int PageNumber, [FromQuery] int PageSize, [FromQuery] Guid Id, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetBrokersLastDealsOverTimeRequest
        {
            Id = Id,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = PageNumber,
            PageSize = PageSize
        };

        var query = new GetBrokersLastDealsOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("BrokersLastOrdersOverTimeSpan")]
    public async Task<IActionResult> BrokersLastOrdersOverTimeSpan([FromQuery] int PageNumber, [FromQuery] int PageSize, [FromQuery] Guid Id, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetBrokersLastOrdersOverTimeRequest
        {
            Id = Id,
            PageNumber = PageNumber,
            PageSize = PageSize,
            StartDate = startDate,
            EndDate = endDate
        };

        var query = new GetBrokersLastOrdersOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("BrokerSellerDealsCostOverTimeSpan")]
    public async Task<IActionResult> BrokerSellerDealsCostOverTimeSpan([FromQuery] Guid Id, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetBrokerHandledDealsCostOverTimeRequest
        {
            Id = Id,
            StartDate = startDate,
            EndDate = endDate
        };

        var query = new GetBrokerSellerDealsCostOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("BrokerBuyerDealsCostOverTimeSpan")]
    public async Task<IActionResult> BrokerBuyerDealsCostOverTimeSpan([FromQuery] Guid Id, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new GetBrokerHandledDealsCostOverTimeRequest
        {
            Id = Id,
            StartDate = startDate,
            EndDate = endDate
        };

        var query = new GetBrokerBuyerDealsCostOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("BrokerClientsOverTimeSpan")]
    public async Task<IActionResult> BrokerClientsOverTimeSpan([FromQuery] int PageNumber, [FromQuery] int PageSize, [FromQuery] Guid Id, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new Application.Brokers.GetBrokerClientsOverTime.GetBrokerClientsOverTimeRequest
        {
            BrokerId = Id,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = PageNumber,
            PageSize = PageSize
        };

        var query = new GetBrokerClientsOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }

    [HttpGet("BuyerBrokerClientOverTimeSpan")]
    public async Task<IActionResult> BuyerBrokerClientOverTimeSpan([FromQuery] int PageNumber, [FromQuery] int PageSize, [FromQuery] Guid Id, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
    {
        var request = new Application.Brokers.BrokerClients.Shared.GetBrokerClientsOverTimeRequest
        {
            Id = Id,
            StartDate = startDate,
            EndDate = endDate,
            PageNumber = PageNumber,
            PageSize = PageSize
        };

        var query = new Application.Brokers.BrokerClients.GetBuyerBrokerClientsOverTime.GetBuyerBrokerClientOverTimeQuery(request);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
}