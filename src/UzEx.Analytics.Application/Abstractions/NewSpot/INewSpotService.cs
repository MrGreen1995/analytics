using UzEx.Analytics.Application.Models.NewSpot;

namespace UzEx.Analytics.Application.Abstractions.NewSpot;

public interface INewSpotService
{
    Task<IReadOnlyList<NewSpotOrderModel>> GetOrders(DateTime date, CancellationToken cancellationToken);
    Task<IReadOnlyList<NewSpotDealModel>> GetDeals(DateTime date, CancellationToken cancellationToken);
    Task<NewSpotContractModel?> GetContract(long id, CancellationToken cancellationToken);
    Task<NewSpotClientModel?> GetClient(string id, CancellationToken cancellationToken);
    Task<NewSpotBrokerModel?> GetBroker(string id, CancellationToken cancellationToken);
}