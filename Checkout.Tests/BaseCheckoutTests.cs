using Checkout.App;
using Checkout.Domain;
using Moq;

namespace Checkout.Tests
{
    public abstract class BaseCheckoutTests
    {
        protected readonly Mock<ICheckoutPriceCalculator> MockPriceCalculator;
        protected readonly Mock<IProductSKUFactory> MockProductFactory;

        public BaseCheckoutTests()
        {
            MockPriceCalculator = new Mock<ICheckoutPriceCalculator>();
            MockProductFactory = new Mock<IProductSKUFactory>();
        }
        protected ICheckout CreateSUT()
        {
            return new Till
            (
                new[] { MockPriceCalculator.Object },
                MockProductFactory.Object
            );
        }
    }
}
