using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Clients.Errors;

public class ClientErrors
{
    public static Error NotFound = new ("Client.Found", "Client not found");
}