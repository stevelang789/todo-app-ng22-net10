namespace SteveLang.TodoApp.Dtos;

public class TodoItemDto
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
