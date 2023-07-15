namespace Checkout.Domain.DiscountRules
{
    public class BuyXGetXPercentageDiscountRule : IDiscount
    {
        private readonly string _sku;
        private readonly int _quantity;
        private readonly int _percentagediscount;

        /// <summary>
        /// Creates a discount rule that will apply a percentage discount to a certain number of SKUs.
        /// 
        /// e.g.    Buy 5 of x at £10 each and get a 20% discount, price will be £40, saving of £10 instead of total price of £50.
        ///         Any items over the quantity of 5 will be priced at full. Discount can be applied multiple times, say if another 5 items were added.
        /// </summary>
        /// <param name="name">Name of the discount.</param>
        /// <param name="sku">The SKU the discount should apply too.</param>
        /// <param name="quantity">The quantity of the SKU required to activate the discount, only applies to that quantity of items.</param>
        /// <param name="percentagediscount">The percentage of the discount in natural numbers, e.g. 5%.</param>
        public BuyXGetXPercentageDiscountRule(string name, string sku, int quantity, int percentagediscount)
        {
            Name = name;
            _sku = sku;
            _quantity = quantity;
            _percentagediscount = percentagediscount;
        }

        public string Name { get; private set; }

        public (bool IsApplicable, int DiscountedPrice, IEnumerable<Guid> AppliedTo) CalculateDiscounts(IEnumerable<ICheckoutItem> items)
        {
            if (items == null || items.Any() == false)
            {
                return Settings.NO_DISCOUNT;
            }

            var applicableItems = items.Where(i => i.Product.SKU == _sku);
            if (applicableItems.Any() == false)
            {
                return Settings.NO_DISCOUNT;
            }

            var discountedSets = applicableItems
                .Select((ai, index) => new { Set = index / _quantity, Item = ai })
                .GroupBy(k => k.Set)
                .Select(g => g.Select(g => g.Item).ToArray())
                .Where(ds => ds.Count() == _quantity);

            var discountedItems = discountedSets.SelectMany(ds => ds);

            if (discountedSets.Any() == false)
            {
                return Settings.NO_DISCOUNT;
            }

            int totalPrice = discountedItems.Sum(ds => ds.Product.Price);
            int discountedPrice = (int) (totalPrice - (totalPrice * ((double) _percentagediscount / 100)));

            return (true, discountedPrice, discountedItems.Select(di => di.Id));
        }
    }
}
