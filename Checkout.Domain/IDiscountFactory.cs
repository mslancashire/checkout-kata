namespace Checkout.Domain
{
    /// <summary>
    /// Provices access to a set of discount rules.
    /// </summary>
    public interface IDiscountFactory
    {
        /// <summary>
        /// Creates the current set of discount rules.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IDiscount> CreateDiscountRules();
    }
}
