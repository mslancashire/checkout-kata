namespace Checkout.Domain.Calculators
{
    /// <summary>
    /// Basic chcekout calculator that just totals up all the checkout items using its product price.
    /// 
    /// Should not be used inconjuction with the Caculator With Discounts.
    /// </summary>
    public class CheckoutCalculatorBasic : ICheckoutPriceCalculator
    {
        public int GetTotalPrice(IEnumerable<ICheckoutItem> checkoutItems)
        {
            if (checkoutItems == null || checkoutItems.Any() == false)
            {
                return Settings.EMPTY_CHECKOUT_PRICE;
            }

            return checkoutItems.Sum(ci => ci.Product.Price);
        }
    }
}
