import { Component, inject, signal } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TodoService } from '../todo.service';
import { TodoItem } from '../todoitem';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { form, FormField, pattern, required, requiredError, validate } from '@angular/forms/signals';
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
  todoForm = form(this.todoModel, (schemaPath) => {
    required(schemaPath.description, { message: 'Description is required' });
    validate(schemaPath.description, ({ value }) => {
      if (value().trim().length === 0) {
        return {
          kind: 'required',
          message: 'Description is required'
        }
      }
      return null;
    });
  });
  isNew = false;

  constructor() {
    const mode = this.route.snapshot.data['mode'];
    this.isNew = mode === 'new';
    if (this.isNew) {
      this.todoItem = {
        id: '',
        description: '',
        isCompleted: false
      };
    } else {
      const id = this.route.snapshot.params['id'];
      if (id) {
        this.todoService.getById(id).then(item => {
          this.todoItem = item;
          this.todoForm.description().value.set(this.todoItem?.description ?? '');
          this.todoForm.isCompleted().value.set(this.todoItem?.isCompleted ?? false);
        });
      }
    }
  }

  async onSubmit(event: Event) {
    event.preventDefault();
    console.log('Submitting form');
    if (this.todoItem) {
      this.todoItem.description = this.todoForm.description().value().trim();
      this.todoItem.isCompleted = this.todoForm.isCompleted().value();
      await this.todoService.save(this.todoItem);
      this.router.navigate(['/']);
    }
  }

  cancel() {
    this.router.navigate(['/']);
  }
}
