using Checkout.Domain;
using Checkout.Domain.DiscountRules;
using FluentAssertions;

namespace Checkout.Tests.DiscountRules
{
    public class BuyOneGetOneFreeDiscountRuleTests
    {
        private IDiscount CreateSUT()
        {
            return new BuyOneGetOneFreeDiscountRule("Buy one Test-SKU-BOGOF get one Test-SKU-BOGOF free.", "Test-SKU-BOGOF");
        }
        
        [Fact]
        public void BuyOneGetOneFreeDiscountRule_should_not_be_applicable_when_null_shopping_cart_is_provided()
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
        public void BuyOneGetOneFreeDiscountRule_should_not_be_applicable_when_empty_shopping_cart_is_provided()
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
        public void BuyOneGetOneFreeDiscountRule_should_not_be_applicable_when_no_products_are_appliable()
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
        public void BuyOneGetOneFreeDiscountRule_should_not_be_applicable_when_only_one_product_is_appliable()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-NA"),
                Helper.CreateCheckoutItem("Test-SKU-BOGOF")
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
        public void BuyOneGetOneFreeDiscountRule_should_be_applicable_when_two_applicable_products_are_in_cart_but_only_apply_to_one()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-BOGOF"),
                Helper.CreateCheckoutItem("Test-SKU-BOGOF")
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(0);
            discount.AppliedTo.Should().NotBeEmpty();
            discount.AppliedTo.Should().HaveCount(1);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[0].Id);
        }

        [Fact]
        public void BuyOneGetOneFreeDiscountRule_should_be_applicable_when_three_applicable_products_are_in_cart_but_only_apply_to_one()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-BOGOF"),
                Helper.CreateCheckoutItem("Test-SKU-BOGOF"),
                Helper.CreateCheckoutItem("Test-SKU-BOGOF")
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(0);
            discount.AppliedTo.Should().NotBeEmpty();
            discount.AppliedTo.Should().HaveCount(1);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[0].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[2].Id);
        }

        [Fact]
        public void BuyOneGetOneFreeDiscountRule_should_be_applicable_when_five_applicable_products_are_in_cart_but_only_apply_to_two()
        {
            // arrange
            var shoppingCartItems = new[]
            {
                Helper.CreateCheckoutItem("Test-SKU-BOGOF"),
                Helper.CreateCheckoutItem("Test-SKU-BOGOF"),
                Helper.CreateCheckoutItem("Test-SKU-BOGOF"),
                Helper.CreateCheckoutItem("Test-SKU-BOGOF"),
                Helper.CreateCheckoutItem("Test-SKU-BOGOF")
            };

            var sut = CreateSUT();

            // act
            var discount = sut.CalculateDiscounts(shoppingCartItems);

            // assert
            discount.IsApplicable.Should().BeTrue();
            discount.DiscountedPrice.Should().Be(0);
            discount.AppliedTo.Should().NotBeEmpty();
            discount.AppliedTo.Should().HaveCount(2);
            discount.AppliedTo.Should().Contain(shoppingCartItems[1].Id);
            discount.AppliedTo.Should().Contain(shoppingCartItems[3].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[0].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[2].Id);
            discount.AppliedTo.Should().NotContain(shoppingCartItems[4].Id);
        }
    }
}
