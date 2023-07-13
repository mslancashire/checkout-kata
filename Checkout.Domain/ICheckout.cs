namespace Checkout.Domain
{
    /// <summary>
    /// Provides an interface for scanning products and getting a total price.
    /// </summary>
    public interface ICheckout
    {
        /// <summary>
        /// Will scan the provided sku and ensure its a valid product before adding it to the checkout.
        /// </summary>
        /// <param name="item"></param>
        void Scan(string item);
        
        /// <summary>
        /// Will provide the current total price of scanned items, including any applicable discounts.
        /// </summary>
        /// <returns></returns>
        int GetTotalPrice();
    }
}