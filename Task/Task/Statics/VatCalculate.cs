using Taskk.Entities;

namespace Taskk.Statics;

public static class VatCalculate
{
    public static void VatCalculating(this Product product, int vat)
    {
        try
        {
            product.TotalPriceWithVat = (product.Quantity * product.Price) * (1 + vat);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}