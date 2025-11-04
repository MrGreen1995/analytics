namespace UzEx.Analytics.Infrastructure.Dtos.HandBook;

public sealed class HandBookResponseDto<T>
    where T : class
{
    public bool Success { get; init; }
    public List<T>? Data { get; init; }

    public string? Error { get; init; }
}
