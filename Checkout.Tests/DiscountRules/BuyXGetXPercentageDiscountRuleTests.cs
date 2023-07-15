using Checkout.Domain;
using Checkout.Domain.DiscountRules;
using FluentAssertions;

namespace Checkout.Tests.DiscountRules
{
    public class BuyXGetXPercentageDiscountRuleTests
    {
        public IDiscount CreateSUT()
        {
            return new BuyXGetXPercentageDiscountRule("", "Test-SKU-%", 2, 10);
        }

        [Fact]
        public void BuyXGetXPercentageDiscountRuleTests_should_not_be_applicable_when_null_shopping_cart_is_provided()
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
        public void BuyXGetXPercentageDiscountRuleTests_should_not_be_applicable_when_empty_shopping_cart_is_provided()
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
        public void BuyXGetXPercentageDiscountRuleTests_should_not_be_applicable_when_no_products_are_appliable()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-NA")
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
        public void BuyXGetXPercentageDiscountRuleTests_should_not_be_applicable_when_only_one_product_is_appliable()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-NA"),
                Helper.CreateCheckoutItem("Test-SKU-%")
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
        public void BuyXGetXPercentageDiscountRuleTests_should_be_applicable_when_two_products_are_appliable()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
                Helper.CreateCheckoutItem("Test-SKU-%", 50)
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(90);
            discount.AppliedTo.Should().NotBeEmpty();
            discount.AppliedTo.Should().HaveCount(2);
            discount.AppliedTo.Should().Contain(shoppingCartItems[0].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
        }

        [Fact]
        public void BuyXGetXPercentageDiscountRuleTests_should_be_applicable_when_three_products_are_appliable_but_only_apply_to_two()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(90);
            discount.AppliedTo.Should().NotBeEmpty();
            discount.AppliedTo.Should().HaveCount(2);
            discount.AppliedTo.Should().Contain(shoppingCartItems[0].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[2].Id);
        }

        [Fact]
        public void BuyXGetXPercentageDiscountRuleTests_should_be_applicable_when_five_products_are_appliable_but_only_apply_to_four()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
                Helper.CreateCheckoutItem("Test-SKU-%", 50),
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(180);
            discount.AppliedTo.Should().NotBeEmpty();
            discount.AppliedTo.Should().HaveCount(4);
            discount.AppliedTo.Should().Contain(shoppingCartItems[0].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[2].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[3].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[4].Id);
        }

        [Fact]
        public void BuyXGetXPercentageDiscountRuleTests_should_be_applicable_when_two_products_are_appliable_calculate_based_on_different_pricing()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-%", 100),
                Helper.CreateCheckoutItem("Test-SKU-%", 50)
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(135);
            discount.AppliedTo.Should().NotBeEmpty();
            discount.AppliedTo.Should().HaveCount(2);
            discount.AppliedTo.Should().Contain(shoppingCartItems[0].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
        }
    }
}
