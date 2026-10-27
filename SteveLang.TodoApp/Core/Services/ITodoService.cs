using SteveLang.TodoApp.Dtos;

namespace SteveLang.TodoApp.Services;

public interface ITodoService
{
    Task<List<TodoItemDto>> GetAll();
    Task<TodoItemDto?> GetById(Guid id);
    Task Update(TodoItemDto dto);
}
