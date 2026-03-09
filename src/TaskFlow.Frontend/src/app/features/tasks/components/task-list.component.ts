import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { TaskService, Task, CreateTaskRequest } from '../services/task.service';
import { AuthService } from '../../auth/services/auth.service';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="tasks-container">
      <div class="header">
        <h1>Minhas Tarefas</h1>
        <button (click)="onLogout()" class="logout-btn">Sair</button>
      </div>

      <div class="create-task">
        <h2>Nova Tarefa</h2>
        <form (ngSubmit)="onCreateTask()">
          <div class="form-group">
            <input
              type="text"
              [(ngModel)]="newTask.title"
              name="title"
              placeholder="Título da tarefa"
              required
            />
          </div>

          <div class="form-group">
            <textarea
              [(ngModel)]="newTask.description"
              name="description"
              placeholder="Descrição (opcional)"
              rows="3"
            ></textarea>
          </div>

          <div class="form-row">
            <div class="form-group">
              <select [(ngModel)]="newTask.priority" name="priority">
                <option value="Low">Baixa</option>
                <option value="Medium">Média</option>
                <option value="High">Alta</option>
                <option value="Critical">Crítica</option>
              </select>
            </div>

            <div class="form-group">
              <input
                type="datetime-local"
                [(ngModel)]="newTask.dueDate"
                name="dueDate"
                placeholder="Data de vencimento"
              />
            </div>
          </div>

          <button type="submit" [disabled]="loading" class="create-btn">
            {{ loading ? 'Criando...' : 'Criar Tarefa' }}
          </button>
        </form>
      </div>

      <div class="tasks-list">
        <h2>Tarefas ({{ tasks.length }})</h2>
        
        <div *ngIf="tasks.length === 0" class="no-tasks">
          Nenhuma tarefa criada ainda. Crie uma acima!
        </div>

        <div *ngFor="let task of tasks" class="task-card">
          <div class="task-header">
            <h3>{{ task.title }}</h3>
            <span class="priority" [ngClass]="'priority-' + task.priority.toLowerCase()">
              {{ task.priority }}
            </span>
          </div>

          <p *ngIf="task.description" class="task-description">
            {{ task.description }}
          </p>

          <div class="task-footer">
            <span class="status">{{ task.status }}</span>
            <span class="date">{{ task.createdAt | date: 'short' }}</span>
            <button (click)="onDeleteTask(task.id)" class="delete-btn">Deletar</button>
          </div>
        </div>
      </div>

      <div *ngIf="error" class="error-message">
        {{ error }}
      </div>
    </div>
  `,
  styles: [`
    .tasks-container {
      max-width: 1000px;
      margin: 0 auto;
      padding: 20px;
      background: #f5f5f5;
      min-height: 100vh;
    }

    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 30px;
      background: white;
      padding: 20px;
      border-radius: 10px;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    }

    h1 {
      color: #667eea;
      margin: 0;
    }

    .logout-btn {
      padding: 10px 20px;
      background: #d32f2f;
      color: white;
      border: none;
      border-radius: 5px;
      cursor: pointer;
      font-weight: 600;
    }

    .logout-btn:hover {
      background: #b71c1c;
    }

    .create-task {
      background: white;
      padding: 20px;
      border-radius: 10px;
      margin-bottom: 30px;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    }

    .create-task h2 {
      color: #333;
      margin-top: 0;
    }

    .form-group {
      margin-bottom: 15px;
    }

    .form-row {
      display: grid;
      grid-template-columns: 1fr 1fr;
      gap: 15px;
    }

    input, textarea, select {
      width: 100%;
      padding: 10px;
      border: 1px solid #ddd;
      border-radius: 5px;
      font-size: 14px;
      box-sizing: border-box;
      font-family: inherit;
    }

    input:focus, textarea:focus, select:focus {
      outline: none;
      border-color: #667eea;
      box-shadow: 0 0 5px rgba(102, 126, 234, 0.3);
    }

    .create-btn {
      width: 100%;
      padding: 12px;
      background: #667eea;
      color: white;
      border: none;
      border-radius: 5px;
      font-size: 16px;
      font-weight: 600;
      cursor: pointer;
      transition: background 0.3s;
    }

    .create-btn:hover:not(:disabled) {
      background: #5568d3;
    }

    .create-btn:disabled {
      background: #ccc;
      cursor: not-allowed;
    }

    .tasks-list {
      background: white;
      padding: 20px;
      border-radius: 10px;
      box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    }

    .tasks-list h2 {
      color: #333;
      margin-top: 0;
    }

    .no-tasks {
      text-align: center;
      color: #999;
      padding: 40px 20px;
    }

    .task-card {
      background: #f9f9f9;
      padding: 15px;
      border-radius: 5px;
      margin-bottom: 15px;
      border-left: 4px solid #667eea;
    }

    .task-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 10px;
    }

    .task-header h3 {
      margin: 0;
      color: #333;
    }

    .priority {
      padding: 5px 10px;
      border-radius: 3px;
      font-size: 12px;
      font-weight: 600;
      color: white;
    }

    .priority-low {
      background: #4caf50;
    }

    .priority-medium {
      background: #ff9800;
    }

    .priority-high {
      background: #f44336;
    }

    .priority-critical {
      background: #9c27b0;
    }

    .task-description {
      color: #666;
      margin: 10px 0;
      font-size: 14px;
    }

    .task-footer {
      display: flex;
      justify-content: space-between;
      align-items: center;
      font-size: 12px;
      color: #999;
    }

    .status {
      background: #e0e0e0;
      padding: 3px 8px;
      border-radius: 3px;
    }

    .delete-btn {
      padding: 5px 10px;
      background: #d32f2f;
      color: white;
      border: none;
      border-radius: 3px;
      cursor: pointer;
      font-size: 12px;
    }

    .delete-btn:hover {
      background: #b71c1c;
    }

    .error-message {
      color: #d32f2f;
      background: #ffebee;
      padding: 15px;
      border-radius: 5px;
      margin-top: 20px;
      text-align: center;
    }
  `]
})
export class TaskListComponent implements OnInit {
  tasks: Task[] = [];
  newTask: CreateTaskRequest = {
    title: '',
    description: '',
    priority: 'Medium',
    dueDate: ''
  };
  loading = false;
  error = '';

  constructor(
    private taskService: TaskService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getAllTasks().subscribe({
      next: (tasks) => {
        this.tasks = tasks;
      },
      error: (err) => {
        this.error = 'Erro ao carregar tarefas';
      }
    });
  }

  onCreateTask(): void {
    if (!this.newTask.title.trim()) {
      this.error = 'Por favor, preencha o título da tarefa';
      return;
    }

    this.loading = true;
    this.error = '';

    this.taskService.createTask(this.newTask).subscribe({
      next: (task) => {
        this.tasks.push(task);
        this.newTask = {
          title: '',
          description: '',
          priority: 'Medium',
          dueDate: ''
        };
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Erro ao criar tarefa';
        this.loading = false;
      }
    });
  }

  onDeleteTask(id: string): void {
    if (confirm('Tem certeza que deseja deletar esta tarefa?')) {
      this.taskService.deleteTask(id).subscribe({
        next: () => {
          this.tasks = this.tasks.filter(t => t.id !== id);
        },
        error: (err) => {
          this.error = 'Erro ao deletar tarefa';
        }
      });
    }
  }

  onLogout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
