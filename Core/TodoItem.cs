namespace Core.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Title { get; set; } // Nullable string to avoid warnings
        public bool IsCompleted { get; set; }
    }
}
