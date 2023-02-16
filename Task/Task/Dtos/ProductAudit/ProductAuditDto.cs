using Taskk.Dtos.AppUser;
using Taskk.Dtos.Product;

namespace Taskk.Dtos.ProductAudit;

public class ProductAuditDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int ProductId { get; set; }
    public ProductDto? ProductDto { get; set; }
    public string? UserId { get; set; }
    public AppUserDto? AppUserDto { get; set; }
    public DateTime ChangedDate { get; set; }
}