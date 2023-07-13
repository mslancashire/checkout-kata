using Checkout.Domain;
using Checkout.Domain.Calculators;
using Checkout.Domain.DiscountRules;
using FluentAssertions;

namespace Checkout.Tests
{
    public class CheckoutCalculatorWithDiscountsTests : BaseCheckoutCalculatorTests
    {
        protected override ICheckoutPriceCalculator CreateSUT() => new CheckoutCalculatorWithDiscounts(MockDiscountFactory.Object);

        [Fact]
        public void GetTotalPrice_should_return_0_when_there_are_no_items_in_checkout()
        {
            // arrange
            var items = new List<ICheckoutItem>();

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(0);
        }

        [Fact]
        public void GetTotalPrice_should_return_correct_price_based_on_all_items_being_discounted()
        {
            // arrange
            const string testSKU = "Test-SKU";
            const int testPrice = 10;

            var items = new[] { CreateCheckoutItem(testSKU, testPrice), CreateCheckoutItem(testSKU, testPrice) };

            MockDiscountFactory
                .Setup(mock => mock.CreateDiscountRules())
                .Returns(() => new[] { new SKUQuantityDiscountRule("Test", testSKU, 2, 15) });

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(15);
        }

        [Fact]
        public void GetTotalPrice_should_return_correct_price_based_some_items_being_discounted_with_others_at_full_price()
        {
            // arrange
            const string testSKU = "Test-SKU";
            const int testPrice = 10;

            var items = new[]
            {
                CreateCheckoutItem(testSKU, testPrice),
                CreateCheckoutItem(testSKU, testPrice),
                CreateCheckoutItem(testSKU, testPrice)
            };

            MockDiscountFactory
                .Setup(mock => mock.CreateDiscountRules())
                .Returns(() => new[] { new SKUQuantityDiscountRule("Test", testSKU, 2, 15) });

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(25);
        }
    }
}