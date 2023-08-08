namespace TaskTracker.Services
{
    public class ItemByIdNotFoundException : Exception
    {
        private int _itemId { get; } = 0;

        public ItemByIdNotFoundException(int itemId) : base($"Item ID = {itemId} - not found")
        {
            _itemId = itemId;
        }
    }

    public class ItemByStringNotFoundException : Exception
    {
        private string _message { get; } = "";

        public ItemByStringNotFoundException(string message) : base($"Item STRING = {message} - not found")
        {
            _message = message;
        }
    }
}
