using Checkout.App;
using Checkout.Domain;
using Checkout.Domain.DiscountRules;
using FluentAssertions;

namespace Checkout.Tests.DiscountRules
{
    public class SKUQuantityDiscountRuleTests
    {
        private IDiscount CreateSUT()
        {
            return new SKUQuantityDiscountRule("Buy 2 get £5 off.", "Test-SKU-Discounted", 2, 15);
        }

        [Fact]
        public void SKUQuantityDiscountRule_should_not_be_applicable_when_null_cart_is_provided()
        {
            // arrange
            IEnumerable<ICheckoutItem>? shoppingCartItems = null;
            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeFalse();
            discount.DiscountedPrice.Should().Be(0);
            discount.AppliedTo.Should().BeEmpty();
        }

        [Fact]
        public void SKUQuantityDiscountRule_should_not_be_applicable_when_cart_is_empty()
        {
            // arrange
            var shoppingCartItems = new List<ICheckoutItem>();
            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeFalse();
            discount.DiscountedPrice.Should().Be(0);
            discount.AppliedTo.Should().BeEmpty();
        }

        [Fact]
        public void SKUQuantityDiscountRule_should_not_be_applicable_when_cart_does_not_contain_discounted_sku()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                new CheckoutItem(Guid.NewGuid(), new ProductSKU("Test-SKU-Not-Discounted", 10))
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert            
            discount.IsApplicable.Should().BeFalse();
            discount.DiscountedPrice.Should().Be(0);
            discount.AppliedTo.Should().BeEmpty();
        }

        [Fact]
        public void SKUQuantityDiscountRule_should_not_be_applicable_when_cart_does_not_contain_discounted_sku_of_correct_quantity()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                new CheckoutItem(Guid.NewGuid(), new ProductSKU("Test-SKU-Discounted", 10))
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeFalse();
            discount.DiscountedPrice.Should().Be(0);
            discount.AppliedTo.Should().BeEmpty();
        }

        [Fact]
        public void SKUQuantityDiscountRule_should_be_applicable_when_cart_does_contain_discounted_sku_of_correct_quantity()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                new CheckoutItem(Guid.NewGuid(), new ProductSKU("Test-SKU-Discounted", 10)),
                new CheckoutItem(Guid.NewGuid(), new ProductSKU("Test-SKU-Discounted", 10))
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(15);
            discount.AppliedTo.Should().Contain(shoppingCartItems[0].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
        }

        [Fact]
        public void SKUQuantityDiscountRule_should_be_applicable_when_cart_does_contain_discounted_sku_of_correct_quantity_but_not_include_extra_id()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                new CheckoutItem(Guid.NewGuid(), new ProductSKU("Test-SKU-Discounted", 10)),
                new CheckoutItem(Guid.NewGuid(), new ProductSKU("Test-SKU-Discounted", 10)),
                new CheckoutItem(Guid.NewGuid(), new ProductSKU("Test-SKU-Discounted", 10)),
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(15);
            discount.AppliedTo.Should().Contain(shoppingCartItems[0].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[2].Id);
        }
    }
}
