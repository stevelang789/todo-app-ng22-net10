using SteveLang.TodoApp.Dtos;

namespace SteveLang.TodoApp.Services;

public interface ITodoService
{
    Task<List<TodoItemDto>> GetAllAsync();
    Task<TodoItemDto?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateTodoItemDto dto);
    Task UpdateAsync(TodoItemDto dto);
}
