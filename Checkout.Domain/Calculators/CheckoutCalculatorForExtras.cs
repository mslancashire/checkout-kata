namespace Checkout.Domain.Calculators
{
    /// <summary>
    /// A checkout calculator that provides the total prices of extra items required for the current checkout.
    /// This currently supports pricing additional bags at £5 where a bag has a capacity of 5 items.
    /// 
    /// Can be unsed in conjuction with other checkout calculators.
    /// </summary>
    public class CheckoutCalculatorForExtras : ICheckoutPriceCalculator
    {
        public int GetTotalPrice(IEnumerable<ICheckoutItem> checkoutItems)
        {
            if (checkoutItems == null || checkoutItems.Any() == false)
            {
                return Settings.EMPTY_CHECKOUT_PRICE;
            }

            var extraPriceForBags = 0;

            for (int i = 0; i < checkoutItems.Count(); i++)
            {
                if (i % Settings.BAG_ITEM_CAPACITY == 0)
                {
                    extraPriceForBags += Settings.PRODUCT_PRICE_BAG;
                }
            }

            return extraPriceForBags;
        }
    }
}
