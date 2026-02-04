using Microsoft.EntityFrameworkCore;
using PetWorld.Application.Services;
using PetWorld.Infrastructure.Data;

namespace PetWorld.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly PetWorldDbContext _dbContext;

    public ProductRepository(PetWorldDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<object>> GetAllProductsAsync(CancellationToken ct)
    {
        var products = await _dbContext.Products
            .Select(p => new
            {
                p.Id,
                p.Name,
                p.Category,
                p.Price,
                p.Description
            })
            .ToListAsync(ct);

        return products.Cast<object>().ToList();
    }
}
