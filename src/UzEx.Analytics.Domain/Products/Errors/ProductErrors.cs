using UzEx.Analytics.Domain.Abstractions;

namespace UzEx.Analytics.Domain.Products.Errors;

public class ProductErrors
{
    public static Error NotFound = new ("Product.Found", "Product not found");
}