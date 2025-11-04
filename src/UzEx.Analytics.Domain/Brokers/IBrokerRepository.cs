namespace UzEx.Analytics.Domain.Brokers;

public interface IBrokerRepository
{
    void Add(Broker broker);
    Task<Broker?> GetByNewSpotKeyAsync(string key, CancellationToken cancellationToken = default);
    Task<Broker?> GetByOldSpotKeyAsync(string key, CancellationToken cancellationToken = default);
}