namespace Checkout.Domain
{
    /// <summary>
    /// Provides a way of applying discounts to items at checkout.
    /// </summary>
    public interface IDiscount
    {
        /// <summary>
        /// Friendly name of the discount.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Calculates the discount, detailing if the discount is applicable to the current checkout items, the discounted price and to which items in the checkout the discount has been appplied.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        (bool IsApplicable, int DiscountedPrice, IEnumerable<Guid> AppliedTo) CalculateDiscounts(IEnumerable<ICheckoutItem> items);
    }
}
