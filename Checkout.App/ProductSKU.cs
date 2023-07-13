using Checkout.Domain;

namespace Checkout.App
{
    public class ProductSKU : IPricedSKU
    {
        public ProductSKU(string sku, int price)
        {
            SKU = sku;
            Price = price;
        }

        public static ProductSKU CreateEmptyProduct()
        {
            return new ProductSKU("", 0);
        }

        public string SKU { get; private set; }
        
        public int Price { get; private set; }
    }
}
