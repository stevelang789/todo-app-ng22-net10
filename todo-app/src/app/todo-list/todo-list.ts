import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { TodoService } from '../todo.service';
import { TodoItem } from '../todoitem';
import { MatListModule } from '@angular/material/list';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';

@Component({
  imports: [MatListModule, MatButtonModule, MatCardModule],
  selector: 'app-todo-list',
  styleUrls: ['./todo-list.css'],
  templateUrl: './todo-list.html',
})
export class TodoList {
  todoList: TodoItem[] = [];
  todoService: TodoService = inject(TodoService);
  router: Router = inject(Router);
  changeDetectorRef: ChangeDetectorRef = inject(ChangeDetectorRef);

  constructor() {
    this.todoService
    .getAll()
    .then((list) => {
      this.todoList = list;
      this.changeDetectorRef.markForCheck();
    });
  }

  edit(id: string) {
    this.router.navigate(['/todo', id]);
  }

  add() {
    this.router.navigate(['/new-todo']);
  }
}
