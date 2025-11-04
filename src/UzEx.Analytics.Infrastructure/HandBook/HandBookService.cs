using System.Net.Http.Json;
using UzEx.Analytics.Application.Abstractions.HandBook;
using UzEx.Analytics.Application.Models.HandBook;
using UzEx.Analytics.Infrastructure.Dtos.HandBook;

namespace UzEx.Analytics.Infrastructure.HandBook;

public sealed class HandBookService : IHandBookService
{
    private readonly HttpClient _httpClient;

    public HandBookService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<County>> GetAllCountriesAsync(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<HandBookResponseDto<CountryDto>>(
            "/Country/GetAll",
            cancellationToken);

        if (response is not { Success: true })
        {
            return [];
        }

        var dto = response.Data;

        if (dto is null)
        {
            return [];
        }

        return dto
            .Select(country => new County
            {
                Id = country.Id,
                Name = country.Name,
                ShortName = country.ShortName,
                Code = country.Code,
                Number = country.Number,
                OftenUse = country.OftenUse
            })
            .ToList();
    }

    public async Task<County?> GetCountryByIdAsync(string id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<HandBookResponseDto<CountryDto>>(
            "/Country/GetAll",
            cancellationToken);

        if (response is not { Success: true })
        {
            return null;
        }

        var dto = response.Data;

        return dto?.Select(country => new County
        {
            Id = country.Id,
            Name = country.Name,
            ShortName = country.ShortName,
            Code = country.Code,
            Number = country.Number,
            OftenUse = country.OftenUse
        }).FirstOrDefault(c => c.Id.ToString() == id);
    }

    public async Task<List<Region>> GetAllUzbRegionsAsync(CancellationToken cancellationToken, int? country = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<HandBookResponseDto<RegionDto>>(
            $"/Region/GetAll/{country}",
            cancellationToken
            );

        if (response is not { Success: true })
        {
            return [];
        }
        
        var dto = response.Data;

        if (dto is null)
        {
            return [];
        }

        return dto
            .Select(region => new Region
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Number = region.Number,
                ShortName = region.ShortName,
                CountryId = region.CountryId,
                CountryName = region.CountryName,
                CountryShortName = region.CountryShortName
            })
            .ToList();
    }

    public async Task<Region?> GetUzbRegionByIdAsync(string id, CancellationToken cancellationToken, int? country = 1)
    {
        var response = await _httpClient.GetFromJsonAsync<HandBookResponseDto<RegionDto>>(
            $"/Region/GetAll/{country}",
            cancellationToken
        );

        if (response is not { Success: true })
        {
            return null;
        }
        
        var dto = response.Data;

        return dto?.Select(region => new Region
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            Number = region.Number,
            ShortName = region.ShortName,
            CountryId = region.CountryId,
            CountryName = region.CountryName,
            CountryShortName = region.CountryShortName
        }).FirstOrDefault(r => r.Id.ToString() == id);
    }

    public async Task<List<District>> GetAllDistrictsFromUzbRegion(int region, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<HandBookResponseDto<DistrictDto>>(
            $"/District/GetAll/{region}",
            cancellationToken);

        if (response is not { Success: true })
        {
            return [];
        }
        
        var dto = response.Data;

        if (dto is null)
        {
            return [];
        }

        return dto
            .Select(distric => new District
            {
                Id = distric.Id,
                Name = distric.Name,
                Code = distric.Code,
                Number = distric.Number,
                ShortName = distric.ShortName,
                RegionId = distric.RegionId,
                RegionName = distric.RegionName,
                RegionShortName = distric.RegionShortName
            })
            .ToList();
    }

    public async Task<District?> GetDistrictById(string regionId, string id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<HandBookResponseDto<DistrictDto>>(
            $"/District/GetAll/{regionId}",
            cancellationToken);

        if (response is not { Success: true })
        {
            return null;
        }
        
        var dto = response.Data;

        return dto?.Select(distric => new District
            {
                Id = distric.Id,
                Name = distric.Name,
                Code = distric.Code,
                Number = distric.Number,
                ShortName = distric.ShortName,
                RegionId = distric.RegionId,
                RegionName = distric.RegionName,
                RegionShortName = distric.RegionShortName
            }).FirstOrDefault(d => d.Id.ToString() == id);
    }

    public async Task<List<Currency>> GetAllCurrencyAsync(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<HandBookResponseDto<CurrencyDto>>(
            "/Currency/GetAll",
            cancellationToken);

        if (response is not { Success: true })
        {
            return [];
        }
        
        var dto = response.Data;

        if (dto is null)
        {
            return [];
        }
        
        return dto
            .Select(currency => new Currency
            {
                Id = currency.Id,
                Name = currency.Name,
                Code = currency.Code,
                Number = currency.Number,
                ShortName = currency.ShortName
            }).ToList();
    }

    public async Task<Currency?> GetCurrencyByNumCodeAsync(string code, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetFromJsonAsync<HandBookResponseDto<CurrencyDto>>(
            "/Currency/GetAll",
            cancellationToken);

        if (response is not { Success: true })
        {
            return null;
        }
        
        var dto = response.Data;

        return dto?.Select(currency => new Currency
        {
            Id = currency.Id,
            Name = currency.Name,
            Code = currency.Code,
            Number = currency.Number,
            ShortName = currency.ShortName
        }).FirstOrDefault(a => a.Number == code);
    }
}
