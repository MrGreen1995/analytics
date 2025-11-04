namespace UzEx.Analytics.Infrastructure.Dtos.Soliq;

public class SoliqResponse<T>
    where T : class
{
    public bool Success { get; init; }

    public string? Error { get; init; }
    
    public T? Data { get; init; }
}