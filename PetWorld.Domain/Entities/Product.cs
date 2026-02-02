namespace PetWorld.Domain.Entities;

/// <summary>
/// Represents a product available in the PetWorld store.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category to which the product belongs.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty; 
}
