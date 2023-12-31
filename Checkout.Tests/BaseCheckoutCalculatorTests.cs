﻿using Checkout.App;
using Checkout.Domain;
using Moq;

namespace Checkout.Tests
{
    public abstract class BaseCheckoutCalculatorTests
    {
        protected readonly Mock<IDiscountFactory> MockDiscountFactory;

        public BaseCheckoutCalculatorTests()
        {
            MockDiscountFactory = new Mock<IDiscountFactory>();
        }

        protected abstract ICheckoutPriceCalculator CreateSUT();
    }
}
