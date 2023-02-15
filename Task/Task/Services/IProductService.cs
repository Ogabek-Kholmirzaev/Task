using Taskk.Entities;

namespace Taskk.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product newProduct);
    Task UpdateAsync(int id, Product updateProduct);
    Task DeleteAsync(Product deleteProduct);
}