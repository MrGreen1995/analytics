namespace UzEx.Analytics.Domain.Deals;

public interface IDealRepository
{
    void Add(Deal deal);
    Task<Deal?> GetByBusinessKey(string businessKey, CancellationToken cancellationToken = default);
    Task<Deal?> GetById(Guid id, CancellationToken cancellationToken = default);
}