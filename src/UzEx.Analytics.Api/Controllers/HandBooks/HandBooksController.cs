using MediatR;
using Microsoft.AspNetCore.Mvc;
using UzEx.Analytics.Application.HandBook.GetCountries;
using UzEx.Analytics.Application.HandBook.GetCurrencies;
using UzEx.Analytics.Application.HandBook.GetDistricts;
using UzEx.Analytics.Application.HandBook.GetRegions;

namespace UzEx.Analytics.Api.Controllers.HandBooks
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandBooksController : ControllerBase
    {
        private readonly ISender _sender;

        public HandBooksController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetCountries")]
        public async Task<IActionResult> GetCountries(CancellationToken cancellationToken)
        {
            var query = new GetCountriesQuery();

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("GetRegions")]
        public async Task<IActionResult> GetRegions(CancellationToken cancellationToken)
        {
            var query = new GetRegionsQuery();

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("GetDistricts")]
        public async Task<IActionResult> GetDistricts(int regionId, CancellationToken cancellationToken)
        {
            var query = new GetDistrictsQuery(regionId);

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("GetCurrencies")]
        public async Task<IActionResult> GetCurrencies(CancellationToken cancellationToken)
        {
            var query = new GetCurrenciesQuery();

            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
