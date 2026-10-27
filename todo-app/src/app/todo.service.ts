import { Service } from '@angular/core';
import { TodoItem } from './todoitem';

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
    const response = await fetch(this.url, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(item)
    });
    if (!response.ok) {
      throw new Error('Failed to save todo item');
    }
  }

  protected todoList: TodoItem[] = [
    {
      id: '1',
      description: 'Sample todo item',
      isCompleted: false
    },
    {
      id: '2',
      description: 'Another sample todo item',
      isCompleted: false
    },
    {
      id: '3',
      description: 'Yet another sample todo item',
      isCompleted: false
    }
  ];

  getTodoList(): TodoItem[] {
    return this.todoList;
  }

  // getById(id: string): TodoItem | undefined {
  //     return this.todoList.find(item => item.id === id);
  // }
}
