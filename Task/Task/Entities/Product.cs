using System.ComponentModel.DataAnnotations;

namespace Taskk.Entities;

public class Product
{
    public int Id { get; set; }

    [Display(Name = "Title")]
    [Required]
    public string? Title { get; set; }

    [Display(Name = "Quantity")]
    [Required]
    public int Quantity { get; set; }

    [Display(Name = "Price")]
    [Required]
    public double Price { get; set; }

    [Display(Name = "Total Price with VAT")]
    public double TotalPriceWithVat { get; set; }
}