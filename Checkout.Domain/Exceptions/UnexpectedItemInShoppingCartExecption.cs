namespace Checkout.Domain.Exceptions
{
    public class UnexpectedItemInShoppingCartExecption : Exception
    {
        public UnexpectedItemInShoppingCartExecption(string item) : base($"Item {item} is not a vaild product.")
        {

        }
    }
}
