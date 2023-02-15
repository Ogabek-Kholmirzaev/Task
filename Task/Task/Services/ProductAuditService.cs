using Microsoft.EntityFrameworkCore;
using Taskk.Data;
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

    public async Task<IEnumerable<ProductAudit>> FilterAsync(Filter filter)
    {
        var productAudits = await this.appDbContext.ProductAudits.ToListAsync();
        var result = new List<ProductAudit>();

        filter.StartDate ??= productAudits.Min(productAudit => productAudit.ChangedDate);
        filter.EndDate ??= productAudits.Max(productAudit => productAudit.ChangedDate);

        foreach (var productAudit in productAudits)
        {
            var duration1 = (long)((TimeSpan)(productAudit.ChangedDate - filter.StartDate)).TotalSeconds;
            var duration2 = (long)((TimeSpan)(productAudit.ChangedDate - filter.EndDate)).TotalSeconds;

            if (duration1 >= 0 && duration2 <= 0)
                result.Add(productAudit);
        }

        return result;
    }
}