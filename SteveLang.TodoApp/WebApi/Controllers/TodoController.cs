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
    public async Task<ActionResult<List<TodoItemDto>>> GetAll()
    {
        return Ok(await _todoService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItemDto>> GetById(Guid id)
    {
        var todoItem = await _todoService.GetByIdAsync(id);
        if (todoItem == null)
        {
            return NotFound();
        }
        return Ok(todoItem);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoItemDto dto)
    {
        await _todoService.CreateAsync(dto);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] TodoItemDto dto)
    {
        await _todoService.UpdateAsync(dto);
        return Ok();
    }
}
