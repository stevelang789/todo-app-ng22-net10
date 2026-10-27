namespace SteveLang.TodoApp.Model;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public DateTime DateCreatedUtc { get; set; } = DateTime.UtcNow;
}
