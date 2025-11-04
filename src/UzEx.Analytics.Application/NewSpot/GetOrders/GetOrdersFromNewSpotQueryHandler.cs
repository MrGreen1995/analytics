using UzEx.Analytics.Application.Abstractions.Messaging;
using UzEx.Analytics.Application.Abstractions.NewSpot;
using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Orders.Errors;

namespace UzEx.Analytics.Application.NewSpot.GetOrders;

public sealed class GetOrdersFromNewSpotQueryHandler : IQueryHandler<GetOrdersFromNewSpotQuery, IReadOnlyList<GetOrdersFromNewSpotResponse>>
{
    private readonly INewSpotService _newSpotService;

    public GetOrdersFromNewSpotQueryHandler(INewSpotService newSpotService)
    {
        _newSpotService = newSpotService;
    }

    public async Task<Result<IReadOnlyList<GetOrdersFromNewSpotResponse>>> Handle(GetOrdersFromNewSpotQuery request, CancellationToken cancellationToken)
    {
        var orders = await _newSpotService.GetOrders(request.date, cancellationToken);

        if (orders == null)
        {
            return Result.Failure<IReadOnlyList<GetOrdersFromNewSpotResponse>>(OrderErrors.NotFound);
        }

        var response = orders.Select(order => new GetOrdersFromNewSpotResponse
        {
            ClientId = order.ClientId,
            BrokerId = order.BrokerId,
            Id = order.Id,
            Direction = order.Direction,
            Price = order.Price,
            Amount = order.Amount,
            ContractId = order.ContractId,
            Date = order.Date,
            Status = order.Status,
            ParentId = order.ParentId
        }).ToList();

        return response;
    }
}
