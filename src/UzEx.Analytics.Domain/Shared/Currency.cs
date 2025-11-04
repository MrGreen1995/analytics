namespace UzEx.Analytics.Domain.Shared;

public sealed record Currency
{
    private Currency(string code) => Code = code;
    
    public static readonly Currency Usd = new("USD");
    public static readonly Currency Eur = new("EUR");
    public static readonly Currency Uzs = new("UZS");
    public static readonly Currency Jpy = new("JPY");
    public static readonly Currency Gbp = new("GBP");
    public static readonly Currency Aud = new("AUD");
    public static readonly Currency Chf = new("CHF");
    public static readonly Currency Kzt = new("KZT");
    public static readonly Currency Amd = new("AMD");
    public static readonly Currency Ats = new("ATS");
    public static readonly Currency Azm = new("AZM");
    public static readonly Currency Byn = new("BYN");
    public static readonly Currency Cny = new("CNY");
    
    internal static readonly Currency None = new("");
    
    public string Code { get; init; }
    
    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
               throw new ApplicationException("The currency code is invalid");
    }

    public static readonly IReadOnlyList<Currency> All =
    [
        Uzs,
        Usd,
        Eur,
        Jpy,
        Gbp,
        Aud,
        Chf,
        Kzt,
        Amd,
        Ats,
        Azm,
        Byn,
        Cny
    ];
};