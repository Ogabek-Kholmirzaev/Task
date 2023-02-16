using Mapster;
using Microsoft.EntityFrameworkCore;
using Taskk.Data;
using Taskk.Dtos.AppUser;
using Taskk.Dtos.Product;
using Taskk.Dtos.ProductAudit;
using Taskk.Entities;

namespace Taskk.Services;

public class ProductAuditService : IProductAuditService
{
    private readonly AppDbContext appDbContext;

    public ProductAuditService(AppDbContext appDbContext)
    {
        this.appDbContext = appDbContext;
    }

    public async Task<IEnumerable<ProductAudit>> GetAllAsync() => 
        await this.appDbContext.ProductAudits.ToListAsync();

    public async Task AddAsync(ProductAudit productAudit)
    {
        await this.appDbContext.ProductAudits.AddAsync(productAudit);
        await this.appDbContext.SaveChangesAsync();
    }

    public async Task<List<ProductAuditDto>> FilterAsync(Filter filter)
    {
        var productAudits = await this.appDbContext.ProductAudits.ToListAsync();
        var result = new List<ProductAuditDto>();

        filter.StartDate ??= productAudits.Min(productAudit => productAudit.ChangedDate);
        filter.EndDate ??= productAudits.Max(productAudit => productAudit.ChangedDate);

        foreach (var productAudit in productAudits)
        {
            var duration1 = (long)((TimeSpan)(productAudit.ChangedDate - filter.StartDate)).TotalSeconds;
            var duration2 = (long)((TimeSpan)(productAudit.ChangedDate - filter.EndDate)).TotalSeconds;

            if (duration1 >= 0 && duration2 <= 0)
            {
                var productAuditDto = new ProductAuditDto()
                {
                    Id = productAudit.Id,
                    AppUserDto = productAudit.User!.Adapt<AppUserDto>(),
                    ProductDto = productAudit.Product!.Adapt<ProductDto>(),
                    Name = productAudit.Name,
                    UserId = productAudit.UserId,
                    ProductId = productAudit.ProductId,
                    ChangedDate = productAudit.ChangedDate
                };

                result.Add(productAuditDto);
            }
        }

        return result;
    }
}