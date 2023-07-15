namespace Checkout.Domain
{
    public static class Settings
    {
        public const int EMPTY_CHECKOUT_PRICE = 0;
        public const int PRODUCT_PRICE_BAG = 5;
        public const int BAG_ITEM_CAPACITY = 5;

        public static (bool IsApplicable, int DiscountedPrice, IEnumerable<Guid> AppliedTo) NO_DISCOUNT = (false, 0, new List<Guid>());
    }
}
