using Microsoft.EntityFrameworkCore;
using SteveLang.TodoApp.Dtos;
using SteveLang.TodoApp.Model;
using SteveLang.TodoApp.Persistence;

namespace SteveLang.TodoApp.Services;

public class TodoService : ITodoService
{
    private readonly TodoDbContext _context;

    public TodoService(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<List<TodoItemDto>> GetAllAsync()
    {
        return await _context.TodoItems
            .OrderBy(x => x.DateCreatedUtc)
            .Select(x => new TodoItemDto
            {
                Id = x.Id,
                Description = x.Description,
                IsCompleted = x.IsCompleted
            })
            .ToListAsync();
    }

    public async Task<TodoItemDto?> GetByIdAsync(Guid id)
    {
        var todoItem = await _context.TodoItems.FindAsync(id);
        if (todoItem == null)
        {
            return null;
        }
        return new TodoItemDto
        {
            Id = todoItem.Id,
            Description = todoItem.Description,
            IsCompleted = todoItem.IsCompleted
        };
    }

    public async Task CreateAsync(CreateTodoItemDto dto)
    {
        if (dto == null) return;
        var todoItem = new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            DateCreatedUtc = DateTime.UtcNow
        };
        await _context.AddAsync(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItemDto dto)
    {
        if (dto == null) return;
        var todoItem = await _context.TodoItems.FindAsync(dto.Id);
        if (todoItem == null) return;
        todoItem.Description = dto.Description;
        todoItem.IsCompleted = dto.IsCompleted;
        await _context.SaveChangesAsync();
    }
}
