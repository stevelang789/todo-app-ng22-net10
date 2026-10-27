namespace SteveLang.TodoApp.Dtos;

public class TodoItemDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
