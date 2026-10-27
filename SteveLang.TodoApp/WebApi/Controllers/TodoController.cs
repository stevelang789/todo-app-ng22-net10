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

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] TodoItemDto todoItemDto)
    {
        await _todoService.Add(todoItemDto);
        return Ok();
    }
}
