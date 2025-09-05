namespace TaskMate.Api.Dtos
{
    public class TaskReadDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Descricao { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}