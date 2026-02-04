namespace PetWorld.Application.Services;

public interface IProductRepository
{
    Task<List<object>> GetAllProductsAsync(CancellationToken ct);
}
