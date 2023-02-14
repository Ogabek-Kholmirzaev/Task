using Microsoft.AspNetCore.Identity;

namespace Taskk.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
}