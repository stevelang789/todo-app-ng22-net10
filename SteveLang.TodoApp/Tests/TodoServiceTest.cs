using Moq;
using Moq.EntityFrameworkCore;
using SteveLang.TodoApp.Dtos;
using SteveLang.TodoApp.Model;
using SteveLang.TodoApp.Persistence;
using SteveLang.TodoApp.Services;
using Xunit;

namespace SteveLang.TodoApp.Tests;

public class TodoServiceTest
{
    [Fact]
    public async Task GetAllAsync_ReturnsInAscendingOrderByDateCreatedUtc()
    {
        // Arrange
        var todoDbContextMock = new Mock<TodoDbContext>();
        var todoItems = new List<TodoItem>
        {
            new() { Id = Guid.NewGuid(), Description = "Task 2", IsCompleted = false, DateCreatedUtc = DateTime.UtcNow.AddDays(1) },
            new() { Id = Guid.NewGuid(), Description = "Task 1", IsCompleted = true, DateCreatedUtc = DateTime.UtcNow }
        };
        todoDbContextMock.Setup(x => x.TodoItems).ReturnsDbSet(todoItems);
        var todoService = new TodoService(todoDbContextMock.Object);
        // Act
        var result = await todoService.GetAllAsync();
        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal("Task 1", result[0].Description);
        Assert.Equal("Task 2", result[1].Description);
    }

    [Fact]
    public async Task CreateAsync_AddsNewTodoItem()
    {
        // Arrange
        var todoDbContextMock = new Mock<TodoDbContext>();
        var todoService = new TodoService(todoDbContextMock.Object);
        var dto = new CreateTodoItemDto
        {
            Description = "New Task"
        };
        // Act
        await todoService.CreateAsync(dto);
        // Assert
        todoDbContextMock.Verify(x => x.AddAsync(It.Is<TodoItem>(item => item.Description == "New Task")), Times.Once);
        todoDbContextMock.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}
