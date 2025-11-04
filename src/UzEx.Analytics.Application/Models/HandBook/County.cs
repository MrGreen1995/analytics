namespace UzEx.Analytics.Application.Models.HandBook;

public class County
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public required string ShortName { get; init; }

    public required string Code { get; init; }

    public required string Number { get; init; }

    public bool OftenUse { get; init; }
}
