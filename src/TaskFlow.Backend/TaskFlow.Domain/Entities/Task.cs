namespace TaskFlow.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public DateTime? CompletedAt { get; set; }

        public virtual User? User { get; set; }

    }

    public enum TaskStatus

    {
        Todo,
        InProgress,
        Done,
        Cancelled
    }

    public enum TaskPriority

    {
        Low,
        Medium,
        High,
        Critical
    }
}