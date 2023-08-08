namespace TaskTracker.Services
{
    public class ItemNotFoundException : Exception
    {
        public int itemId { get; }

        public ItemNotFoundException(int id) : base($"Item ID = {id} - not found")
        {
            itemId = id;
        }
    }
}
