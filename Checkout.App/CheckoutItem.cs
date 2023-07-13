using Checkout.Domain;

namespace Checkout.App
{
    public class CheckoutItem : ICheckoutItem
    {
        public CheckoutItem(Guid id, IPricedSKU product)
        {
            Id = id;
            Product = product ?? ProductSKU.CreateEmptyProduct();
        }

        public Guid Id { get; private set; }

        public IPricedSKU Product { get; private set; }
    }
}
