using Checkout.Domain;
using Checkout.Domain.Calculators;
using FluentAssertions;

namespace Checkout.Tests.Calculators
{
    public class CheckoutCalculatorBasicTests : BaseCheckoutCalculatorTests
    {
        public CheckoutCalculatorBasicTests()
        {
        }

        protected override ICheckoutPriceCalculator CreateSUT() => new CheckoutCalculatorBasic();

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
        public void GetTotalPrice_should_return_correct_total_price_based_on_one_item()
        {
            // arrange
            const string testSKU = "Test-SKU";
            const int testPrice = 99;

            var items = new[] { Helper.CreateCheckoutItem(testSKU, testPrice) };

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(testPrice);
        }

        [Fact]
        public void GetTotalPrice_should_return_correct_total_based_on_two_items()
        {
            // arrange
            const string testSKU_A = "Test-SKU-A";
            const int testPrice_A = 99;

            const string testSKU_B = "Test-SKU-B";
            const int testPrice_B = 15;

            var items = new[] { Helper.CreateCheckoutItem(testSKU_A, testPrice_A), Helper.CreateCheckoutItem(testSKU_B, testPrice_B) };

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(testPrice_A + testPrice_B);
        }
    }
}
