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
        var products = await this.appDbContext.Products.ToListAsync();
        
        products.ForEach(product => product.VatCalculating(Vat.Value));

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id) => await this.appDbContext.Products.FindAsync(id);

    public async System.Threading.Tasks.Task AddAsync(Product newProduct)
    {
        await this.appDbContext.Products.AddAsync(newProduct);
        await this.appDbContext.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task UpdateAsync(int id, Product updateProduct)
    {
        var product = await this.appDbContext.Products.FindAsync(id);

        product!.Price = updateProduct.Price;
        product.Quantity = updateProduct.Quantity;
        product.Title = updateProduct.Title;

        await this.appDbContext.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteAsync(Product deleteProduct)
    {
        this.appDbContext.Remove(deleteProduct);
        await this.appDbContext.SaveChangesAsync();
    }
}