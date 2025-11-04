namespace UzEx.Analytics.Domain.Contracts;

public interface IContractRepository
{
    Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Contract>> GetAllAsync(CancellationToken cancellationToken = default);
    void Add(Contract contract);
    Task<Contract?> GetByBusinessKeyAsync(string businessKey, ContractPlatformType platformType, CancellationToken cancellationToken = default);
}