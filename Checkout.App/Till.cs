using Checkout.Domain;
using Checkout.Domain.Exceptions;

namespace Checkout.App
{
    public class Till : ICheckout
    {
        private readonly IEnumerable<IPricedSKU> _pricedSKUs;
        private readonly List<ICheckoutItem> _checkoutItems;
        
        private readonly IEnumerable<ICheckoutPriceCalculator> _priceCalculators;

        public Till
        (
            IEnumerable<ICheckoutPriceCalculator> priceCalculators,
            IProductSKUFactory productFactory
        )
        {
            ArgumentNullException.ThrowIfNull(priceCalculators);
            ArgumentNullException.ThrowIfNull(productFactory);

            _priceCalculators = priceCalculators;
            _pricedSKUs = productFactory.CreateCurrentSKUPrices();

            _checkoutItems = new List<ICheckoutItem>();
        }

        public void Scan(string item)
        {
            var pricedSKU = GetPricedSKU(item);

            if (pricedSKU == null)
            {
                throw new UnexpectedItemInShoppingCartExecption(item);
            }

            _checkoutItems.Add(CreateCheckoutItem(pricedSKU));
        }

        private ICheckoutItem CreateCheckoutItem(IPricedSKU product)
        {
            return new CheckoutItem(Guid.NewGuid(), product);
        }

        private IPricedSKU? GetPricedSKU(string item) => _pricedSKUs.FirstOrDefault(ps => ps.SKU == item);

        public int GetTotalPrice()
        {
            if (_checkoutItems.Any() == false)
            {
                return Settings.EMPTY_CHECKOUT_PRICE;
            }

            return _priceCalculators.Sum(pc => pc.GetTotalPrice(_checkoutItems));
        }
    }
}
