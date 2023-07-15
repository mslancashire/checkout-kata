using Checkout.App;
using Checkout.Domain;

namespace Checkout.Tests
{
    internal static class Helper
    {
        internal static ICheckoutItem CreateCheckoutItem(string sku = "A", int price = 10)
        {
            return new CheckoutItem(Guid.NewGuid(), new ProductSKU(sku, price));
        }
    }
}
