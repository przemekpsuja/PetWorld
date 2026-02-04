using Microsoft.EntityFrameworkCore;
using PetWorld.Application.DTOs;
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

    public async Task<List<ProductDto>> GetAllProductsAsync(CancellationToken ct)
    {
        var products = await _dbContext.Products
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Description = p.Description
            })
            .ToListAsync(ct);

        return products;
    }
}
