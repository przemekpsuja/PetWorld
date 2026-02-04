using PetWorld.Application.DTOs;

namespace PetWorld.Application.Contracts;

public interface IProductRepository
{
    Task<List<ProductDto>> GetAllProductsAsync(CancellationToken ct);
}
