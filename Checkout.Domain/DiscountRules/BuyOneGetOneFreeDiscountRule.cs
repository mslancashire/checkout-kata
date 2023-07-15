using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;

namespace Checkout.Domain.DiscountRules
{
    public class BuyOneGetOneFreeDiscountRule : IDiscount
    {
        private readonly string _name;
        private readonly string _sku;

        private const int FREE = 0;

        public BuyOneGetOneFreeDiscountRule(string name, string sku)
        {
            _name = name;
            _sku = sku;
        }

        public string Name => _name;

        public (bool IsApplicable, int DiscountedPrice, IEnumerable<Guid> AppliedTo) CalculateDiscounts(IEnumerable<ICheckoutItem> items)
        {
            if (items == null || items.Any() == false)
            {
                return Settings.NO_DISCOUNT;
            }

            var applicableItems = items.Where(i => i.Product.SKU == _sku).ToList();
            var appliedCartIds = new List<Guid>();

            for (var i = 1; i <= applicableItems.Count; i++)
            {
                if (i % 2 == 0)
                {
                    appliedCartIds.Add(applicableItems[i-1].Id);
                }
            }

            if (appliedCartIds.Any() == false)
            {
                return Settings.NO_DISCOUNT;
            }
            
            return (true, FREE, appliedCartIds);
        }
    }
}
