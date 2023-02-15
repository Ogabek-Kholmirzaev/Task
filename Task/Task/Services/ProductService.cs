using Microsoft.EntityFrameworkCore;
using Taskk.Data;
using Taskk.Entities;
using Taskk.Statics;

namespace Taskk.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext appDbContext;

    public ProductService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await appDbContext.Products.ToListAsync();
        
        products.ForEach(product => product.VatCalculating(Vat.Value));

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id) => await appDbContext.Products.FindAsync(id);

    public async Task AddAsync(Product newProduct)
    {
        await appDbContext.Products.AddAsync(newProduct);
        await appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, Product updateProduct)
    {
        var product = await appDbContext.Products.FindAsync(id);

        product!.Price = updateProduct.Price;
        product.Quantity = updateProduct.Quantity;
        product.Title = updateProduct.Title;

        await appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product deleteProduct)
    {
        appDbContext.Remove(deleteProduct);
        await appDbContext.SaveChangesAsync();
    }
}