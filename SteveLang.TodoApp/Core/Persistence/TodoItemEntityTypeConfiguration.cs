using Microsoft.EntityFrameworkCore;
using SteveLang.TodoApp.Model;

namespace SteveLang.TodoApp.Persistence;

public class TodoItemEntityTypeConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Description).IsRequired().HasMaxLength(200);
        builder.Property(t => t.IsCompleted).IsRequired();
        builder.Property(t => t.DateCreatedUtc).IsRequired();
    }
}
