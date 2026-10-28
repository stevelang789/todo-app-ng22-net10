using Microsoft.EntityFrameworkCore;
using SteveLang.TodoApp.Model;

namespace SteveLang.TodoApp.Persistence;

public class TodoDbContext : DbContext
{
    public TodoDbContext()
    {
    }

    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new TodoItemEntityTypeConfiguration().Configure(modelBuilder.Entity<TodoItem>());
    }
}
