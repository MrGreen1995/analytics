using UzEx.Analytics.Domain.Abstractions;
using UzEx.Analytics.Domain.Contracts;

namespace UzEx.Analytics.Domain.Products;

public sealed class Product : Entity
{
    public DateTime CreatedOnUtc { get; private set; }

    public ProductCode Code { get; private set; }
    
    public ProductName Name { get; private set; }
    
    public IReadOnlyCollection<Contract>? Contracts { get; init; }

    private Product()
    {
    }

    public Product(
        Guid id,
        DateTime createdOnUtc,
        ProductCode code,
        ProductName name) : base(id)
    {
        CreatedOnUtc = createdOnUtc;
        Code = code;
        Name = name;
    }
}