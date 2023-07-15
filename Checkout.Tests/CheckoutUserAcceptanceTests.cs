using Checkout.App;
using Checkout.Domain;
using Checkout.Domain.Calculators;
using Checkout.Domain.Exceptions;
using FluentAssertions;

namespace Checkout.Tests
{
    public class CheckoutUserAcceptanceTests
    {
        private List<ICheckoutPriceCalculator> _priceCalculators;

        public CheckoutUserAcceptanceTests()
        {
            _priceCalculators = new List<ICheckoutPriceCalculator>()
            {
                new CheckoutCalculatorWithDiscounts(new DiscountRulesFactory()),
            };
        }

        private ICheckout CreateSUT()
        {
            return new Till
            (
                _priceCalculators,
                new ProductSKUFactory()
            );
        }

        private void AddExtraCalculator()
        {
            _priceCalculators.Add(new CheckoutCalculatorForExtras());
        }

        [Fact]
        public void Scan_should_throw_execption_when_item_is_unexpected()
        {
            // arrange
            const string item = "Unknown-Item";
            var sut = CreateSUT();

            // act
            var exception = Assert.Throws<UnexpectedItemInShoppingCartExecption>(() => sut.Scan(item));

            // assert
            exception.Message.Should().Be("Item Unknown-Item is not a vaild product.");
            sut.GetTotalPrice().Should().Be(0);
        }

        [Fact]
        public void Scan_should_add_item_A_to_checkout_and_return_correct_total_price()
        {
            // arrange
            const string item = "A";
            var sut = CreateSUT();

            // act
            sut.Scan(item);

            // assert
            sut.GetTotalPrice().Should().Be(50);
        }

        [Fact]
        public void Scan_should_add_item_B_to_checkout_and_return_correct_total_price()
        {
            // arrange
            const string item = "B";
            var sut = CreateSUT();

            // act
            sut.Scan(item);

            // assert
            sut.GetTotalPrice().Should().Be(30);
        }

        [Fact]
        public void Scan_should_add_item_C_to_checkout_and_return_correct_total_price()
        {
            // arrange
            const string item = "C";
            var sut = CreateSUT();

            // act
            sut.Scan(item);

            // assert
            sut.GetTotalPrice().Should().Be(20);
        }

        [Fact]
        public void Scan_should_add_item_D_to_checkout_and_return_correct_total_price()
        {
            // arrange
            const string item = "D";
            var sut = CreateSUT();

            // act
            sut.Scan(item);

            // assert
            sut.GetTotalPrice().Should().Be(15);
        }

        [Fact]
        public void Scan_should_add_items_to_checkout_and_return_correct_total_price_based_on_discounts_and_extra_bags()
        {
            // arrange
            AddExtraCalculator();

            var sut = CreateSUT();

            var shoppingCartItems = new[]
            {
                "A", "B", "A", "C", "D", "A", "B"
            };

            // act
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                sut.Scan(shoppingCartItem);
            }

            // assert
            sut.GetTotalPrice().Should().Be(220);
        }

        [Fact]
        public void Scan_should_add_items_to_checkout_and_return_correct_total_price_based_on_discounted_items_full_priced_items_and_extra_bags()
        {
            // arrange
            AddExtraCalculator();

            var sut = CreateSUT();

            var shoppingCartItems = new[]
            {
                "A", "B", "A", "C", "D", "A", "B", "C", "A", "B", "C", "D", "D"
            };

            // act
            foreach (var shoppingCartItem in shoppingCartItems)
            {
                sut.Scan(shoppingCartItem);
            }

            // assert
            sut.GetTotalPrice().Should().Be(349);
        }
    }
}