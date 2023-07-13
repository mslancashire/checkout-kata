namespace Checkout.Domain
{
    /// <summary>
    /// Provides price details of a SKU.
    /// </summary>
    public interface IPricedSKU
    {
        /// <summary>
        /// The SKU.
        /// </summary>
        string SKU { get; }

        /// <summary>
        /// The current prices of the SKU per quantity.
        /// </summary>
        int Price { get; }
    }
}
