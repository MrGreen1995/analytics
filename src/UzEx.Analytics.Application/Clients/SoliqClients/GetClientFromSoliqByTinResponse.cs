namespace UzEx.Analytics.Application.Clients.SoliqClients;

public sealed class GetClientFromSoliqByTinResponse
{
    public int Ns10Code { get; set; }
    
    public int Ns11Code { get; set; }

    public string? ShortName { get; set; }
    
    public string? Tin { get; set; }

    public string? Name { get; set; }

    public int StatusCode { get; set; }

    public string? StatusName { get; set; }
    
    public string? Mfo { get; set; }
    
    public string? Account { get; set; }
    
    public string? Address { get; set; }
    
    public string? Oked { get; set; }
    
    public string? DirectorTin { get; set; }
    
    public string? Director { get; set; }
}