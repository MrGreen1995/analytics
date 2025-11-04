using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Contracts.Errors;

public class ContractErrors
{
    public static Error NotFound = new ("Contract.Found", "Contract not found");
}