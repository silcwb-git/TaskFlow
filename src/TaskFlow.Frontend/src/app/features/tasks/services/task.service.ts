import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Task {
  id: string;
  title: string;
  description?: string;
  status: string;
  priority: string;
  createdAt: string;
  dueDate?: string;
  completedAt?: string;
}

export interface CreateTaskRequest {
  title: string;
  description?: string;
  priority: string;
  dueDate?: string;
}

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private apiUrl = 'https://localhost:5001/api/tasks';

  constructor(private http: HttpClient) {}

  createTask(task: CreateTaskRequest): Observable<Task> {
    return this.http.post<Task>(this.apiUrl, task);
  }

  getAllTasks(): Observable<Task[]> {
    return this.http.get<Task[]>(this.apiUrl);
  }

  getTaskById(id: string): Observable<Task> {
    return this.http.get<Task>(`${this.apiUrl}/${id}`);
  }

  updateTask(id: string, task: CreateTaskRequest): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, task);
  }

  deleteTask(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
  getTasksByPriority(priority: string): Observable<Task[]> {
    return this.http.get<Task[]>(`${this.apiUrl}/filter?priority=${priority}`);
  }
}
