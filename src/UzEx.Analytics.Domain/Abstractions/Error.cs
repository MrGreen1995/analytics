namespace UzEx.Analytics.Domain.Abstractions;

public record Error(string Code, string Message)
{
    public static Error None = new (string.Empty, string.Empty);

    public static Error NullVaue = new("Error.NullValue", "Null value was provided");
}