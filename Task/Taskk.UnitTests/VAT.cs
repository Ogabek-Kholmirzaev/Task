using Taskk.Entities;
using Taskk.Statics;

namespace Taskk.UnitTests;

public class VAT
{

    [Fact]
    public void VatCalculatingTest()
    {
        var products = new List<Product>()
        {
            new Product()
            {
                Id = 1,
                Title = "HDD 1TB",
                Quantity = 55,
                Price = 74.09
            },
            new Product()
            {
                Id = 2,
                Title = "HDD SSD 512GB",
                Quantity = 102,
                Price = 190.99
            },
            new Product()
            {
                Id = 3,
                Title = "RAM DDR4 16GB",
                Quantity = 47,
                Price = 80.32
            }
        };

        foreach (var product in products)
        {
            product.VatCalculating(Vat.Value);
            
            Assert.Equal(product.TotalPriceWithVat, (product.Quantity * product.Price) / (1 + Vat.Value));
        }
    }
}