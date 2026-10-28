namespace SteveLang.TodoApp.Dtos;

public class CreateTodoItemDto
{
    public string Description { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
}
