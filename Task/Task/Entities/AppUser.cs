using Microsoft.AspNetCore.Identity;

namespace Taskk.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }

    public virtual List<ProductAudit>? Audits { get; set; }
}