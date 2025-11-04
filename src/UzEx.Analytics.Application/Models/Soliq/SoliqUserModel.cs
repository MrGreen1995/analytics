namespace UzEx.Analytics.Application.Models.Soliq;

public sealed class SoliqUserModel
{
    public int Ns10Code { get; init; }
    
    public int Ns11Code { get; init; }

    public string? ShortName { get; init; }
    
    public string? Tin { get; init; }
    
    public string? Name { get; init; }
    
    public int StatusCode { get; init; }
    
    public string? StatusName { get; init; }
    
    public string? Mfo { get; init; }
    
    public string? Account { get; init; }
    
    public string? Address { get; init; }
    
    public string? Oked { get; init; }
    
    public string? DirectorTin { get; init; }
    
    public string? Director { get; init; }
}