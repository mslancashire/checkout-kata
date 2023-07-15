namespace Checkout.Domain.DiscountRules
{
    /// <summary>
    /// A discount rules that can be used to discount a particular SKU when the checkout contains equal to or greater than a set quantity.
    /// </summary>
    public class SKUQuantityDiscountRule : IDiscount
    {
        private readonly string _name;
        private readonly string _sku;
        private readonly int _quantity;
        private readonly int _price;

        public SKUQuantityDiscountRule(string name, string sku, int quantity, int price)
        {
            _name = name;
            _sku = sku;
            _quantity = quantity;
            _price = price;
        }

        public string Name { get => _name; }

        public int DiscountedPrice { get => _price; }

        public (bool IsApplicable, int DiscountedPrice, IEnumerable<Guid> AppliedTo) CalculateDiscounts(IEnumerable<ICheckoutItem> items)
        {
            if (items == null || items.Any() == false)
            {
                return Settings.NO_DISCOUNT;
            }

            var applicableItems = items.Where(i => _sku == i.Product.SKU);

            if (applicableItems.Count() >= _quantity)
            {
                return (true, _price, applicableItems.Take(_quantity).Select(ai => ai.Id));
            }

            return Settings.NO_DISCOUNT;
        }
    }
}