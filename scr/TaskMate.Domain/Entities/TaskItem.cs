namespace TaskMate.Domain.Entities
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Status { get; set; } = "A Fazer";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}