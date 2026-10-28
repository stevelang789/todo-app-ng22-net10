import { Service } from '@angular/core';
import { TodoItem, CreateTodoItem } from './todoitem';

@Service()
export class TodoService {
  url = 'https://localhost:7294/api/todo';

  async getAll(): Promise<TodoItem[]> {
    const response = await fetch(this.url);
    return (await response.json()) ?? [];
  }

  async getById(id: string): Promise<TodoItem | undefined> {
    const response = await fetch(`${this.url}/${id}`);
    return (await response.json()) ?? undefined;
  }

  async save(item: TodoItem): Promise<void> {
    if (item.id) {
      await this.update(item);
    } else {
      await this.create(item);
    }
  }

  private async create(item: TodoItem): Promise<void> {
    const createTodoItem: CreateTodoItem = {
      description: item.description,
      isCompleted: item.isCompleted
    };
    const response = await fetch(this.url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(createTodoItem)
    });
    if (!response.ok) {
      throw new Error('Failed to create todo item');
    }
  }

  private async update(item: TodoItem): Promise<void> {
    const response = await fetch(this.url, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(item)
    });
    if (!response.ok) {
      throw new Error('Failed to update todo item');
    }
  }
}
