namespace UzEx.Analytics.Application.Models.HandBook;

public sealed class Currency
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public required string Code { get; init; }

    public required string Number { get; init; }

    public string? ShortName { get; init; }
}
