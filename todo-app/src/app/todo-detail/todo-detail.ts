import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TodoService } from '../todo.service';
import { TodoItem } from '../todoitem';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { form, FormField } from '@angular/forms/signals';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';

@Component({
  imports: [FormField, MatInputModule, MatFormFieldModule, MatButtonModule, MatCardModule, MatCheckboxModule],
  selector: 'app-todo-detail',
  styleUrl: './todo-detail.css',
  templateUrl: './todo-detail.html',
})
export class TodoDetail {
  route: ActivatedRoute = inject(ActivatedRoute);
  router: Router = inject(Router);
  todoService: TodoService = inject(TodoService);
  todoItem: TodoItem | undefined;
  todoModel = signal<TodoItem>({
    id: '',
    description: '',
    isCompleted: false
  });
  todoForm = form(this.todoModel);

  constructor() {
    const id = this.route.snapshot.params['id'];
    console.log('Todo item ID:', id);
    if (id === 'new') {
      this.todoItem = {
        id: '00000000-0000-0000-0000-000000000000',
        description: '',
        isCompleted: false
      };
    } else {
      this.todoService.getById(id).then(item => {
        this.todoItem = item;
        this.todoForm.description().value.set(this.todoItem?.description ?? '');
        this.todoForm.isCompleted().value.set(this.todoItem?.isCompleted ?? false);
      });
    }
  }

  async onSubmit(event: Event) {
    event.preventDefault();
    if (this.todoItem) {
      this.todoItem.description = this.todoForm.description().value();
      this.todoItem.isCompleted = this.todoForm.isCompleted().value();
      console.log('Saving todo item:', this.todoItem);
      await this.todoService.save(this.todoItem);
      this.router.navigate(['/']);
    }
  }
}
