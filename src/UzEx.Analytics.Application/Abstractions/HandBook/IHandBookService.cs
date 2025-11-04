using UzEx.Analytics.Application.Models.HandBook;

namespace UzEx.Analytics.Application.Abstractions.HandBook;

public interface IHandBookService
{
    Task<List<County>> GetAllCountriesAsync(CancellationToken cancellationToken);
    Task<County?> GetCountryByIdAsync(string id, CancellationToken cancellationToken);
    Task<List<Region>> GetAllUzbRegionsAsync(CancellationToken cancellationToken, int? country = 1);
    Task<Region?> GetUzbRegionByIdAsync(string id, CancellationToken cancellationToken, int? country = 1);
    Task<List<District>> GetAllDistrictsFromUzbRegion(int region, CancellationToken cancellationToken);
    Task<District?> GetDistrictById(string regionId, string id, CancellationToken cancellationToken);
    Task<List<Currency>> GetAllCurrencyAsync(CancellationToken cancellationToken);
    Task<Currency?> GetCurrencyByNumCodeAsync(string code, CancellationToken cancellationToken);
}
