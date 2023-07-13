using Checkout.Domain;
using Checkout.Domain.DiscountRules;

namespace Checkout.App
{
    public class DiscountRulesFactory : IDiscountFactory
    {
        private readonly IEnumerable<IDiscount> _discountRules = new[]
        {
            new SKUQuantityDiscountRule("Buy 3 A's for £130.", "A", 3, 130),
            new SKUQuantityDiscountRule("Buy 2 B's for £45.", "B", 2, 45)
        };

        public DiscountRulesFactory()
        {
        }

        public IEnumerable<IDiscount> CreateDiscountRules()
        {
            return _discountRules;
        }
    }
}
