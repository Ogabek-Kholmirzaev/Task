using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskk.Entities;

public class ProductAudit
{
    public int Id { get; set; }

    [Display(Name = "Changes")]
    public string? Name { get; set; }

    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product? Product { get; set; }

    public string? UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual AppUser? User { get; set; }

    [Display(Name = "Time")]
    public DateTime ChangedDate { get; set; }
}