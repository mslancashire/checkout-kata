namespace Checkout.Domain.Calculators
{
    /// <summary>
    /// Discount chcekout calculator that totals up all the checkout items using its product price but adding a discount if a provided discount rule is applicable.
    /// 
    /// Should not be used inconjuction with the Basic Caculator.
    /// </summary>
    public class CheckoutCalculatorWithDiscounts : ICheckoutPriceCalculator
    {
        private readonly IDiscountFactory _discountRulesFactory;

        public CheckoutCalculatorWithDiscounts(IDiscountFactory discountRulesFactory)
        {
            ArgumentNullException.ThrowIfNull(discountRulesFactory);
            
            _discountRulesFactory = discountRulesFactory;
        }

        private IEnumerable<IDiscount> GetFreshDiscountRules() => _discountRulesFactory.CreateDiscountRules();

        public int GetTotalPrice(IEnumerable<ICheckoutItem> checkoutItems)
        {
            if (checkoutItems == null || checkoutItems.Any() == false)
            {
                return Settings.EMPTY_CHECKOUT_PRICE;
            }

            var calculatedDiscounts = GetFreshDiscountRules().Select(dr => dr.CalculateDiscounts(checkoutItems));

            var checkoutItemsWithDiscounts = calculatedDiscounts.SelectMany(cd => cd.AppliedTo);
            var totalDiscountedPrice = calculatedDiscounts.Sum(cd => cd.DiscountedPrice);

            var checkoutItemsWithoutDiscounts = checkoutItems.Where(ci => checkoutItemsWithDiscounts.Contains(ci.Id) == false);
            var totalNonDiscountedPrice = checkoutItemsWithoutDiscounts.Sum(ci => ci.Product.Price);

            return totalNonDiscountedPrice + totalDiscountedPrice;
        }
    }
}
