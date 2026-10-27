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

    public async Task Add(TodoItemDto dto)
    {
        _context.Add(new TodoItem
        {
            Id = Guid.NewGuid(),
            Description = dto.Description
        });

        await _context.SaveChangesAsync();
    }

    public async Task<List<TodoItemDto>> GetAll()
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

    public async Task Update(TodoItemDto todoItemDto)
    {
        var todoItem = await _context.TodoItems.FindAsync(todoItemDto.Id);

        if (todoItem == null) return;

        todoItem.Description = todoItemDto.Description;
        todoItem.IsCompleted = todoItemDto.IsCompleted;

        await _context.SaveChangesAsync();
    }
}
