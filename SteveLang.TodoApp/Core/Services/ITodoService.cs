using SteveLang.TodoApp.Dtos;

namespace SteveLang.TodoApp.Services;

public interface ITodoService
{
    Task<List<TodoItemDto>> GetAll();
    Task Add(TodoItemDto todoItemDto);
    Task Update(TodoItemDto todoItemDto);
}
