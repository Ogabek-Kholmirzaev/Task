using Taskk.Entities;

namespace Taskk.Services;

public interface IProductAuditService
{
    Task<IEnumerable<ProductAudit>> GetAllAsync();
    Task AddAsync(ProductAudit productAudit);
    Task<IEnumerable<ProductAudit>> FilterAsync(Filter filter);
}