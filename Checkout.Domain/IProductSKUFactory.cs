namespace Checkout.Domain
{
    /// <summary>
    /// Provices access to a set of products.
    /// </summary>
    public interface IProductSKUFactory
    {
        /// <summary>
        /// Creates the current set of SKU with their prices.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPricedSKU> CreateCurrentSKUPrices();
    }
}
