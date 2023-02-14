using Taskk.Entities;

namespace Taskk.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    System.Threading.Tasks.Task AddAsync(Product newProduct);
    System.Threading.Tasks.Task UpdateAsync(int id, Product updateProduct);
    System.Threading.Tasks.Task DeleteAsync(Product deleteProduct);
}