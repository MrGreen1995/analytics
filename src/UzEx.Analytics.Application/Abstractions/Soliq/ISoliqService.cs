using UzEx.Analytics.Application.Models.Soliq;

namespace UzEx.Analytics.Application.Abstractions.Soliq;

public interface ISoliqService
{
    Task<SoliqUserModel?> GetUserAsync(string userTin, CancellationToken cancellationToken);
}