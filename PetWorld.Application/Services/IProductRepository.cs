using PetWorld.Application.DTOs;

namespace PetWorld.Application.Services;

public interface IProductRepository
{
    Task<List<ProductDto>> GetAllProductsAsync(CancellationToken ct);
}
