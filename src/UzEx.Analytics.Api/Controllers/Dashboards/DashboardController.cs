using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.Dashboards.GetExchangeAverageBuySum;
using UzEx.Analytics.Application.Dashboards.GetExchangeAverageSellSum;
using UzEx.Analytics.Application.Dashboards.GetExchangeParticipants;
using UzEx.Analytics.Application.Dashboards.GetExchangeTotalBuyersCount;
using UzEx.Analytics.Application.Dashboards.GetExchangeTotalSellersCount;
using UzEx.Analytics.Application.Dashboards.GetExchangeVolumes;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Api.Controllers.Dashboards;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ISender _sender;

    public DashboardController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost("GetExchangeVolume")]
    public async Task<IActionResult> GetExchangeVolume(GetExchangeVolumeRequest request, CancellationToken cancellationToken)
    {
        var contractsTypes = request.ContractTypes.Select(r => (ContractType)r).ToList();
        
        var query = new GetExchangeVolumesQuery((ContractTradeType)request.TradeType, request.Start, request.End, contractsTypes);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
    
    [HttpPost("GetExchangeParticipants")]
    public async Task<IActionResult> GetExchangeParticipants(GetExchangeParticipantsRequest request, CancellationToken cancellationToken)
    {
        var contractsTypes = request.ContractTypes.Select(r => (ContractType)r).ToList();
        
        var query = new GetExchangeParticipantsQuery((ContractTradeType)request.TradeType, request.Start, request.End, contractsTypes);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
    
    [HttpGet("GetExchangeTotalSellersCount")]
    public async Task<IActionResult> GetExchangeTotalSellersCount(
        [FromQuery] long year, 
        [FromQuery] int type, 
        CancellationToken cancellationToken)
    {
        var query = new GetExchangeTotalSellersCountQuery(year, type);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
    
    [HttpGet("GetExchangeTotalBuyersCount")]
    public async Task<IActionResult> GetExchangeTotalBuyersCount(
        [FromQuery] long year, 
        [FromQuery] int type, 
        CancellationToken cancellationToken)
    {
        var query = new GetExchangeTotalBuyersCountQuery(year, type);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
    
    [HttpGet("GetExchangeAverageSellSum")]
    public async Task<IActionResult> GetExchangeAverageSellSum(
        [FromQuery] long year, 
        [FromQuery] int type, 
        CancellationToken cancellationToken)
    {
        var query = new GetExchangeAverageSellSumQuery(year, type);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
    
    [HttpGet("GetExchangeAverageBuySum")]
    public async Task<IActionResult> GetExchangeAverageBuySum(
        [FromQuery] long year, 
        [FromQuery] int type, 
        CancellationToken cancellationToken)
    {
        var query = new GetExchangeAverageBuySumQuery(year, type);

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result) : NotFound();
    }
}