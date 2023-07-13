using Checkout.Domain;

namespace Checkout.App
{
    public class ProductSKUFactory : IProductSKUFactory
    {
        private readonly IEnumerable<ProductSKU> _products = new[]
        {
            new ProductSKU("A", 50),
            new ProductSKU("B", 30),
            new ProductSKU("C", 20),
            new ProductSKU("D", 15),
        };

        public ProductSKUFactory()
        { }

        public IEnumerable<IPricedSKU> CreateCurrentSKUPrices()
        {
            return _products;
        }
    }
}
