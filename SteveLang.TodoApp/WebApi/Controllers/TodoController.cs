using Microsoft.AspNetCore.Mvc;
using SteveLang.TodoApp.Dtos;
using SteveLang.TodoApp.Services;

namespace SteveLang.TodoApp.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;

    public TodoController(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [HttpGet]
    public async Task<List<TodoItemDto>> GetAll()
    {
        return await _todoService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetById(Guid id)
    {
        var todoItem = await _todoService.GetById(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        return Ok(todoItem);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] TodoItemDto todoItemDto)
    {
        await _todoService.Update(todoItemDto);
        return Ok();
    }
}
