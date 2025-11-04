using System.Net.Http.Json;
using UzEx.Analytics.Application.Abstractions.Soliq;
using UzEx.Analytics.Application.Models.Soliq;
using UzEx.Analytics.Infrastructure.Dtos.Soliq;

namespace UzEx.Analytics.Infrastructure.Soliq;

public sealed class SoliqService : ISoliqService
{
    private readonly HttpClient _httpClient;

    public SoliqService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SoliqUserModel?> GetUserAsync(string userTin, CancellationToken cancellationToken)
    {
        var dto = await _httpClient.GetFromJsonAsync<SoliqResponse<SoliqUserDto>>(
            $"/Soliq/JurInfo/{userTin}",
            cancellationToken);

        if (dto == null)
        {
            return null;
        }

        if (!dto.Success)
        {
            return null;
        }
        
        return new  SoliqUserModel
        {
            Account = dto.Data!.Account,
            Director = dto.Data!.Director,
            Address = dto.Data!.Address,
            DirectorTin = dto.Data!.DirectorTin,
            Mfo = dto.Data!.Mfo,
            Name = dto.Data!.Name,
            Ns10Code = dto.Data!.Ns10Code,
            Ns11Code = dto.Data!.Ns11Code,
            Oked = dto.Data!.Oked,
            ShortName = dto.Data!.ShortName,
            StatusCode = dto.Data!.StatusCode,
            StatusName = dto.Data!.StatusName,
            Tin = dto.Data!.Tin
        };
    }
}