namespace Checkout.Domain
{
    /// <summary>
    /// Provides a unique reference to a scanned checkout item.
    /// </summary>
    public interface ICheckoutItem
    {        
        /// <summary>
        /// The unique id of a scanned checkout item.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The identified product of the scanned checkout item.
        /// </summary>
        IPricedSKU Product { get; }
    }
}
