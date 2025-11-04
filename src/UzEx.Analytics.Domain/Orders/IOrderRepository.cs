namespace UzEx.Analytics.Domain.Orders;

public interface IOrderRepository
{
    void Add(Order order);
    
    Task<Order?> GetByBusinessKeyAsync(string key, CancellationToken cancellationToken = default);
}