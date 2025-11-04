namespace UzEx.Analytics.Infrastructure.Dtos.NewSpot;

public class NewSpotResponse<T>
{
    public bool Success { get; init; }

    public string? Error { get; init; }
    
    public T? Data { get; init; }
}