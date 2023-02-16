using Taskk.Dtos.ProductAudit;
using Taskk.Entities;

namespace Taskk.Services;

public interface IProductAuditService
{
    Task<IEnumerable<ProductAudit>> GetAllAsync();
    Task AddAsync(ProductAudit productAudit);
    Task<List<ProductAuditDto>> FilterAsync(Filter filter);
}