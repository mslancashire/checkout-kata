namespace Checkout.Domain
{
    public interface ICheckoutPriceCalculator
    {
        int GetTotalPrice(IEnumerable<ICheckoutItem> checkoutItems);
    }
}
