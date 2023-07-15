using Checkout.Domain;
using Checkout.Domain.Calculators;
using FluentAssertions;

namespace Checkout.Tests.Calculators
{
    public class CheckoutCalculatorForExtrasTests : BaseCheckoutCalculatorTests
    {
        public CheckoutCalculatorForExtrasTests()
        {
        }

        protected override ICheckoutPriceCalculator CreateSUT() => new CheckoutCalculatorForExtras();

        [Fact]
        public void GetTotalPrice_should_return_0_when_no_items_in_checkout_and_no_bags_are_required()
        {
            // arrange
            IEnumerable<ICheckoutItem>? items = null;

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(0);
        }

        [Fact]
        public void GetTotalPrice_should_return_correct_value_when_one_bag_is_required_with_one_item_in_checkout()
        {
            // arrange
            var items = new[] { Helper.CreateCheckoutItem() };

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(5);
        }

        [Fact]
        public void GetTotalPrice_should_return_correct_value_when_one_bag_is_required_with_five_items_in_checkout()
        {
            // arrange
            var items = new[]
            {
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem()
            };

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(5);
        }

        [Fact]
        public void GetTotalPrice_should_return_correct_value_when_two_bags_are_required_with_six_items_in_checkout()
        {
            // arrange
            var items = new[]
            {
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem(),
                Helper.CreateCheckoutItem()
            };

            var sut = CreateSUT();

            // act
            var price = sut.GetTotalPrice(items);

            // assert
            price.Should().Be(10);
        }
    }
}
