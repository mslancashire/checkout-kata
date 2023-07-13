// See https://aka.ms/new-console-template for more information
using Checkout.App;
using Checkout.Domain;
using Checkout.Domain.Calculators;
using Microsoft.Extensions.DependencyInjection;

var serviceCollection = new ServiceCollection();
var serviceProvider = serviceCollection

    // Added as singleton as they are currently static, these could be replaced with services that get products and discount rules from an api or DB.
    // The fetching proccess within the Till could also be then changed to be an async await for both so the fetch runs concurrently,
    // or fetched before the total price calculation instead of in the construtor..
    .AddSingleton<ICheckoutPriceCalculator, CheckoutCalculatorWithDiscounts>()
    .AddSingleton<ICheckoutPriceCalculator, CheckoutCalculatorForExtras>()
    .AddSingleton<IProductSKUFactory, ProductSKUFactory>()
    .AddSingleton<IDiscountFactory, DiscountRulesFactory>()
    
    // Added as scoped so that for each scope of the checkout a current product and discount rules set is take, but not changed for that scope.
    .AddScoped<ICheckout, Till>()
    
    .BuildServiceProvider();

var checkout = serviceProvider.GetRequiredService<ICheckout>();

var shoppingCartItems = new[]
{
    "A", "B", "A", "C", "D", "A", "B"
};

Console.WriteLine("Checking out shopping cart...");

foreach (var shoppingCartItem in shoppingCartItems)
{
    checkout.Scan(shoppingCartItem);
}

Console.WriteLine($"Total shopping cart £{checkout.GetTotalPrice()}");