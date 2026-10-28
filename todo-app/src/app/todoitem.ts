export interface TodoItem {
  id: string;
  description: string;
  isCompleted: boolean;
}

export interface CreateTodoItem {
  description: string;
  isCompleted: boolean;
}
