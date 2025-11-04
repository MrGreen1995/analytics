namespace UzEx.Analytics.Domain.Clients;

public interface IClientRepository
{
    void Add(Client client);
    
    Task<Client?> GetRezidentByRegNumAsync(string regNum, CancellationToken cancellationToken = default);
    Task<Client?> GetByNewSpotKeyAsync(string key, CancellationToken cancellationToken = default);
    Task<Client?> GetByOldSpotKeyAsync(string key, CancellationToken cancellationToken = default);
}