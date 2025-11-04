using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using UzEx.Analytics.Application.Deals.ExportDealsToExcel;
using UzEx.Analytics.Application.Deals.GetAnnulatedDealsPercentage;
using UzEx.Analytics.Application.Deals.GetClosedDealsPercentage;
using UzEx.Analytics.Application.Deals.GetDealsCostOverTime;
using UzEx.Analytics.Application.Deals.GetDealsCountOverTime;
using UzEx.Analytics.Application.Deals.GetDealsCountTrendOverTime;
using UzEx.Analytics.Application.Deals.GetDealsVolumeOverTime;
using UzEx.Analytics.Application.Deals.GetExportDealsSum;
using UzEx.Analytics.Application.Deals.GetForwardDealsSum;
using UzEx.Analytics.Application.Deals.GetFutureDealsSum;
using UzEx.Analytics.Application.Deals.GetImportDealsSum;
using UzEx.Analytics.Application.Deals.GetInternalDealsSum;
using UzEx.Analytics.Application.Deals.GetPaidDealsPercentage;
using UzEx.Analytics.Application.Deals.GetRevenueByContractFormOverTime;
using UzEx.Analytics.Application.Deals.GetRevenueByContractTypeOverTime;
using UzEx.Analytics.Application.Deals.GetRevenueByPlatformOverTime;
using UzEx.Analytics.Application.Deals.GetRevenueByTradeType;
using UzEx.Analytics.Application.Deals.GetRevenueByTradeTypeOverTime;
using UzEx.Analytics.Application.Deals.GetSpotDealsSum;
using UzEx.Analytics.Application.Deals.GetTotalDealsAmount;
using UzEx.Analytics.Application.Deals.GetTotalDealsCount;
using UzEx.Analytics.Application.Deals.GetTotalDealsCountOverTime;
using UzEx.Analytics.Application.Deals.GetTotalDealsSum;
using UzEx.Analytics.Application.Deals.GetTotalDealsSumOverTime;
using UzEx.Analytics.Application.Deals.GetTotalDealSumByMonth;
using UzEx.Analytics.Application.Deals.GetTotalDealsVolumeOverTime;
using UzEx.Analytics.Application.Deals.SearchDeals;

namespace UzEx.Analytics.Api.Controllers.Deals
{
    [ApiController]
    [Route("api/[controller]")]
    public class DealsController : ControllerBase
    {
        private readonly ISender _sender;

        public DealsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("SearchDeals")]
        public async Task<IActionResult> SearchDeals([FromBody] SearchDealsRequest request, CancellationToken cancellationToken)
        {
            var query = new SearchDealsQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalCount")]
        public async Task<IActionResult> TotalCount(CancellationToken cancellationToken)
        {
            var query = new GetTotalDealsCountQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("DealsCountOverTimeSpan")]
        public async Task<IActionResult> DealsCountOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetDealsCountOverTimeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetDealsCountOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("DealsCountTrendOverTimeSpan")]
        public async Task<IActionResult> DealsCountTrendOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetDealsCountTrendOverTimeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetDealsCountTrendOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalAmount")]
        public async Task<IActionResult> TotalAmount(CancellationToken cancellationToken)
        {
            var query = new GetTotalDealsAmountQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("DealsAmountOverTimeSpan")]
        public async Task<IActionResult> DealsAmountOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetDealsVolumeOverTimeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetDealsVolumeOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalSum")]
        public async Task<IActionResult> TotalSum(CancellationToken cancellationToken)
        {
            var query = new GetTotalDealsSumQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("DealsSumOverTimeSpan")]
        public async Task<IActionResult> DealsSumOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetDealsCostOverTimeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetDealsCostOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("PaidDealsPercentage")]
        public async Task<IActionResult> PaidDealsPercentage(CancellationToken cancellationToken)
        {
            var query = new GetPaidDealsPercentageQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("ClosedDealsPercentage")]
        public async Task<IActionResult> ClosedDealsPercentage(CancellationToken cancellationToken)
        {
            var query = new GetClosedDealsPercentageQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("AnnulatedDealsPercentage")]
        public async Task<IActionResult> AnnulatedDealsPercentage(CancellationToken cancellationToken)
        {
            var query = new GetAnnulatedDealsPercentageQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("SpotDealsSum")]
        public async Task<IActionResult> SpotDealsSum(CancellationToken cancellationToken)
        {
            var query = new GetSpotDealsSumQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("ForwardDealsSum")]
        public async Task<IActionResult> ForwardDealsSum(CancellationToken cancellationToken)
        {
            var query = new GetForwardDealsSumQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("FutureDealsSum")]
        public async Task<IActionResult> FutureDealsSum(CancellationToken cancellationToken)
        {
            var query = new GetFutureDealsSumQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("InternalDealsSum")]
        public async Task<IActionResult> InternalDealsSum(CancellationToken cancellationToken)
        {
            var query = new GetInternalDealsSumQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("ExportDealsSum")]
        public async Task<IActionResult> ExportDealsSum(CancellationToken cancellationToken)
        {
            var query = new GetExportDealsSumQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("ImportDealsSum")]
        public async Task<IActionResult> GetImportDealsSum(CancellationToken cancellationToken)
        {
            var query = new GetImportDealsSumQuery();

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalDealSumByMonth")]
        public async Task<IActionResult> TotalDealSumByMonth([FromQuery] long year, CancellationToken cancellationToken)
        {
            var query = new GetTotalDealSumByMonthQuery(year);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("DownloadExcelFile")]
        public async Task<IActionResult> ExportToExcel([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new ExportDealsToExcelRequest()
            {
                StartDate = startDate,
                EndDate = endDate
            };
            var query = new ExportDealsToExcelQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsSuccess)
            {
                byte[] fileBytes = result.Value;

                string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                string fileName = $"Deals_{request.StartDate}_{request.EndDate}.xlsx";


                return File(fileBytes, contentType, fileName);
            }
            return NotFound();
        }

        [HttpGet("RevenueByTradeType")]
        public async Task<IActionResult> RevenueByTradeType([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetRevenueByTradeTypeRequest()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetRevenueByTradeTypeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("RevenueByTradeTypeOverTimeSpan")]
        public async Task<IActionResult> RevenueByTradeTypeOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetRevenueByTradeTypeOverTimeRequest()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetRevenueByTradeTypeOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalDealsCountOverTimeSpan")]
        public async Task<IActionResult> TotalDealsCountOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetTotalDealsCountOverTimeRequest()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetTotalDealsCountOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalDealsVolumeOverTimeSpan")]
        public async Task<IActionResult> TotalDealsVolumeOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetTotalDealsVolumeOverTimeRequest()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetTotalDealsVolumeOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("TotalDealsSumOverTimeSpan")]
        public async Task<IActionResult> TotalDealsSumOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetTotalDealsSumOverTimeRequest
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetTotalDealsSumOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("RevenueByPlatformOverTimeSpan")]
        public async Task<IActionResult> RevenueByPlatformOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetRevenueByPlatformOverTimeRequest()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetRevenueByPlatformOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("RevenueByContractTypeOverTimeSpan")]
        public async Task<IActionResult> RevenueByContractTypeOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetRevenueByContractTypeOverTimeRequest()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetRevenueByContractTypeOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }

        [HttpGet("RevenueByContractFormOverTimeSpan")]
        public async Task<IActionResult> RevenueByContractFormOverTimeSpan([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate, CancellationToken cancellationToken)
        {
            var request = new GetRevenueByContractFormOverTimeRequest()
            {
                StartDate = startDate,
                EndDate = endDate
            };

            var query = new GetRevenueByContractFormOverTimeQuery(request);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : NotFound();
        }
    }
}
