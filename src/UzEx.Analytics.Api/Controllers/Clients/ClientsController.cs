using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.Clients.GetTopBuyersOverTime;
using UzEx.Analytics.Application.Clients.GetTopSellersOverTime;
using UzEx.Analytics.Application.Clients.GetTotalClientsCount;
using UzEx.Analytics.Application.Clients.GetTotalClientsCountByType;
using UzEx.Analytics.Application.Clients.SearchClients;

namespace UzEx.Analytics.Api.Controllers.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ISender _sender;

        public ClientsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("SearchClients")]
        public async Task<IActionResult> SearchClients([FromBody] SearchClientsRequest request, CancellationToken cancellationToken)
        {
            var query = new SearchClientsQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalClientsCount")]
        public async Task<IActionResult> TotalClientsCount(CancellationToken cancellationToken)
        {
            var query = new GetTotalClientsCountQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalClientsCountByType")]
        public async Task<IActionResult> TotalClientsCountByType(CancellationToken cancellationToken)
        {
            var query = new GetTotalClientsCountByTypeQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TopSellersOverTimeSpan")]
        public async Task<IActionResult> TopSellersOverTimeSpan([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endData, CancellationToken cancellationToken)
        {
            var request = new GetTopSellersOverTimeRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endData
            };

            var query = new GetTopSellersOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TopBuyersOverTimeSpan")]
        public async Task<IActionResult> TopBuyersOverTimeSpan([FromQuery] int pageNumber, [FromQuery] int pageSize, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endData, CancellationToken cancellationToken)
        {
            var request = new GetTopBuyersOverTimeRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endData
            };

            var query = new GetTopBuyersOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }
    }
}
