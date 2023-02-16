using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Taskk.Entities;

public class Product
{
    public int Id { get; set; }

    [Display(Name = "Title")]
    [Required]
    public string? Title { get; set; }

    [Display(Name = "Quantity")]
    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }

    [Display(Name = "Price")]
    [Required]
    [Range(0, double.MaxValue)]
    public double Price { get; set; }

    [Display(Name = "Total Price with VAT")]
    [NotMapped]
    public double TotalPriceWithVat { get; set; }

    public virtual List<ProductAudit>? Audits { get; set; }
}